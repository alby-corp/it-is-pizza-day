using System.Collections.Generic;

namespace ItIsPizzaDay.Shared.Models
{
    public class Report
    {
        public Report(Food food, IEnumerable<string> removals, IEnumerable<string> supplements, int count)
        {
            Food = food;
            Removals = removals;
            Supplements = supplements;
            Count = count;
        }

        public Food Food { get; }
        public IEnumerable<string> Supplements { get; }
        public IEnumerable<string> Removals { get; }
        public int Count { get; }
    }
}