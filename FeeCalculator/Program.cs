using System;
using System.Linq;
using System.Collections.Generic;
using FeeCalculator.Services.Files;
using FeeCalculator.Services.Fee;
using FeeCalculator.Services.Fee.Model;

namespace FeeCalculator
{
    class Program
    {
        static readonly FileService fileService = new FileService();

        static void Main()
        {
            var processed = new List<Transaction>();

            var transactions = fileService.ReadTransactions("transactions.txt", 5, processed.Count);

            while (transactions.Count != 0)
            {
                foreach (var transaction in transactions)
                {
                    var calculator = default(BaseCalculator);

                    switch (transaction.MerchantName)
                    {
                        case "TELIA":
                            calculator = new TeliaCalculator(transaction);
                            break;
                        case "CIRCLE_K":
                            calculator = new CircleKCalculator(transaction);
                            break;
                        default:
                            calculator = new DefaultMerchantCalculator(transaction);
                            break;
                    }

                    var firstInMonthWithFee = processed.FirstOrDefault(c => c.MerchantName.Equals(transaction.MerchantName, StringComparison.Ordinal)
                        && c.Date.Year == transaction.Date.Year
                        && c.Date.Month == transaction.Date.Month
                        && c.Fee != 0);

                    calculator.Calculate(firstInMonthWithFee == null || (firstInMonthWithFee != null && firstInMonthWithFee.InvoiceFee == 0));

                    Console.WriteLine($"{transaction.Date:yyyy-MM-dd} {string.Format("{0,-8} {1,5}", transaction.MerchantName, (transaction.Fee + transaction.InvoiceFee).ToString("N2"))}");

                    processed.Add(transaction);
                }

                transactions = fileService.ReadTransactions("transactions.txt", 5, processed.Count);
            }

            Console.ReadKey();
        }
    }
}