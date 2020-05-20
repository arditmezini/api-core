using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApi.Common.LinqExtensions
{
    public static class IEnumerable
    {
        public static bool AnyOrDefault<T>(this IEnumerable<T> sequence)
        {
            if (sequence != null)
            {
                return sequence.Any();
            }
            return false;
        }
    }
}