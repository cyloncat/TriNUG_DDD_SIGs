using System.Collections.Generic;
using System.Linq;

namespace DomainPatterns
{
    public abstract class DomainEntity
    {
    }

    public abstract class EntityWithCompositeId : DomainEntity
    {
        protected abstract IEnumerable<object> GetIdentityComponents();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            if (GetType() != obj.GetType()) return false;
            EntityWithCompositeId other = obj as EntityWithCompositeId;
            return other != null && GetIdentityComponents().SequenceEqual(other.GetIdentityComponents());
        }

        public override int GetHashCode()
        {
            return HashCodeHelper.CombineHashCodes(GetIdentityComponents());
        }
    }
}