using System;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainDrivenTests
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void TestProductEqualsSameProduct()
        {
            ProductId prodOneId = new ProductId("1");
            Product prod = new Product(prodOneId, 10m);
            Assert.IsTrue(prod.Equals(prod));
        }

        [TestMethod]
        public void TestProductEqualsIdenticalProduct()
        {
            ProductId prodOneId = new ProductId("1");
            Product prodOne = new Product(prodOneId, 10m);
            Product prodOneClone = new Product(prodOneId, 10m);
            Assert.IsTrue(prodOne.Equals(prodOneClone));
        }

        [TestMethod]
        public void TestProductNotEqualsDifferentProductById()
        {
            ProductId prodOneId = new ProductId("1");
            ProductId prodTwoId = new ProductId("2");
            Product prodOne = new Product(prodOneId, 10m);
            Product prodTwo = new Product(prodTwoId, 10m);
            Assert.IsFalse(prodOne.Equals(prodTwo));
        }

        [TestMethod]
        public void TestProductNotEqualsNull()
        {
            ProductId prodOneId = new ProductId("1");
            Product prodOne = new Product(prodOneId, 10m);
            Assert.IsFalse(prodOne.Equals(null));
        }

        [TestMethod]
        public void TestProductReturnsListPrice()
        {
            ProductId prodOneId = new ProductId("1");
            decimal ListPrice = 10m;
            Product prodOne = new Product(prodOneId, ListPrice);
            Assert.IsTrue(prodOne.ListPrice == ListPrice);
        }

        [TestMethod]
        public void TestProductDiscountWithValidDiscountByDate()
        {
            ProductId prodOneId = new ProductId("1");
            Product prodOne = new Product(prodOneId, 10m);
            DiscountByDate discount = new DiscountByDate(.1m, DateTime.Now.AddDays(1));
            decimal discountedPrice = prodOne.DiscountPrice(discount);
            Assert.IsTrue(discountedPrice == 9m);
        }

        [TestMethod]
        public void TestProductDiscountWithExpiredDiscountByDate()
        {
            ProductId prodOneId = new ProductId("1");
            Product prodOne = new Product(prodOneId, 10m);
            DiscountByDate discount = new DiscountByDate(.1m, DateTime.Now.AddDays(-1));
            decimal discountedPrice = prodOne.DiscountPrice(discount);
            Assert.IsTrue(discountedPrice == 10m);
        }

        [TestMethod]
        public void TestProductDiscountWithInvalidDiscount()
        {
            ProductId prodOneId = new ProductId("1");
            Product prodOne = new Product(prodOneId, 10m);
            string notADiscount = "bad discount";
            decimal discountedPrice = prodOne.DiscountPrice(notADiscount);
            Assert.IsTrue(discountedPrice == 10m);
        }

        [TestMethod]
        public void TestProductDiscountWithNullDiscount()
        {
            ProductId prodOneId = new ProductId("1");
            Product prodOne = new Product(prodOneId, 10m);
            decimal discountedPrice = prodOne.DiscountPrice(null);
            Assert.IsTrue(discountedPrice == 10m);
        }
    }
}
