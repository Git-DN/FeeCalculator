using FeeCalculator.Services.Fee.Model;

namespace FeeCalculator.Services.Fee
{
    public class TeliaCalculator : BaseCalculator
    {
        public TeliaCalculator(Transaction transaction) : base(transaction) { }

        protected override void CalculateDiscount()
        {
            _transaction.Discount = _transaction.Amount / 100 * 10;
        }
    }
}