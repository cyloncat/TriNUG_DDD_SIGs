using DomainPatterns;

namespace DomainModel
{
    public class ProductId : Identity
    {
        public ProductId()
            : base()
        {
        }

        public ProductId(string id)
            : base(id)
        {
        }
    }
}