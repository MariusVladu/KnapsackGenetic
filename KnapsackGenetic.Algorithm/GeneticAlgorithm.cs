using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using KnapsackGenetic.Providers.Contracts;
using System.Collections.Generic;

namespace KnapsackGenetic.Algorithm
{
    public class GeneticAlgorithm
    {
        private readonly IFitnessFunction fitnessFunction;
        private readonly ISelectionOperator selectionOperator;
        private readonly ISelectionOperator elitistSelection;
        private readonly ICrossoverOperator crossoverOperator;
        private readonly IMutationOperator mutationOperator;

        private readonly Settings settings;

        private List<Individual> currentPopulation;
        private List<Solution> currentSolutions;

        public GeneticAlgorithm(Settings settings,
            IInitialPopulationProvider initialPopulationProvider,
            IFitnessFunction fitnessFunction,
            ISelectionOperator selectionOperator,
            ISelectionOperator elitistSelection,
            ICrossoverOperator crossoverOperator,
            IMutationOperator mutationOperator)
        {
            this.settings = settings;
            this.fitnessFunction = fitnessFunction;
            this.selectionOperator = selectionOperator;
            this.elitistSelection = elitistSelection;
            this.crossoverOperator = crossoverOperator;
            this.mutationOperator = mutationOperator;

            currentPopulation = initialPopulationProvider.GetInitialPopulation(settings.InitialPopulationSize, settings.NumberOfGenes);
            currentSolutions = GetCurrentSolutions();
        }

        public List<Solution> ComputeNextGeneration()
        {
            var nextGeneration = new List<Individual>();

            var numberOfCrossovers = currentSolutions.Count / 2 - settings.NumberOfElites;

            for (int i = 0; i < numberOfCrossovers; i++)
            {
                var parent1 = selectionOperator.SelectOne(currentSolutions);
                RemoveFromCurrentSolutions(parent1);

                var parent2 = selectionOperator.SelectOne(currentSolutions);
                RemoveFromCurrentSolutions(parent2);

                var offsprings = crossoverOperator.GetOffsprings(parent1, parent2, settings.CrossoverRate);

                mutationOperator.ApplyMutation(offsprings.Item1, settings.MutationRate);
                mutationOperator.ApplyMutation(offsprings.Item2, settings.MutationRate);

                nextGeneration.Add(offsprings.Item1);
                nextGeneration.Add(offsprings.Item2);
            }

            AddFirstTwoRemainingElites(nextGeneration);

            currentPopulation = nextGeneration;
            currentSolutions = GetCurrentSolutions();

            return currentSolutions;
        }

        private void AddFirstTwoRemainingElites(List<Individual> nextGeneration)
        {
            for (int i = 0; i < settings.NumberOfElites; i++)
            {
                var elite = elitistSelection.SelectOne(currentSolutions);
                RemoveFromCurrentSolutions(elite);

                nextGeneration.Add(elite);
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
                    FitnessScore = fitnessFunction.GetFitnessScore(individual, settings.WeightLimit, settings.Items)
                });

            return solutions;
        }
    }
}
