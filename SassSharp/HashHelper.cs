using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp
{
    internal static class HashHelper
    {
        // Generic, rathher than taking object params so as to avoid boxing
        internal static int Hash<T1, T2>(T1 a, T2 b)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + a.GetHashCode();
                hash = hash * 23 + b.GetHashCode();
                return hash;
            }
        }

        internal static int Hash<T1, T2, T3, T4>(T1 a, T2 b, T3 c, T4 d)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + a.GetHashCode();
                hash = hash * 23 + b.GetHashCode();
                hash = hash * 23 + c.GetHashCode();
                hash = hash * 23 + d.GetHashCode();
                return hash;
            }
        }
    }
}
