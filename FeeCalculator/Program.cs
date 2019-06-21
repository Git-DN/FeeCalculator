using System;
using FeeCalculator.Services.Files;
using FeeCalculator.Services.Config;
using FeeCalculator.Services.Calculator;
using FeeCalculator.Services.Transactions;

namespace FeeCalculator
{
    class Program
    {
        // In a real world app we would use DI Container to manage dependencies
        private static readonly TransactionService transactionService = 
            new TransactionService(
                new FileService(), new CalculatorService(FeeCalculatorConfig.Instance), FeeCalculatorConfig.Instance);

        static void Main()
        {
            transactionService.TransactionProcessed += TransactionProcessed;

            transactionService.ProcessTransactions(7);

            transactionService.TransactionProcessed -= TransactionProcessed;

            Console.ReadKey();
        }

        static void TransactionProcessed(object sender, TransactionProcessedEventArgs args)
            => Console.WriteLine($"{args.Date:yyyy-MM-dd} {string.Format("{0,-8} {1,5}", args.MerchantName, args.Fee.ToString("N2"))}");
    }
}