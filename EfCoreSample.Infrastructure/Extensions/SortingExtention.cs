using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCoreSample.Infrastructure.Extensions
{
    /*public static class SortingExtention
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> source, string sortBy)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (string.IsNullOrEmpty(sortBy))
                throw new ArgumentNullException("sortBy");
            
                    source = source.OrderBy(sortBy);

            return source;
        }
    }*/
}
/*
            switch (sortBy)
            {
                case "updated_desc":
                    source = source.OrderByDescending(s => s.LastUpdated);
                    break;
                default:
                    source = source.OrderBy(s => s.LastUpdated);
                    break;
            }
     */
