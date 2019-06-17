using System;

namespace FeeCalculator.Services.Fee.Model
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string MerchantName { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal InvoiceFee { get; set; }
        public decimal Fee { get; set; }
    }
}