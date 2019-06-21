using FeeCalculator.Services.Calculator;
using FeeCalculator.Services.Config;
using FeeCalculator.Services.Files;
using FeeCalculator.Services.Transactions;
using NUnit.Framework;

namespace FeeCalculator.Tests.Transactions
{
    [TestFixture]
    class TransactionServiceTests
    {
        [Test]
        public void ProovesThatProcessTransactionsFunctionFiresEventNamedTransactionProcessed()
        {
            // Arrange
            var fileService = new FileService();
            var transactionService = new TransactionService(
                new FileService(), new CalculatorService(FeeCalculatorConfig.Instance), FeeCalculatorConfig.Instance);

            // Act
            // Assert
            transactionService.TransactionProcessed += (s, e) => Assert.IsTrue(e.MerchantName != null);

            transactionService.ProcessTransactions(3);
        }
    }
}