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
    }
}
