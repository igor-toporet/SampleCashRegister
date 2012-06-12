using SampleCashRegister.UnitTests.Infrastructure;
using Xunit;

namespace SampleCashRegister.UnitTests
{
    public class OrderTests
    {
        private readonly Order order = new Order();

        [Fact]
        public void CanAddItem()
        {
            // arrange
            var orderItem = new OrderItem();

            // act
            order.AddItem(orderItem);

            // assert
            Assert.Equal(1, order.Items.Count);
        }

        [Fact]
        public void CanCalculateTotalNaive()
        {
            // arrange
            order
                .AddItem("Apples")
                .PricePerUnit(0.70m)
                .Quantity(1.1m);

            // act
            decimal total = order.CalculateTotal();

            // assert
            Assert.Equal(0.77m, total);
        }

        [Fact]
        public void CanCalculateTotalWithFewItems()
        {
            // arrange
            order
                .AddItem("Apples")
                .PricePerUnit(0.70m)
                .Quantity(1.1m);

            order
                .AddItem("Bananas")
                .PricePerUnit(0.50m)
                .Quantity(2);

            // act
            decimal total = order.CalculateTotal();

            // assert
            Assert.Equal(1.77m, total);
        }

        [Fact]
        public void CanAddDiscount()
        {
            // arrange
            var discount = new FakeDiscount();

            // act
            order.AddDiscount(discount);

            // assert
            Assert.Equal(1, order.Discounts.Count);
        }

        [Fact]
        public void CanAddCoupon()
        {
            // arrange
            var coupon = new Coupon();

            // act
            order.AddCoupon(coupon);

            // assert
            Assert.Equal(1, order.Coupons.Count);
        }
    }
}