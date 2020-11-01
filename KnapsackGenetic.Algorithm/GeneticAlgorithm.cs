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

        private readonly List<Item> items;
        private readonly int weightLimit;
        private readonly int numberOfGenes;

        private List<Individual> currentPopulation;
        private List<Solution> currentSolutions;

        public GeneticAlgorithm(List<Item> items, int weightLimit, int initialPopulationSize,
            IInitialPopulationProvider initialPopulationProvider,
            IFitnessFunction fitnessFunction, 
            ISelectionOperator selectionOperator, 
            ISelectionOperator elitistSelection)
        {
            this.items = items;
            this.weightLimit = weightLimit;
            this.initialPopulationProvider = initialPopulationProvider;
            this.fitnessFunction = fitnessFunction;
            this.selectionOperator = selectionOperator;
            this.elitistSelection = elitistSelection;

            numberOfGenes = items.Count;
            currentPopulation = initialPopulationProvider.GetInitialPopulation(initialPopulationSize, numberOfGenes);
            currentSolutions = GetCurrentSolutions();
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
