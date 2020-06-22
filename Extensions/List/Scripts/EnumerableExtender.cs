using System.Linq;
using System.Collections.Generic;

namespace homehelp.Extenders
{
    public static class EnumerableExtender
    {
        /// <summary>
        /// Method that shuffles the Enumerable that is passed as parameter
        /// </summary>
        /// <param name="enumerable">A list or a vector</param>
        /// <typeparam name="T">Collection type</typeparam>
        /// <returns>The same type as passed before, but shuffled</returns>
        public static IEnumerable<T> ShuffleList<T>(this IEnumerable<T> enumerable)
        {
            var random = new System.Random();
            var query = from element in enumerable
                let r = random.Next()
                orderby r
                select element;

            return query;
        }
    }
}
