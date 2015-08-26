using System;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainDrivenTests
{
    [TestClass]
    public class DiscountByDateTest
    {
        [TestMethod]
        public void TestMatchSameDateObject()
        {
            DiscountByDate discount = new DiscountByDate(.1m, DateTime.Now);
            Assert.IsTrue(discount.Equals(discount));
        }

        [TestMethod]
        public void TestMatchSameValues()
        {
            DiscountByDate discount = new DiscountByDate(.1m, DateTime.Now);
            DiscountByDate identicalDiscount = new DiscountByDate(discount);
            Assert.IsTrue(discount.Equals(identicalDiscount));
        }

        [TestMethod]
        public void TestMatchDifferentValues()
        {
            DiscountByDate discount = new DiscountByDate(.1m, DateTime.Now);
            DiscountByDate differentDiscount = new DiscountByDate(.1m, DateTime.Today);
            Assert.IsFalse(discount.Equals(differentDiscount));
        }

        [TestMethod]
        public void VerifyExtendedDate()
        {
            DiscountByDate original = new DiscountByDate(.1m, DateTime.Now);
            DiscountByDate extended = original.ExtendByDays(7);
            Assert.IsTrue(extended.ExpirationDate == original.ExpirationDate.AddDays(7));
        }

        [TestMethod]
        public void TestImmutabilityWhenChangingExpirationDate()
        {
            DiscountByDate original = new DiscountByDate(.1m, DateTime.Now);
            DiscountByDate verifier = new DiscountByDate(original);
            DiscountByDate extended = original.ExtendByDays(7);
            Assert.IsFalse(original.Equals(extended));
            Assert.IsTrue(original.Equals(verifier));
        }
    }
}
