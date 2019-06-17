using FeeCalculator.Services.Fee.Model;

namespace FeeCalculator.Services.Fee
{
    public class DefaultMerchantCalculator : BaseCalculator
    {
        public DefaultMerchantCalculator(Transaction transaction) : base(transaction) { }
    }
}