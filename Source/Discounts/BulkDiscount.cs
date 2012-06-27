using System;
using System.Linq;

namespace SampleCashRegister.Discounts
{
    public class BulkDiscount : Discount
    {
        public string Product { get; set; }

        public int NumberInGroup { get; set; }

        public decimal SpecialPrice { get; set; }

        public override void Apply(Order order)
        {
            var desiredItems = order.ItemsWithProduct(Product).ToArray();

            if (!desiredItems.Any()) return;

            var regularPrice = desiredItems.First().PricePerUnit;

            var sumOfDesiredItems = desiredItems.Sum(i => i.Quantity);

            int wholeGroupsCount = Convert.ToInt32(sumOfDesiredItems/NumberInGroup);

            string item = string.Format(
                "Bulk discount on '{0}' groups of {1} ({2} whole groups)",
                Product, NumberInGroup, wholeGroupsCount);

            order.AddItem(item).Quantity(wholeGroupsCount)
                .PricePerUnit((SpecialPrice - regularPrice)*wholeGroupsCount*NumberInGroup);
        }
    }
}
