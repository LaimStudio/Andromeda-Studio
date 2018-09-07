using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaStudio.Classes.Extensions
{
    public static class ListExtension
    {
        public static T FindRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childrenSelector, Predicate<T> condition)
        {
            if (source == null || !source.Any()) return default(T);

            var attempt = source.FirstOrDefault(t => condition(t));
            if (!Equals(attempt, default(T))) return attempt;

            return source.SelectMany(childrenSelector)
                .FindRecursive(childrenSelector, condition);
        }
    }
}
