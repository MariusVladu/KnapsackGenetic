using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using KnapsackGenetic.Providers.Contracts;
using System.Collections.Generic;

namespace KnapsackGenetic.Algorithm
{
    public class GeneticAlgorithm
    {
        private readonly IInitialPopulationProvider initialPopulationProvider;
        private readonly IFitnessFunction fitnessFunction;
        private readonly ISelectionOperator selectionOperator;
        private readonly ISelectionOperator elitistSelection;
        private readonly ICrossoverOperator crossoverOperator;

        private readonly List<Item> items;
        private readonly int weightLimit;
        private readonly int numberOfGenes;
        private readonly double crossoverRate;

        private List<Individual> currentPopulation;
        private List<Solution> currentSolutions;

        public GeneticAlgorithm(List<Item> items, int weightLimit, double crossoverRate, int initialPopulationSize,
            IInitialPopulationProvider initialPopulationProvider,
            IFitnessFunction fitnessFunction,
            ISelectionOperator selectionOperator,
            ISelectionOperator elitistSelection, 
            ICrossoverOperator crossoverOperator)
        {
            this.items = items;
            this.weightLimit = weightLimit;
            this.crossoverRate = crossoverRate;
            this.initialPopulationProvider = initialPopulationProvider;
            this.fitnessFunction = fitnessFunction;
            this.selectionOperator = selectionOperator;
            this.elitistSelection = elitistSelection;
            this.crossoverOperator = crossoverOperator;

            numberOfGenes = items.Count;
            currentPopulation = initialPopulationProvider.GetInitialPopulation(initialPopulationSize, numberOfGenes);
            currentSolutions = GetCurrentSolutions();
        }

        public List<Solution> ComputeNextGeneration()
        {
            var numberOfCrossovers = currentSolutions.Count / 2 - 2;

            for (int i = 0; i < numberOfCrossovers; i++)
            {
                var parent1 = selectionOperator.SelectOne(currentSolutions);
                RemoveFromCurrentSolutions(parent1);

                var parent2 = selectionOperator.SelectOne(currentSolutions);
                RemoveFromCurrentSolutions(parent2);

                var offsprings = crossoverOperator.GetOffsprings(parent1, parent2, crossoverRate);

            }
        }

        private void RemoveFromCurrentSolutions(Individual individual)
        {
            var indexToRemove = currentSolutions.FindIndex(s => s.Individual == individual);

            currentSolutions.RemoveAt(indexToRemove);
        }

        private List<Solution> GetCurrentSolutions()
        {
            var solutions = new List<Solution>();
            foreach (var individual in currentPopulation)
                solutions.Add(new Solution
                {
                    Individual = individual,
                    FitnessScore = fitnessFunction.GetFitnessScore(individual, weightLimit, items)
                });

            return solutions;
        }
    }
}
