using SampleCashRegister.Core;
using SampleCashRegister.Core.Discounts;

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