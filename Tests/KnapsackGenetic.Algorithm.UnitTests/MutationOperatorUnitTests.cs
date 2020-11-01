using KellermanSoftware.CompareNetObjects;
using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace KnapsackGenetic.Algorithm.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MutationOperatorUnitTests
    {
        private IMutationOperator mutationOperator;

        private CompareLogic comparer;

        [TestInitialize]
        public void Setup()
        {
            mutationOperator = new MutationOperator();

            comparer = new CompareLogic();
        }

        [TestMethod]
        public void TestThatWhenMutationRateIs1AllGenesAreFlipped()
        {
            var individual = new Individual { Genes = new bool[] { false, true, false, true } };
            var expectedIndividual = new Individual { Genes = new bool[] { true, false, true, false } };

            mutationOperator.ApplyMutation(individual, 1);

            Assert.IsTrue(comparer.Compare(expectedIndividual, individual).AreEqual);
        }

        [TestMethod]
        public void TestThatWhenMutationRateIs0AllGenesAreFlipped()
        {
            var individual = new Individual { Genes = new bool[] { false, true, false, true } };
            var expectedIndividual = new Individual { Genes = new bool[] { false, true, false, true } };

            mutationOperator.ApplyMutation(individual, 0);

            Assert.IsTrue(comparer.Compare(expectedIndividual, individual).AreEqual);
        }
    }
}
