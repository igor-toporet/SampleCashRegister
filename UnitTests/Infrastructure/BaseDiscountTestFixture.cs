namespace SampleCashRegister.UnitTests.Infrastructure
{
    public class BaseDiscountTestFixture
    {
        protected const string Pepsi = "pepsi";

        protected readonly Order _order = new Order();

// ReSharper disable MemberCanBeProtected.Global
        public BaseDiscountTestFixture()
// ReSharper restore MemberCanBeProtected.Global
        {
            _order.AddItem(Pepsi).PricePerUnit(1).Quantity(1);
        }
    }
}
