namespace ItIsPizzaDay.Shared.Models
 {
     using System;
     using System.Collections.Generic;
     using System.Linq;
 
     public class FoodOrder : Entity
     {
         public Guid? Food { get; set; }
         public Guid? Order { get; set; }
         public Food FoodNavigation { get; set; }
         public Order OrderNavigation { get; set; }
         public ICollection<FoodOrderIngredient> FoodOrderIngredient { get; set; } = new List<FoodOrderIngredient>();
     }
 
     public static class FoodOrderExtensions
     {
         public static ICollection<string> Ingredients(this FoodOrder foodOrder)
         {
             return foodOrder.FoodNavigation.FoodIngredient.Select(fi => fi.IngredientNavigation.Name)
                 .Except(foodOrder.FoodOrderIngredient.Where(foi => foi.Isremoval).Select(foi => foi.IngredientNavigation.Name))
                 .Concat(foodOrder.FoodOrderIngredient.Where(foi => !foi.Isremoval).Select(foi => foi.IngredientNavigation.Name))
                 .ToList();
         }
 
         public static decimal Price(this FoodOrder foodOrder)
         {
             return foodOrder.FoodOrderIngredient.Where(i => !i.Isremoval).Sum(i => i.IngredientNavigation.Price ?? 0) + foodOrder.FoodNavigation.Price;
         }
     }
 }