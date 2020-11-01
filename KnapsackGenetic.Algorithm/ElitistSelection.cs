using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using System.Collections.Generic;
using System.Linq;

namespace KnapsackGenetic.Algorithm
{
    public class ElitistSelection : ISelectionOperator
    {
        public Individual SelectOne(List<Solution> solutions)
        {
            return solutions.OrderByDescending(s => s.FitnessScore).First().Individual;
        }
    }
}
