using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using System.Collections.Generic;

namespace KnapsackGenetic.Algorithm
{
    public class FitnessFunction : IFitnessFunction
    {
        public int GetFitnessScore(Individual individual, int weightLimit, List<Item> items)
        {
            var score = 0;
            var totalWeight = 0;
            
            for (int i = 0; i < items.Count; i++)
            {
                if (individual.Genes[i] == 1)
                {
                    score += items[i].Value;
                    totalWeight += items[i].Weight;
                }
            }

            return totalWeight <= weightLimit
                ? score
                : 0;
        }
    }
}
