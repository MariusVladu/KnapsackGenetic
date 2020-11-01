using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using KnapsackGenetic.Providers.Contracts;
using System;
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
        private static readonly Random random = new Random();

        private readonly Settings settings;

        private List<Individual> currentPopulation;
        public List<Solution> CurrentSolutions;
        public int CurrentGenerationNumber;
        public double AverageScore;
        public Solution CurrentBestSolution;

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
            ComputeCurrentGenerationData();
        }

        public List<Solution> ComputeNextGeneration()
        {
            var nextGeneration = new List<Individual>();

            var numberOfCrossovers = CurrentSolutions.Count / 2;

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

            AddElites(nextGeneration);

            currentPopulation = nextGeneration;
            ComputeCurrentGenerationData();

            return CurrentSolutions;
        }

        private void ComputeCurrentGenerationData()
        {
            CurrentGenerationNumber++;
            CurrentSolutions = GetCurrentSolutions();
            ComputeAverageScore();
            ComputeCurrentBestSolution();
        }

        private void AddElites(List<Individual> nextGeneration)
        {
            for (int i = 0; i <= settings.NumberOfElites; i++)
            {
                var elite = elitistSelection.SelectOne(CurrentSolutions);

                nextGeneration.RemoveAt(random.Next(nextGeneration.Count));

                nextGeneration.Add(elite);
            }
        }

        private void ComputeAverageScore()
        {
            AverageScore = CurrentSolutions.Average(s => s.FitnessScore);
        }

        private void ComputeCurrentBestSolution()
        {
            CurrentBestSolution = CurrentSolutions.OrderByDescending(s => s.FitnessScore).First();
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
