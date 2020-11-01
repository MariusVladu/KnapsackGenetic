using KellermanSoftware.CompareNetObjects;
using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace KnapsackGenetic.Algorithm.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ElitistSelectionUnitTests
    {
        private ISelectionOperator elitistSelection;

        [TestInitialize]
        public void Setup()
        {
            elitistSelection = new ElitistSelection();
        }

        [TestMethod]
        public void TestThatSelectOneReturnsIndividualFromSolutionWithTheHighestScore()
        {
            var result = elitistSelection.SelectOne(Entities.GetSolutions());

            var comparer = new CompareLogic();
            Assert.IsTrue(comparer.Compare(Entities.GetIndividual2(), result).AreEqual);
        }
    }
}
