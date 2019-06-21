using System;
using FeeCalculator.Services.Calculator;
using FeeCalculator.Services.Config;
using FeeCalculator.Services.Transactions.Model;
using NUnit.Framework;

namespace FeeCalculator.Tests.Calculator
{
    [TestFixture]
    public class CalculatorServiceTests
    {
        [Test]
        public void GivenAMerchantNameReturnsARule()
        {
            // Arange
            var config = FeeCalculatorConfig.Instance;
            var service = new CalculatorService(config);

            // Act
            var ruleA = service.FindRule("MerchantNameString");
            var ruleB = service.FindRule("TELIA");

            // Assert
            Assert.NotNull(ruleA);
            Assert.NotNull(ruleB);
        }

        [Test]
        public void GivenATransactionCalculatesFees()
        {
            // Arange
            var config = FeeCalculatorConfig.Instance;
            var service = new CalculatorService(config);
            var transaction = new Transaction()
            {
                MerchantName = "MerchantNameString",
                Date = DateTime.Now,
                Amount = 100,
            };
            var rule = service.FindRule(transaction.MerchantName);
            var fee = ((transaction.Amount - (transaction.Amount / 100 * rule.Discount)) / 100 * rule.Fee) + rule.InvoiceFee;

            // Act
            service.Calculate(transaction);

            // Assert
            Assert.NotNull(transaction);
            Assert.IsTrue(transaction.Fee == fee);
        }
        
        [Test]
        public void GivenATwoTransactionsInTheSameMonthWithTheSameMerchantNameOnlyFirstTransactionWillContainInvoiceFee()
        {
            // Arrange
            var config = FeeCalculatorConfig.Instance;
            var service = new CalculatorService(config);

            var transactionA = new Transaction()
            {
                MerchantName = "MerchantNameString",
                Date = DateTime.Parse("2019-01-01"),
                Amount = 100,
            };

            var transactionB = new Transaction()
            {
                MerchantName = "MerchantNameString",
                Date = DateTime.Parse("2019-01-02"),
                Amount = 100,
            };

            var ruleA = service.FindRule(transactionA.MerchantName);
            var ruleB = service.FindRule(transactionB.MerchantName);

            var feeA = ((transactionA.Amount - (transactionA.Amount / 100 * ruleA.Discount)) / 100 * ruleA.Fee) + ruleA.InvoiceFee;
            var feeB = (transactionB.Amount - (transactionB.Amount / 100 * ruleB.Discount)) / 100 * ruleB.Fee;

            // Act
            service.Calculate(transactionA);
            service.Calculate(transactionB);

            // Assert
            Assert.NotNull(transactionA);
            Assert.NotNull(transactionB);
            Assert.IsTrue(transactionA.Fee == feeA);
            Assert.IsTrue(transactionB.Fee == feeB);
        }
    }
}