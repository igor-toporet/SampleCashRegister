using System;
using System.Collections.Generic;
using System.Linq;
using SampleCashRegister.Discounts;

namespace SampleCashRegister
{
    public static class FluentApiExtensions
    {
        public static OrderItem AddItem(this Order order, string product)
        {
            var orderItem = new OrderItem {Product = product};

            order.AddItem(orderItem);

            return orderItem;
        }

        public static OrderItem PricePerUnit(this OrderItem orderItem, decimal pricePerUnit)
        {
            orderItem.PricePerUnit = pricePerUnit;

            return orderItem;
        }

        public static OrderItem Quantity(this OrderItem orderItem, decimal quantity)
        {
            orderItem.Quantity = quantity;

            return orderItem;
        }

        public static IEnumerable<OrderItem> ItemsWithProduct(this Order order, string product)
        {
            return
                order.Items
                    .Where(i => i.Product.IsSameAs(product))
                    .ToArray();
        }

        private static bool IsSameAs(this string prod1, string prod2)
        {
            return string.Equals(prod1, prod2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Total amount of given product in the order
        /// </summary>
        /// <param name="order"></param>
        /// <param name="product">name of product</param>
        /// <returns></returns>
        public static decimal ProductQuantity(this Order order, string product)
        {
            return
                order
                    .ItemsWithProduct(product)
                    .Sum(i => i.Quantity);
        }

        public static Bundle.BundleAdditionProduct AddAdditionProduct(this Bundle bundle, string product)
        {
            var additionProduct = new Bundle.BundleAdditionProduct { Product = product };

            return bundle.AddAdditionProduct(additionProduct);
        }

        public static Bundle.BundleAdditionProduct WithPercentOff(
            this Bundle.BundleAdditionProduct additionProduct, int percentOff)
        {
            additionProduct.PercentOff = percentOff;

            return additionProduct;
        }

        public static Bundle.BundleAdditionProduct OnQuantity(this Bundle.BundleAdditionProduct additionProduct, int quantity)
        {
            additionProduct.Quantity = quantity;

            return additionProduct;
        }
    }
}
