using System.Linq;

namespace SampleCashRegister.Discounts
{
    public abstract class SingleProductDiscount : Discount
    {
        public string Product { get; set; }

        public override void Apply(Order order)
        {
            var affectedItems = order.ItemsWithProduct(Product);
            var adjustmentItems = affectedItems.Select(CreateAdjustmentItem);

            foreach (var newItem in adjustmentItems)
            {
                order.AddItem(newItem);
            }
        }

        protected abstract OrderItem CreateAdjustmentItem(OrderItem item);
    }
}
