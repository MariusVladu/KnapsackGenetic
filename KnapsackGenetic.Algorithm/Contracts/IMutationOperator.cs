using KnapsackGenetic.Domain;

namespace KnapsackGenetic.Algorithm.Contracts
{
    public interface IMutationOperator
    {
        void ApplyMutation(Individual individual, double mutationRate);
    }
}
