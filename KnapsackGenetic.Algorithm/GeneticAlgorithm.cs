using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using System;
using System.Collections.Generic;

namespace KnapsackGenetic.Algorithm
{
    public class GeneticAlgorithm : IGeneticAlgorithm
    {
        private readonly List<Item> items;
        private readonly int numberOfGenes;
        private readonly int initialPopulationSize;

        private List<Individual> currentPopulation;
        private readonly static Random random = new Random();

        public GeneticAlgorithm(List<Item> items, int initialPopulationSize)
        {
            this.items = items;
            this.initialPopulationSize = initialPopulationSize;

            numberOfGenes = items.Count;
            currentPopulation = GetInitialPopulation(initialPopulationSize);
        }

        public void PerformOneGeneration()
        {
            throw new NotImplementedException();
        }

        private List<Individual> GetInitialPopulation(int populationSize)
        {
            var initialPopulation = new List<Individual>();

            for (int i = 0; i < populationSize; i++)
                initialPopulation.Add(GetRandomIndividual());

            return initialPopulation;
        }

        private Individual GetRandomIndividual()
        {
            var randomGenes = new byte[numberOfGenes];

            for (int i = 0; i < numberOfGenes; i++)
                randomGenes[i] = (byte)random.Next(2);

            return new Individual
            {
                Genes = randomGenes
            };

        }
    }
}
