using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApi.Dal.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<TEntity>> Batch<TEntity>(this IEnumerable<TEntity> entities, int batchSize)
        {
            if (batchSize < 1)
                throw new ArgumentException();

            var rest = entities;

            while (rest.Any())
            {
                yield return rest.Take(batchSize);
                rest = rest.Skip(batchSize);
            }
        }
    }
}
