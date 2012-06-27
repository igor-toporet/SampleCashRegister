using SampleCashRegister.UnitTests.Infrastructure;
using Xunit;

namespace SampleCashRegister.UnitTests
{
    public class OrderTests
    {
        private readonly Order _order = new Order();

        [Fact]
        public void CanAddItem()
        {
            // arrange
            var orderItem = new OrderItem();

            // act
            _order.AddItem(orderItem);

            // assert
            Assert.Equal(1, _order.Items.Count);
        }

        [Fact]
        public void CanCalculateTotalNaive()
        {
            // arrange
            _order
                .AddItem("Apples")
                .PricePerUnit(0.70m)
                .Quantity(1.1m);

            // act
            decimal total = _order.CalculateTotal();

            // assert
            Assert.Equal(0.77m, total);
        }

        [Fact]
        public void CanCalculateTotalWithFewItems()
        {
            // arrange
            _order
                .AddItem("Apples")
                .PricePerUnit(0.70m)
                .Quantity(1.1m);

            _order
                .AddItem("Bananas")
                .PricePerUnit(0.50m)
                .Quantity(2);

            // act
            decimal total = _order.CalculateTotal();

            // assert
            Assert.Equal(1.77m, total);
        }

        [Fact]
        public void CanAddDiscount()
        {
            // arrange
            var discount = new FakeDiscount();

            // act
            _order.AddDiscount(discount);

            // assert
            Assert.Equal(1, _order.Discounts.Count);
        }

        [Fact]
        public void CanAddCoupon()
        {
            // arrange
            var coupon = new Coupon();

            // act
            _order.AddCoupon(coupon);

            // assert
            Assert.Equal(1, _order.Coupons.Count);
        }
    }
}
