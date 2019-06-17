using FeeCalculator.Services.Fee.Model;

namespace FeeCalculator.Services.Fee
{
    public class CircleKCalculator : BaseCalculator
    {
        public CircleKCalculator(Transaction transaction) : base(transaction) { }

        protected override void CalculateDiscount()
        {
            _transaction.Discount = _transaction.Amount / 100 * 20;
        }
    }
}