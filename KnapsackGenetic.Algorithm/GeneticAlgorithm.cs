using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using KnapsackGenetic.Providers.Contracts;
using System.Collections.Generic;
using System.Linq;

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
        public List<Solution> CurrentSolutions;
        public int CurrentGenerationNumber;
        public double AverageScore;

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
            CurrentSolutions = GetCurrentSolutions();
            CurrentGenerationNumber = 1;
            ComputeAverageScore();
        }

        public List<Solution> ComputeNextGeneration()
        {
            var nextGeneration = new List<Individual>();

            var numberOfCrossovers = CurrentSolutions.Count / 2 - settings.NumberOfElites;

            for (int i = 0; i < numberOfCrossovers; i++)
            {
                var parent1 = selectionOperator.SelectOne(CurrentSolutions);
                var parent2 = selectionOperator.SelectOne(CurrentSolutions);

                var offsprings = crossoverOperator.GetOffsprings(parent1, parent2, settings.CrossoverRate);

                mutationOperator.ApplyMutation(offsprings.Item1, settings.MutationRate);
                mutationOperator.ApplyMutation(offsprings.Item2, settings.MutationRate);

                nextGeneration.Add(offsprings.Item1);
                nextGeneration.Add(offsprings.Item2);
            }

            AddRemainingElites(nextGeneration);

            currentPopulation = nextGeneration;
            CurrentSolutions = GetCurrentSolutions();
            CurrentGenerationNumber++;
            ComputeAverageScore();

            return CurrentSolutions;
        }

        private void AddRemainingElites(List<Individual> nextGeneration)
        {
            for (int i = 0; i <= settings.NumberOfElites; i++)
            {
                var elite = elitistSelection.SelectOne(CurrentSolutions);

                nextGeneration.Add(elite);
            }
        }

        private void ComputeAverageScore()
        {
            AverageScore = CurrentSolutions.Average(s => s.FitnessScore);
        }

        private void RemoveFromCurrentSolutions(Individual individual)
        {
            var indexToRemove = CurrentSolutions.FindIndex(s => s.Individual == individual);

            CurrentSolutions.RemoveAt(indexToRemove);
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
