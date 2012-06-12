namespace SampleCashRegister.Discounts
{
    public class AmountDiscount : SingleProductDiscount
    {
        public decimal AmountOff { get; set; }

        protected override OrderItem CreateAdjustmentItem(OrderItem item)
        {
            return
                new OrderItem
                    {
                        Product = item.Product + " $" + AmountOff + " off",
                        PricePerUnit = -AmountOff,
                        Quantity = item.Quantity,
                    };
        }
    }
}
