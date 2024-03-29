﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using FeeCalculator.Services.Transactions.Model;

namespace FeeCalculator.Services.Files
{
    public class FileService
    {
        private int _linesToSkip = 0;

        public List<Transaction> ReadTransactions(string path, int linesToRead)
        {
            var transactions = new List<Transaction>();

            var lines = File.ReadLines(path)
                .Skip(_linesToSkip)
                .Take(linesToRead);

            foreach (var line in lines)
            {
                var lineData = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (lineData.Count() != 3)
                {
                    throw new InconsistentTransactionEntriesException($"lineData count: {lineData.Count()}, expected: 3", line);
                }

                transactions.Add(new Transaction()
                {
                    Date = DateTime.Parse(lineData[0]),
                    MerchantName = lineData[1],
                    Amount = decimal.Parse(lineData[2]),
                });
            }

            _linesToSkip += linesToRead;

            return transactions;
        }
    }
}