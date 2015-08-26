using System;
using DomainPatterns.Interfaces;

namespace DomainPatterns
{
    public abstract class Identity : IEquatable<Identity>, IIdentity
    {
        public string Id { get; protected set; }

        public Identity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Identity(string id)
        {
            Id = id;
        }

        public bool Equals(Identity id)
        {
            if (ReferenceEquals(this, id)) return true;
            if (ReferenceEquals(null, id)) return false;
            return Id.Equals(id.Id);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as Identity);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + "[Id=" + Id + "]";
        }
    }
}