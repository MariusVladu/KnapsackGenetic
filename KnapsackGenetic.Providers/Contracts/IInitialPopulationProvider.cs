using KnapsackGenetic.Domain;
using System.Collections.Generic;

namespace KnapsackGenetic.Providers.Contracts
{
    public interface IInitialPopulationProvider
    {
        List<Individual> GetInitialPopulation(int populationSize, int numberOfGenes);
    }
}
