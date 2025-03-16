using System;
using System.Collections.Generic;

namespace _Scripts.Utils
{
    public static class ListExtensions
    {
        private static Random rng;

        public static void Shuffle<T>(this IList<T> list, int seed)
        {
            rng = new Random(seed);
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}