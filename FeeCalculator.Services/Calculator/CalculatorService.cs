using System;
using System.Linq;
using System.Collections.Generic;
using FeeCalculator.Services.Config;
using FeeCalculator.Services.Transactions.Model;

namespace FeeCalculator.Services.Calculator
{
    public class CalculatorService
    {
        private readonly FeeCalculatorConfig _config;

        private readonly List<Transaction> transactionsWithInvoiceFee = new List<Transaction>();
        
        public CalculatorService(FeeCalculatorConfig config)
        {
            _config = config;
        }
        
        public void Calculate(Transaction transaction)
        {
            var rule = FindRule(transaction.MerchantName);

            transaction.Fee = (transaction.Amount - (transaction.Amount / 100 * rule.Discount)) / 100 * rule.Fee;

            if (transaction.Fee > 0)
            {
                if (transactionsWithInvoiceFee.Any(c => c.MerchantName.Equals(transaction.MerchantName, StringComparison.Ordinal) 
                && c.Date.Year == transaction.Date.Year 
                && c.Date.Month == transaction.Date.Month))
                {
                    return;
                };

                transaction.Fee += rule.InvoiceFee;

                transactionsWithInvoiceFee.Add(transaction);
            }
        }

        public RuleConfig FindRule(string MerchantName)
        {
            return _config.Rules.ContainsKey(MerchantName)
                ? _config.Rules[MerchantName]
                : _config.Rules["Default"];
        }
    }
}