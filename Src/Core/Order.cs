using System.Collections.Generic;
using System.Linq;
using SampleCashRegister.Core.Discounts;

namespace SampleCashRegister.Core
{
    public class Order
    {
        private readonly IList<OrderItem> _items = new List<OrderItem>();
        private readonly IList<Discount> _discounts = new List<Discount>();
        private readonly IList<Coupon> _coupons = new List<Coupon>();

        public IList<OrderItem> Items
        {
            get { return _items; }
        }

        public IList<Discount> Discounts
        {
            get { return _discounts; }
        }

        public IList<Coupon> Coupons
        {
            get { return _coupons; }
        }

        public void AddItem(OrderItem orderItem)
        {
            _items.Add(orderItem);
        }

        public decimal CalculateTotal()
        {
            var total = Items.Sum(i => i.Quantity*i.PricePerUnit);

            var applicableCoupon = GetApplicableCoupon(total);

            return
                applicableCoupon == null
                    ? total
                    : ApplyCoupon(applicableCoupon, total);
        }

        private decimal ApplyCoupon(Coupon applicableCoupon, decimal total)
        {
            applicableCoupon.HasBeenApplied = true;

            string adjustmentItem = string.Format(
                "Coupon ${0} off when the bill total (${1}) exceeds ${2}",
                applicableCoupon.AmountOff, total, applicableCoupon.Threshold);

            this.AddItem(adjustmentItem).Quantity(1)
                .PricePerUnit(-applicableCoupon.AmountOff);

            return total - applicableCoupon.AmountOff;
        }

        private Coupon GetApplicableCoupon(decimal total)
        {
            return
                NonAppliedCoupons
                    .OrderBy(c => c.Threshold)
                    .LastOrDefault(c => c.Threshold <= total);
        }

        public void ApplyDiscounts()
        {
            foreach (var discount in NonAppliedDiscounts)
            {
                discount.Apply(this);
                discount.HasBeenApplied = true;
            }
        }

        private IEnumerable<Discount> NonAppliedDiscounts
        {
            get { return _discounts.Where(d => !d.HasBeenApplied); }
        }

        private IEnumerable<Coupon> NonAppliedCoupons
        {
            get { return _coupons.Where(c => !c.HasBeenApplied); }
        }

        public void AddDiscount(Discount discount)
        {
            _discounts.Add(discount);
        }

        public void AddCoupon(Coupon coupon)
        {
            _coupons.Add(coupon);
        }
    }
}