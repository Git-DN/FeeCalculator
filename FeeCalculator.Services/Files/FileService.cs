using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using FeeCalculator.Services.Fee.Model;

namespace FeeCalculator.Services.Files
{
    public class FileService
    {
        public List<Transaction> ReadTransactions(string path, int linesToRead, int linesToSkip)
        {
            var transactions = new List<Transaction>();

            var lines = File.ReadLines(path)
                .Skip(linesToSkip)
                .Take(linesToRead);

            foreach (var line in lines)
            {
                var lineData = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (lineData.Count() != 3)
                {
                    throw new InconsistentTransactionEntriesException(line, line);
                }

                transactions.Add(new Transaction()
                {
                    Date = DateTime.Parse(lineData[0]),
                    MerchantName = lineData[1],
                    Amount = decimal.Parse(lineData[2]),
                });
            }

            return transactions;
        }
    }
}