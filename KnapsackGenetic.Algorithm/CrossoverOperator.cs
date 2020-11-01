using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using System;

namespace KnapsackGenetic.Algorithm
{
    public class CrossoverOperator : ICrossoverOperator
    {
        public Tuple<Individual, Individual> GetOffsprings(Individual parent1, Individual parent2, double crossoverRate)
        {
            var numberOfGenes = parent1.Genes.Length;
            var splitIndex = (int)(numberOfGenes * crossoverRate);

            var offspring1 = new Individual { Genes = new byte[numberOfGenes] };
            var offspring2 = new Individual { Genes = new byte[numberOfGenes] };

            for (int i = 0; i < numberOfGenes; i++)
            {
                if(i <= splitIndex)
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
    }
}
