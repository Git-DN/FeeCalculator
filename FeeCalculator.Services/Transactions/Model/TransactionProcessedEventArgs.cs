using System;

namespace FeeCalculator.Services.Transactions
{
    public class TransactionProcessedEventArgs : EventArgs
    {
        public TransactionProcessedEventArgs(string merchantName, DateTime date, decimal fee)
        {
            MerchantName = merchantName;
            Date = date;
            Fee = fee;
        }

        public string MerchantName { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Fee { get; private set; }
    }
}