using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public static class BatchExtension
    {
        public static IEnumerable<IEnumerable<TIn>> Batch<TIn>(this IEnumerable<TIn> source, int size)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));
            TIn[] bucket = null;
            var count = 0;
            foreach (var item in source)
            {
                if (bucket == null)
                    bucket = new TIn[size];
                bucket[count++] = item;
                if (count != size)
                    continue;
                yield return bucket;
                bucket = null;
                count = 0;
            }
            if (bucket != null & count > 0)
                yield return bucket.Take(count).ToArray();
        }

        public static void ProcessBatchesNoReturn<TIn>(this IEnumerable<IEnumerable<TIn>> batches, Action<IEnumerable<TIn>> procesingAction)
        {
            foreach(var batch in batches)
            {
                procesingAction(batch);
            }
        }
    }
}
