using SampleCashRegister.Discounts;
using SampleCashRegister.UnitTests.Infrastructure;
using Xunit;

namespace SampleCashRegister.UnitTests
{
    public class GeneralDiscountTests:BaseDiscountTestFixture
    {
        [Fact]
        public void UnableToApplySameDiscountsTwice()
        {
            // arrange
            var pepsiTenPercentOff = new PercentDiscount { PercentOff = 10m, Product = Pepsi };
            _order.AddDiscount(pepsiTenPercentOff);

            // act
            _order.ApplyDiscounts();
            _order.ApplyDiscounts();

            // assert
            var total = _order.CalculateTotal();
            Assert.Equal(0.9m, total);
        }
    }
}
