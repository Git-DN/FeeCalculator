using FeeCalculator.Services.Config;
using NUnit.Framework;
using System.Linq;

namespace FeeCalculator.Tests.Config
{
    [TestFixture]
    public class FeeCalculatorConfigTests
    {
        [Test]
        public void ProovesThatThereAreAlwaysOnlyOneAndTheSameInstanceOfFeeCalculatorConfigInMemory()
        {
            // Arange
            var configA = FeeCalculatorConfig.Instance;
            var configB = FeeCalculatorConfig.Instance;

            // Assert
            Assert.IsTrue(ReferenceEquals(configA, configB));
        }

        [Test]
        public void ReadsConfigurationFile()
        {
            // Arrange
            var config = FeeCalculatorConfig.Instance;

            // Assert
            Assert.IsTrue(config.FilePath.Equals("transactions.txt"));
            Assert.IsTrue(config.Rules.Any());
        }
    }
}