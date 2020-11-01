using KellermanSoftware.CompareNetObjects;
using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using KnapsackGenetic.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace KnapsackGenetic.Algorithm.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CrossoverOperatorUnitTests
    {
        private ICrossoverOperator crossoverOperator;

        [TestInitialize]
        public void Setup()
        {
            crossoverOperator = new CrossoverOperator();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestThatWhenParent1IsNullGetOffspringsThrowsArgumentException()
        {
            crossoverOperator.GetOffsprings(null, Entities.GetIndividual2(), Constants.CrossoverRate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestThatWhenParent1HasNullGenesGetOffspringsThrowsArgumentException()
        {
            crossoverOperator.GetOffsprings(new Individual(), Entities.GetIndividual2(), Constants.CrossoverRate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestThatWhenParent2IsNullGetOffspringsThrowsArgumentException()
        {
            crossoverOperator.GetOffsprings(Entities.GetIndividual1(), null, Constants.CrossoverRate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestThatWhenParent2HasNullGenesGetOffspringsThrowsArgumentException()
        {
            crossoverOperator.GetOffsprings(Entities.GetIndividual1(), new Individual(), Constants.CrossoverRate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenCrossoverRateIsLowerThan0GetOffspringsThrowsArgumentException()
        {
            crossoverOperator.GetOffsprings(Entities.GetIndividual1(), Entities.GetIndividual2(), -0.1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenCrossoverRateIsGreaterThan1GetOffspringsThrowsArgumentException()
        {
            crossoverOperator.GetOffsprings(Entities.GetIndividual1(), Entities.GetIndividual2(), 1.1);
        }

        [TestMethod]
        public void TestThatGetOffspringsSwapsParentsGenesAtHalf()
        {
            const double crossoverRate = 0.5;
            var parent1 = new Individual { Genes = new bool[] { true, true, true, true } };
            var parent2 = new Individual { Genes = new bool[] { false, false, false, false } };
            var expectedOffspring1 = new Individual { Genes = new bool[] { true, true, false, false } };
            var expectedOffspring2 = new Individual { Genes = new bool[] { false, false, true, true } };

            var result = crossoverOperator.GetOffsprings(parent1, parent2, crossoverRate);

            var comparer = new CompareLogic();
            Assert.IsTrue(comparer.Compare(expectedOffspring1, result.Item1).AreEqual);
            Assert.IsTrue(comparer.Compare(expectedOffspring2, result.Item2).AreEqual);
        }

        [TestMethod]
        public void TestThatGetOffspringsSwapsParentsGenesFirstQuarter()
        {
            const double crossoverRate = 0.25;
            var parent1 = new Individual { Genes = new bool[] { true, true, true, true } };
            var parent2 = new Individual { Genes = new bool[] { false, false, false, false } };
            var expectedOffspring1 = new Individual { Genes = new bool[] { true, false, false, false } };
            var expectedOffspring2 = new Individual { Genes = new bool[] { false, true, true, true } };

            var result = crossoverOperator.GetOffsprings(parent1, parent2, crossoverRate);

            var comparer = new CompareLogic();
            Assert.IsTrue(comparer.Compare(expectedOffspring1, result.Item1).AreEqual);
            Assert.IsTrue(comparer.Compare(expectedOffspring2, result.Item2).AreEqual);
        }

        [TestMethod]
        public void TestThatGetOffspringsSwapsParentsGenesThirdQuarter()
        {
            const double crossoverRate = 0.75;
            var parent1 = new Individual { Genes = new bool[] { true, true, true, true } };
            var parent2 = new Individual { Genes = new bool[] { false, false, false, false } };
            var expectedOffspring1 = new Individual { Genes = new bool[] { true, true, true, false } };
            var expectedOffspring2 = new Individual { Genes = new bool[] { false, false, false, true } };

            var result = crossoverOperator.GetOffsprings(parent1, parent2, crossoverRate);

            var comparer = new CompareLogic();
            Assert.IsTrue(comparer.Compare(expectedOffspring1, result.Item1).AreEqual);
            Assert.IsTrue(comparer.Compare(expectedOffspring2, result.Item2).AreEqual);
        }

        [TestMethod]
        public void TestThatWhenCrossoverRateIs1GetOffspringsReturnsParents()
        {
            const double crossoverRate = 1;
            var parent1 = new Individual { Genes = new bool[] { true, true, true, true } };
            var parent2 = new Individual { Genes = new bool[] { false, false, false, false } };

            var result = crossoverOperator.GetOffsprings(parent1, parent2, crossoverRate);

            var comparer = new CompareLogic();
            Assert.IsTrue(comparer.Compare(parent1, result.Item1).AreEqual);
            Assert.IsTrue(comparer.Compare(parent2, result.Item2).AreEqual);
        }

        [TestMethod]
        public void TestThatWhenCrossoverRateIs0GetOffspringsReturnsParentsSwapped()
        {
            const double crossoverRate = 0;
            var parent1 = new Individual { Genes = new bool[] { true, true, true, true } };
            var parent2 = new Individual { Genes = new bool[] { false, false, false, false } };

            var result = crossoverOperator.GetOffsprings(parent1, parent2, crossoverRate);

            var comparer = new CompareLogic();
            Assert.IsTrue(comparer.Compare(parent2, result.Item1).AreEqual);
            Assert.IsTrue(comparer.Compare(parent1, result.Item2).AreEqual);
        }
    }
}
