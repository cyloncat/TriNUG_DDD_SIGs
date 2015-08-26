using System;
using System.Collections.Generic;
using DomainPatterns;

namespace DomainModel
{
    public class DiscountByDate : ValueObject
    {
        public decimal DiscountAmount { get; }
        public DateTime ExpirationDate { get; }


        public DiscountByDate(decimal discountAmount, DateTime expirationDate)
        {
            DiscountAmount = discountAmount;
            ExpirationDate = expirationDate;
        }

        public DiscountByDate(DiscountByDate discount)
        {
            DiscountAmount = discount.DiscountAmount;
            ExpirationDate = discount.ExpirationDate;
        }

        public DiscountByDate ExtendByDays(int days)
        {
            return new DiscountByDate(DiscountAmount, ExpirationDate.AddDays(days));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DiscountAmount;
            yield return ExpirationDate;
        }
    }
}