using KnapsackGenetic.Domain;
using KnapsackGenetic.Providers.Contracts;
using System;
using System.Collections.Generic;

namespace KnapsackGenetic.Providers
{
    public class InitialPopulationProvider : IInitialPopulationProvider
    {
        private readonly static Random random = new Random();

        public List<Individual> GetInitialPopulation(int populationSize, int numberOfGenes)
        {
            var initialPopulation = new List<Individual>();

            for (int i = 0; i < populationSize; i++)
                initialPopulation.Add(GetRandomIndividual(numberOfGenes));

            return initialPopulation;
        }

        private Individual GetRandomIndividual(int numberOfGenes)
        {
            var randomGenes = new bool[numberOfGenes];

            for (int i = 0; i < numberOfGenes; i++)
                randomGenes[i] = random.Next(2) % 2 == 1;

            return new Individual
            {
                Genes = randomGenes
            };
        }
    }
}
