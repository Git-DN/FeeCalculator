using System;
using System.Collections.Generic;
using FeeCalculator.Services.Files;
using FeeCalculator.Services.Config;
using FeeCalculator.Services.Calculator;
using FeeCalculator.Services.Transactions.Model;

namespace FeeCalculator.Services.Transactions
{
    public class TransactionService
    {
        private readonly FileService _fileService;
        private readonly CalculatorService _calculatorService;
        private readonly FeeCalculatorConfig _config;

        public event EventHandler<TransactionProcessedEventArgs> TransactionProcessed;

        public TransactionService(FileService fileService, CalculatorService calculatorService, FeeCalculatorConfig config)
        {
            _fileService = fileService;
            _calculatorService = calculatorService;
            _config = config;
        }
        
        public void ProcessTransactions(int singleReadLineCount)
        {
            List<Transaction> transactions;

            while ((transactions = _fileService.ReadTransactions(_config.FilePath, singleReadLineCount)).Count != 0)
            {
                foreach (var transaction in transactions)
                {
                    _calculatorService.Calculate(transaction);

                    TransactionProcessed?
                        .Invoke(this, new TransactionProcessedEventArgs(transaction.MerchantName, transaction.Date, transaction.Fee));
                }
            }
        }
    }
}