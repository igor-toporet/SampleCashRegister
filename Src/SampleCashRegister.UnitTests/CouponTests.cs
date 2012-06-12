using Xunit;

namespace SampleCashRegister.UnitTests
{
    public class CouponTests
    {
        [Fact]
        public void WhenIsCalculatedTotalThenCouponIsConsidered()
        {
            // arrange
            var order = new Order();
            order.AddItem("some dainty").PricePerUnit(5).Quantity(10);
            order.AddCoupon(new Coupon {Threshold = 50m, AmountOff = 5m});

            // act
            var total = order.CalculateTotal();

            // assert
            Assert.Equal(45, total);
        }

        [Fact]
        public void UnableToApplySameCouponTwice()
        {
            // arrange
            var order = new Order();
            order.AddItem("some dainty").PricePerUnit(7).Quantity(10);
            order.AddCoupon(new Coupon { Threshold = 50m, AmountOff = 5m });

            // act
            order.CalculateTotal();
            var total = order.CalculateTotal();

            // assert
            Assert.Equal(65, total);
        }
    }
}
