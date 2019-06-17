using FeeCalculator.Services.Fee.Model;

namespace FeeCalculator.Services.Fee
{
    public abstract class BaseCalculator
    {
        protected Transaction _transaction;

        public BaseCalculator(Transaction transaction)
        {
            _transaction = transaction;
        }

        public void Calculate(bool applyInvoiceFee)
        {
            CalculateDiscount();
            CalculateExpenseFee();

            if (applyInvoiceFee)
            {
                ApplyInvoiceFee();
            }
        }

        protected virtual void CalculateDiscount()
        {
        }

        protected virtual void CalculateExpenseFee()
        {
            _transaction.Fee = (_transaction.Amount - _transaction.Discount) / 100 * 1;
        }

        protected virtual void ApplyInvoiceFee()
        {
            _transaction.InvoiceFee = 29;
        }
    }
}