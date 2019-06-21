using System;

namespace FeeCalculator.Services.Transactions.Model
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string MerchantName { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
    }
}