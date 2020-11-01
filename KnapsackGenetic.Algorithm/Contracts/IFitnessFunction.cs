using KnapsackGenetic.Domain;
using System.Collections.Generic;

namespace KnapsackGenetic.Algorithm.Contracts
{
    public interface IFitnessFunction
    {
        int GetFitnessScore(Individual individual, int weightLimit, List<Item> items);
    }
}
