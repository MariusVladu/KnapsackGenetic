using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using System.Collections.Generic;
using System.Linq;

namespace KnapsackGenetic.Algorithm
{
    public class ElitistSelection : IElitistSelection
    {
        public List<Solution> SelectMany(int n, List<Solution> solutions)
        {
            return solutions.OrderByDescending(s => s.FitnessScore).Take(n).ToList();
        }

        Solution IElitistSelection.SelectOne(List<Solution> solutions)
        {
            return solutions.OrderByDescending(s => s.FitnessScore).First();
        }
    }
}
