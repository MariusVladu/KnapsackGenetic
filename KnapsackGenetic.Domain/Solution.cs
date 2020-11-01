using System.Linq;

namespace KnapsackGenetic.Domain
{
    public class Solution
    {
        public Individual Individual { get; set; }
        public int FitnessScore { get; set; }

        public override string ToString()
        {
            return $"{string.Join( ", ", Individual.Genes.Select(x => x ? 1 : 0))} - Score = {FitnessScore}";
        }
    }
}
