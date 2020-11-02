using KnapsackGenetic.Domain;
using System.Collections.Generic;

namespace KnapsackGenetic.Algorithm.Contracts
{
    public interface IElitistSelection
    {
        Solution SelectOne(List<Solution> solutions);
        List<Solution> SelectMany(int n, List<Solution> solutions);
    }
}
