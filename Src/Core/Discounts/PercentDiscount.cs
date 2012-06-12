namespace SampleCashRegister.Core.Discounts
{
    public class PercentDiscount : SingleProductDiscount
    {
        public decimal PercentOff { get; set; }

        protected override OrderItem CreateAdjustmentItem(OrderItem item)
        {
            return
                new OrderItem
                    {
                        Product = item.Product + " " + PercentOff + "% off",
                        PricePerUnit = -item.PricePerUnit*PercentOff/100,
                        Quantity = item.Quantity,
                    };
        }
    }
}