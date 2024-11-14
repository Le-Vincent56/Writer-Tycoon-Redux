using System;
using System.Collections.Generic;

namespace GhostWriter.Extensions.Enumerable
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Performs an action on each element in the sequence
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            // Loop through each item in the sequence
            foreach(T item in sequence)
            {
                // Perform the action on the item
                action(item);
            }
        }
    }
}