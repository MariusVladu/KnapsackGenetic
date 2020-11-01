using KnapsackGenetic.Domain;
using System.Collections.Generic;

namespace KnapsackGenetic.Algorithm.Contracts
{
    public interface ISelectionOperator
    {
        Individual SelectOne(List<Solution> solutions);
    }
}
