namespace ItIsPizzaDay.Client.Services.Statics
{
    using System.Collections.Generic;
    using System.Linq;
    using ItIsPizzaDay.Shared.Models;

    public static class FoodOrderService
    {
        public static FoodOrder GetFoodOrder(Food food, ICollection<Ingredient> customIngredients = null)
        {
            var originalIngredients = GetOriginalIngredients(food);
            var foodOrderIngredient = new List<FoodOrderIngredient>();

            if (customIngredients != null)
            {
                foodOrderIngredient.AddRange(customIngredients.Except(originalIngredients).Select(ingredient => new FoodOrderIngredient
                {
                    Isremoval = false,
                    Ingredient = ingredient.Id,
                    IngredientNavigation = ingredient
                }).ToList());

                foodOrderIngredient.AddRange(originalIngredients.Except(customIngredients).Select(ingredient => new FoodOrderIngredient
                {
                    Isremoval = true,
                    Ingredient = ingredient.Id,
                    IngredientNavigation = ingredient
                }).ToList());
            }

            var foodOrder = new FoodOrder
            {
                Food = food.Id,
                FoodNavigation = food,
                FoodOrderIngredient = foodOrderIngredient
            };

            return foodOrder;
        }

        public static FoodOrder GetFoodOrderGuidOnly(Food food, ICollection<Ingredient> customIngredients = null)
        {
            var foodOrderIngredient = new List<FoodOrderIngredient>();

            var originalIngredients = GetOriginalIngredients(food);

            if (customIngredients != null)
            {
                foodOrderIngredient.AddRange(customIngredients.Except(originalIngredients).Select(ingredient => new FoodOrderIngredient
                {
                    Isremoval = false,
                    Ingredient = ingredient.Id
                }).ToList());

                foodOrderIngredient.AddRange(originalIngredients.Except(customIngredients).Select(ingredient => new FoodOrderIngredient
                {
                    Isremoval = true,
                    Ingredient = ingredient.Id
                }).ToList());
            }

            var foodOrder = new FoodOrder
            {
                Food = food.Id,
                FoodOrderIngredient = foodOrderIngredient
            };

            return foodOrder;
        }

        public static ICollection<FoodOrder> ParseGuidOnly(ICollection<FoodOrder> foodsOrder)
        {
            return foodsOrder.Select(fi =>
                new FoodOrder { Food = fi.Food, FoodOrderIngredient = fi.FoodOrderIngredient.Select(foi => new FoodOrderIngredient { Ingredient = foi.Ingredient, Isremoval = foi.Isremoval }).ToList() }).ToList();
        }

        private static ICollection<Ingredient> GetOriginalIngredients(Food food)
        {
            return food.FoodIngredient.Select(fi => fi.IngredientNavigation).ToList();
        }
        
        class Candidate
        {
            public Food Food { get; set; }
            
            public decimal Price { get; set; }
            
            public IReadOnlyList<Ingredient> UnknownIngredients { get; set; }
            
            public IReadOnlyList<Ingredient> Additions { get; set; }
            
            public IReadOnlyList<Ingredient> Removals { get; set; }
        }

        public static ICollection<FoodOrder> LowestPrice(ICollection<Ingredient> target, ICollection<Food> foods)
        {
            decimal SumPrices(Food food, IEnumerable<Ingredient> ingredients) =>
                food.Price + ingredients.Except(food.FoodIngredient.Select(ingredient => ingredient.IngredientNavigation))
                    .Aggregate(seed: 0m, (total, item) => total + (item.Price ?? 0));

            var candidates = foods.Select(food =>
            {
                var additions = target.Except(food.FoodIngredient.Select(ingredient => ingredient.IngredientNavigation)).ToList();

                return new Candidate
                {
                    Food = food,
                    Price = SumPrices(food, target),
                    UnknownIngredients = additions.Where(addition => addition.Price == null).ToList(),
                    Additions = additions,
                    Removals = food.FoodIngredient.Select(ingredient => ingredient.IngredientNavigation).Except(target).ToList()
                };
            });

            Candidate Min(Candidate left, Candidate right)
            {
                var intersection = left.UnknownIngredients.Intersect(right.UnknownIngredients).ToList();

                var leftIsSubset = left.UnknownIngredients.Count == intersection.Count;
                var rightIsSubset = right.UnknownIngredients.Count == intersection.Count;

                if (left.Price == right.Price && leftIsSubset && rightIsSubset)
                {
                    var leftChanges = left.Additions.Count + left.Removals.Count;
                    var rightChanges = right.Additions.Count + right.Removals.Count;

                    if (leftChanges == rightChanges)
                    {
                        return null;
                    }

                    return leftChanges < rightChanges ? left : right;
                }

                if (left.Price <= right.Price && leftIsSubset)
                {
                    return left;
                }

                if (right.Price <= left.Price && rightIsSubset)
                {
                    return right;
                }

                return null;
            }

            var minimal = new List<Candidate>();

            foreach (var candidate in candidates)
            {
                minimal.RemoveAll(min => Min(min, candidate) == candidate);
                if (minimal.All(min => Min(min, candidate) != min))
                {
                    minimal.Add(candidate);
                }
            }

            return minimal
                .OrderBy(candidate => candidate.Price)
                .Select(candidate => new FoodOrder
                {
                    Food = candidate.Food.Id,
                    FoodNavigation = candidate.Food,
                    FoodOrderIngredient = candidate.Additions.Select(ingredient => new FoodOrderIngredient
                        {
                            Ingredient = ingredient.Id,
                            IngredientNavigation = ingredient,
                            Isremoval = false
                        })
                        .Concat(candidate.Removals.Select(ingredient => new FoodOrderIngredient
                        {
                            Ingredient = ingredient.Id,
                            IngredientNavigation = ingredient,
                            Isremoval = true
                        }))
                        .ToList()
                }).ToList();
        }
    }
}