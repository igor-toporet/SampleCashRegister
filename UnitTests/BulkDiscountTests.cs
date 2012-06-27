using SampleCashRegister.Discounts;
using Xunit;

namespace SampleCashRegister.UnitTests
{
    public class BulkDiscountTests
    {
        [Fact]
        public void CanMakeDiscountOnGroupsOfFour()
        {
            // arrange
            var order = new Order();
            order.AddItem("pepsi").Quantity(2).PricePerUnit(1);
            order.AddItem("pepsi").Quantity(3).PricePerUnit(1);
            order.AddDiscount(
                new BulkDiscount
                    {
                        Product = "pepsi",
                        NumberInGroup = 4,
                        SpecialPrice = 0.9m
                    });
            order.ApplyDiscounts();

            // act
            decimal total = order.CalculateTotal();

            // assert
            Assert.Equal(4.6m, total);
        }
    }
}
