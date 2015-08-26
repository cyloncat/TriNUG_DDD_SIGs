using System;
using DomainPatterns;

namespace DomainModel
{
    public class Product : DomainEntity, IEquatable<Product>
    {
        public ProductId ProductId { get; }
        private decimal Price { get; }
        public decimal ListPrice => Price;

        public Product(ProductId productId, decimal price)
        {
            ProductId = productId;
            Price = price;
        }

        public decimal DiscountPrice(DiscountByDate discount)
        {
            return discount != null && discount.ExpirationDate > DateTime.Now
                ? Price - (Price * discount.DiscountAmount)
                : Price;
        }

        public decimal DiscountPrice(object notADiscount)
        {
            return Price;
        }

        public bool Equals(Product other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return ProductId.Equals(other.ProductId);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as Product);
        }

        public override int GetHashCode()
        {
            return 2335 + ProductId.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Product [productId={0}, Price={1}]", ProductId, Price);
        }
    }
}
