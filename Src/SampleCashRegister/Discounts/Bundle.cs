using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleCashRegister.Discounts
{
    public class Bundle : Discount
    {
        private readonly IList<BundleBaseProduct> _baseProducts = new List<BundleBaseProduct>();
        private readonly IList<BundleAdditionProduct> _additionProducts = new List<BundleAdditionProduct>();

        public IList<BundleBaseProduct> BaseProducts
        {
            get { return _baseProducts; }
        }

        public IList<BundleAdditionProduct> AdditionProducts
        {
            get { return _additionProducts; }
        }

        public override void Apply(Order order)
        {
            var numberOfApplicableBundles = BaseProducts
                .Select(b => Convert.ToInt32(order.ProductQuantity(b.Product) / b.Quantity))
                .Min();

            foreach (var addition in AdditionProducts)
            {
                var actualQuantity = order.ProductQuantity(addition.Product);
                if (actualQuantity == 0) continue;

                var applicableQuantity = numberOfApplicableBundles * addition.Quantity;
                //
                // number of addition products in order can be bigger or lesser
                // than according to number of applicable items
                //
                var adjustmentQuantity = Math.Min(actualQuantity, applicableQuantity);

                string item = string.Format(
                    "{0}% off on '{1}' (bundle {2})",
                    addition.PercentOff, addition.Product, GetShortBundleDescr());

                decimal price = order.ItemsWithProduct(addition.Product).First().PricePerUnit;

                order.AddItem(item).Quantity(adjustmentQuantity)
                    .PricePerUnit(-price * addition.PercentOff / 100);
            }
        }

        public string GetShortBundleDescr()
        {
            string bundleBase = BaseProducts
                .Select(b => b.Quantity + " " + b.Product)
                .Aggregate((x, y) => x + ", " + y);

            string bundleAddition = AdditionProducts
                .Select(a => a.PercentOff + "% off on " + a.Quantity + " " + a.Product)
                .Aggregate((x, y) => x + ", " + y);

            return string.Format("[{0}] => [{1}]", bundleBase, bundleAddition);
        }

        #region Nested type: BundleAdditionProduct

        public class BundleAdditionProduct : BundleBaseProduct
        {
            public int PercentOff { get; set; }
        }

        #endregion

        #region Nested type: BundleBaseProduct

        public class BundleBaseProduct
        {
            public string Product { get; set; }
            public int Quantity { get; set; }
        }

        #endregion

        public void AddBaseProduct(int quantity,string product)
        {
            _baseProducts.Add(new BundleBaseProduct { Product = product, Quantity = quantity });
        }

        public BundleAdditionProduct AddAdditionProduct(BundleAdditionProduct additionProduct)
        {
            _additionProducts.Add(additionProduct);

            return additionProduct;
        }
    }
}
