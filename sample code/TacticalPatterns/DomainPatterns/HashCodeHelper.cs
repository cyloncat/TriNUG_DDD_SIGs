using System.Collections;
using System.Collections.Generic;

namespace DomainPatterns
{
    internal static class HashCodeHelper
    {
        public static int CombineHashCodes(IEnumerable<object> objs)
        {
            unchecked
            {
                int hash = 17;
                foreach (object obj in objs)
                {
                    hash = hash * 23 + (obj?.GetHashCode() ?? 0);
                }
                return hash;
            }
        }
    }
}