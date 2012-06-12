using SampleCashRegister.Discounts;
using SampleCashRegister.UnitTests.Infrastructure;
using Xunit;

namespace SampleCashRegister.UnitTests
{
    public class SimpleDiscountTests : BaseDiscountTestFixture
    {
        [Fact]
        public void CanMakeTenPercentDiscountOnPepsi()
        {
            // arrange
            var pepsiTenPercentOff = new PercentDiscount {PercentOff = 10m, Product = Pepsi};
            _order.AddDiscount(pepsiTenPercentOff);
            _order.ApplyDiscounts();

            // act
            var total = _order.CalculateTotal();

            // assert
            Assert.Equal(0.9m, total);
        }

        [Fact]
        public void CanMakeFiveCentsDiscountOnPepsi()
        {
            // arrange
            var pepsiFiveCentsOff = new AmountDiscount {AmountOff = 0.05m, Product = Pepsi};
            _order.AddDiscount(pepsiFiveCentsOff);
            _order.ApplyDiscounts();

            // act
            var total = _order.CalculateTotal();

            // assert
            Assert.Equal(0.95m, total);
        }
    }
}
