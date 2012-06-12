using SampleCashRegister.Core;
using SampleCashRegister.Core.Discounts;
using Xunit;

namespace SampleCashRegister.UnitTests
{
    public class BundleTests
    {
        [Fact]
        public void CanAddBaseProducts()
        {
            // arrange
            var bundle = new Bundle();

            // act
            bundle.AddBaseProduct(5, "pepsi");

            // assert
            Assert.Equal(1, bundle.BaseProducts.Count);
            Assert.Equal("pepsi", bundle.BaseProducts[0].Product);
            Assert.Equal(5, bundle.BaseProducts[0].Quantity);
        }

        [Fact]
        public void CanAddAdditionProducts()
        {
            // arrange
            var bundle = new Bundle();

            // act
            bundle.AddAdditionProduct("cup").WithPercentOff(75).OnQuantity( 2 );

            // assert
            Assert.Equal(1, bundle.AdditionProducts.Count);
            Assert.Equal("cup", bundle.AdditionProducts[0].Product);
            Assert.Equal(2, bundle.AdditionProducts[0].Quantity);
            Assert.Equal(75, bundle.AdditionProducts[0].PercentOff);
        }

        [Fact]
        public void CanGiveTwoCups75PercentOffForFivePepsi()
        {
            // arrange
            var order = new Order();
            order.AddItem("pepsi").Quantity(5).PricePerUnit(1);
            order.AddItem("souvenir cup").Quantity(2).PricePerUnit(2);
            var bundle = new Bundle();
            bundle.AddBaseProduct(5, "pepsi");
            bundle.AddAdditionProduct("souvenir cup").WithPercentOff(75).OnQuantity(2);
            order.AddDiscount(bundle);

            // act
            order.ApplyDiscounts();
            var total = order.CalculateTotal();

            // assert
            Assert.Equal(6, total);
        }

        [Fact]
        public void CanGetShortBundleDescription()
        {
            // arrange
            var bundle = new Bundle();
            bundle.AddBaseProduct(2, "pepsi");
            bundle.AddBaseProduct(3, "hotdogs");
            bundle.AddAdditionProduct("plates").WithPercentOff(50).OnQuantity(2);
            bundle.AddAdditionProduct("forks").WithPercentOff(25).OnQuantity(4);

            // act
            string descr = bundle.GetShortBundleDescr();

            // assert
            const string expected =
                "[2 pepsi, 3 hotdogs] => " +
                "[50% off on 2 plates, 25% off on 4 forks]";
            Assert.Equal(expected, descr);
        }
    }
}