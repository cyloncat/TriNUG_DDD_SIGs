using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainPatterns
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            if (GetType() != obj.GetType()) return false;
            ValueObject v = obj as ValueObject;
            return v != null && GetEqualityComponents().SequenceEqual(v.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return HashCodeHelper.CombineHashCodes(GetEqualityComponents());
        }
    }

    public abstract class ComparableValueObject : ValueObject, IComparable
    {
        protected abstract IEnumerable<IComparable> GetComparableComponents();

        protected IComparable AsNonGenericComparable<T>(IComparable<T> comparable)
        {
            return new NonGenericComparable<T>(comparable);
        }

        class NonGenericComparable<T> : IComparable
        {
            private readonly IComparable<T> comparable;
            public NonGenericComparable(IComparable<T> comparable)
            {
                this.comparable = comparable;
            }

            public int CompareTo(object obj)
            {
                if (ReferenceEquals(comparable, obj)) return 0;
                if (ReferenceEquals(null, obj))
                    throw new ArgumentNullException();
                return comparable.CompareTo((T) obj);
            }
        }

        protected int CompareTo(ComparableValueObject other)
        {
            using (IEnumerator<IComparable> thisComponents = GetComparableComponents().GetEnumerator())
            using (IEnumerator<IComparable> otherComponents = other.GetComparableComponents().GetEnumerator())
            {
                while (true)
                {
                    bool x = thisComponents.MoveNext();
                    bool y = otherComponents.MoveNext();
                    if (x != y)
                        throw new InvalidOperationException();
                    if (x)
                    {
                        int c = thisComponents.Current.CompareTo(otherComponents.Current);
                        if (c != 0)
                            return c;
                    }
                    else
                    {
                        break;
                    }
                }
                return 0;
            }
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(this, obj)) return 0;
            if (ReferenceEquals(null, obj)) return 1;
            if (GetType() != obj.GetType())
                throw new InvalidOperationException();
            return CompareTo(obj as ComparableValueObject);
        }
    }

    public abstract class ComparableValueObject<T> : ComparableValueObject, IComparable<T>
        where T : ComparableValueObject<T>
    {
        public int CompareTo(T other)
        {
            return base.CompareTo(other);
        }
    }
}