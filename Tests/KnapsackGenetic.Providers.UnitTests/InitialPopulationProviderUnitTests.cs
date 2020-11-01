using KnapsackGenetic.Providers.Contracts;
using KnapsackGenetic.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace KnapsackGenetic.Providers.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class InitialPopulationProviderUnitTests
    {
        private IInitialPopulationProvider initialPopulationProvider;

        [TestInitialize]
        public void Setup()
        {
            initialPopulationProvider = new InitialPopulationProvider();
        }

        [TestMethod]
        public void TestThatGetInitialPopulationReturnsNotNullObject()
        {
            var result = initialPopulationProvider.GetInitialPopulation(Constants.InitialPopulationSize, Constants.NumberOfGenes);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestThatGetInitialPopulationReturnsListOfExpectedLength()
        {
            const int populationSize = 10;

            var result = initialPopulationProvider.GetInitialPopulation(populationSize, Constants.NumberOfGenes);

            Assert.AreEqual(populationSize, result.Count);
        }

        [TestMethod]
        public void TestThatGetInitialPopulationReturnsAllIndividualsWithCorrectNumberOfGenes()
        {
            const int numberOfGenes = 4;

            var result = initialPopulationProvider.GetInitialPopulation(Constants.InitialPopulationSize, numberOfGenes);

            Assert.IsTrue(result.All(x => x.Genes.Length == numberOfGenes));
        }
    }
}
