using KnapsackGenetic.Domain;
using System.Collections.Generic;

namespace KnapsackGenetic.Tests.Common
{
    public static class Entities
    {
        public static Individual GetIndividual1()
        {
            return new Individual
            {
                Genes = GetGenes1()
            };
        }

        public static Individual GetIndividual2()
        {
            return new Individual
            {
                Genes = GetGenes2()
            };
        }

        public static Individual GetIndividual3()
        {
            return new Individual
            {
                Genes = GetGenes3()
            };
        }

        public static bool[] GetGenes1()
        {
            return new bool[] { false, false, true, true };
        }

        public static bool[] GetGenes2()
        {
            return new bool[] { false, true, true, false };
        }

        public static bool[] GetGenes3()
        {
            return new bool[] { true, true, true, false };
        }

        public static Settings GetSettings()
        {
            return new Settings
            {
                Items = GetItems(),
                NumberOfGenes = Constants.NumberOfGenes,
                WeightLimit = Constants.WeightLimit,
                NumberOfElites = Constants.NumberOfElites,
                InitialPopulationSize = Constants.InitialPopulationSize,
                CrossoverRate = Constants.CrossoverRate,
                MutationRate = Constants.MutationRate
            };
        }

        public static List<Item> GetItems()
        {
            return new List<Item>
            {
                GetItem1(),
                GetItem2(),
                GetItem3(),
                GetItem4()
            };
        }

        public static Item GetItem1()
        {
            return new Item
            {
                Weight = Constants.Weight1,
                Value = Constants.Value1,
            };
        }

        public static Item GetItem2()
        {
            return new Item
            {
                Weight = Constants.Weight2,
                Value = Constants.Value2,
            };
        }

        public static Item GetItem3()
        {
            return new Item
            {
                Weight = Constants.Weight3,
                Value = Constants.Value3,
            };
        }

        public static Item GetItem4()
        {
            return new Item
            {
                Weight = Constants.Weight4,
                Value = Constants.Value4,
            };
        }

        public static List<Solution> GetSolutions()
        {
            return new List<Solution>
            {
                GetSolution1(),
                GetSolution2(),
                GetSolution3()
            };
        }

        public static Solution GetSolution1()
        {
            return new Solution
            {
                Individual = GetIndividual1(),
                FitnessScore = Constants.FitnessScore1
            };
        }

        public static Solution GetSolution2()
        {
            return new Solution
            {
                Individual = GetIndividual2(),
                FitnessScore = Constants.FitnessScore2
            };
        }

        public static Solution GetSolution3()
        {
            return new Solution
            {
                Individual = GetIndividual3(),
                FitnessScore = Constants.FitnessScore3
            };
        }
    }
}
