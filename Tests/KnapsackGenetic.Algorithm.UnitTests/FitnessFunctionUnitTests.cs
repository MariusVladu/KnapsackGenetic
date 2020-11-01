using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace KnapsackGenetic.Algorithm.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FitnessFunctionUnitTests
    {
        private IFitnessFunction fitnessFunction;

        [TestInitialize]
        public void Setup()
        {
            fitnessFunction = new FitnessFunction();
        }

        [TestMethod]
        public void TestThatGetFitnessScoreReturnsSumOfItemValues()
        {
            var result = fitnessFunction.GetFitnessScore(Entities.GetIndividual1(), Constants.WeightLimit, Entities.GetItems());

            Assert.AreEqual(Constants.FitnessScore1, result);
        }

        [TestMethod]
        public void TestThatWhenTotalWeightIsGreaterThanWeightLimitGetFitnessScoreReturns0()
        {
            var result = fitnessFunction.GetFitnessScore(Entities.GetIndividual1(), 1, Entities.GetItems());

            Assert.AreEqual(0, result);
        }
    }
}
