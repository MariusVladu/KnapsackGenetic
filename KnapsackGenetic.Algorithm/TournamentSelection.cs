using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnapsackGenetic.Algorithm
{
    public class TournamentSelection : ISelectionOperator
    {
        private static readonly Random random = new Random();
        private readonly int tournamentSize;

        public TournamentSelection(int tournamentSize)
        {
            this.tournamentSize = tournamentSize;
        }

        public Individual SelectOne(List<Solution> solutions)
        {
            var selectedForTournament = new List<Solution>(tournamentSize);

            while (selectedForTournament.Count < tournamentSize)
            {
                var randomSolution = solutions[random.Next(solutions.Count)];

                if (!selectedForTournament.Any(x => x.Equals(randomSolution)))
                    selectedForTournament.Add(randomSolution);
            }

            return selectedForTournament.OrderByDescending(x => x.FitnessScore).First().Individual;
        }
    }
}
