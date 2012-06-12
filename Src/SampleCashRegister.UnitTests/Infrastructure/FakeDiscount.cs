using SampleCashRegister.Discounts;

namespace SampleCashRegister.UnitTests.Infrastructure
{
    public class FakeDiscount : Discount
    {
        public override void Apply(Order order)
        {
            return;
        }
    }
}
