using FeeCalculator.Services.Config;
using FeeCalculator.Services.Files;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace FeeCalculator.Tests.Files
{
    [TestFixture]
    public class FileServiceTests
    {
        [Test]
        public void GivenAValidFileNameReturnsTransactions()
        {
            // Arange
            var config = FeeCalculatorConfig.Instance;
            var service = new FileService();

            // Act
            var transactions = service.ReadTransactions(config.FilePath, 7);

            // Assert
            Assert.IsNotNull(transactions);
            Assert.IsTrue(transactions.Any());
        }
        
        [Test]
        public void GivenInvalidFileNameThrowsFileNotFoundException()
        {
            // Arange
            var service = new FileService();

            // Assert
            Assert.Throws<FileNotFoundException>(() => service.ReadTransactions("FileNameString", 7));
        }
    }
}