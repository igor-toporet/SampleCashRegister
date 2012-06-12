namespace SampleCashRegister.Discounts
{
    public abstract class Discount
    {
        public abstract void Apply(Order order);

        public bool HasBeenApplied { get; set; }
    }
}
