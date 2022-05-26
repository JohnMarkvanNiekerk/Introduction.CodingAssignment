using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.CodingAssignment.Data
{
    static class ThreadSafeExtensions
    {
        public static void Remove<T>(this ConcurrentBag<T> bag, T removeitem)
        {
            try
            {
                List<T> removelist = bag.ToList();
                removelist.Remove(removeitem);

                bag = new ConcurrentBag<T>();

                Parallel.ForEach(removelist, currentitem =>
                {
                    bag.Add(currentitem);
                });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
