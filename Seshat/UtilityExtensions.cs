using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class UtilityExtensions
{
    public static void ForEach<T>(this IEnumerable<T> sequence,
        Action<T> action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        foreach (T item in sequence)
            action(item);
    }
}
