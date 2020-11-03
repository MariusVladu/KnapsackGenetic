using KnapsackGenetic.Domain;
using System.Collections.Generic;
using System.Linq;

namespace KnapsackGenetic.Greedy
{
    public class KnapsackGreedy
    {
        public bool[] Solve(List<Item> items, int maxWeight)
        {
            var itemsOrderedByValueWeightRatio = items
                .OrderByDescending(x => (double)x.Value / x.Weight)
                .ToList();

            var solution = new bool[items.Count];
            var currentWeight = 0;

            foreach (var item in itemsOrderedByValueWeightRatio)
            {
                if(currentWeight + item.Weight <= maxWeight)
                {
                    var index = items.IndexOf(item);
                    solution[index] = true;

                    currentWeight += item.Weight;
                }
            }

            return solution;
        }
    }
}
