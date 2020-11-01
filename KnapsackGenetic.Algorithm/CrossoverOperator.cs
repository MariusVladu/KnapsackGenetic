using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using System;

namespace KnapsackGenetic.Algorithm
{
    public class CrossoverOperator : ICrossoverOperator
    {
        public Tuple<Individual, Individual> GetOffsprings(Individual parent1, Individual parent2, double crossoverRate)
        {
            ValidateParameters(parent1, parent2, crossoverRate);

            var numberOfGenes = parent1.Genes.Length;
            var splitIndex = (int)(numberOfGenes * crossoverRate);

            var offspring1 = new Individual { Genes = new bool[numberOfGenes] };
            var offspring2 = new Individual { Genes = new bool[numberOfGenes] };

            for (int i = 0; i < numberOfGenes; i++)
            {
                if(i < splitIndex)
                {
                    offspring1.Genes[i] = parent1.Genes[i];
                    offspring2.Genes[i] = parent2.Genes[i];
                }
                else
                {
                    offspring1.Genes[i] = parent2.Genes[i];
                    offspring2.Genes[i] = parent1.Genes[i];
                }
            }

            return new Tuple<Individual, Individual>(offspring1, offspring2);
        }

        private void ValidateParameters(Individual parent1, Individual parent2, double crossoverRate)
        {
            if (parent1 == null) throw new ArgumentNullException(nameof(parent1));
            if (parent1.Genes == null) throw new ArgumentNullException(nameof(parent1.Genes));
            if (parent2 == null) throw new ArgumentNullException(nameof(parent2));
            if (parent2.Genes == null) throw new ArgumentNullException(nameof(parent2.Genes));
            if (crossoverRate < 0 || crossoverRate > 1) throw new ArgumentException($"{nameof(crossoverRate)} must be in [0, 1]");
        }
    }
}
