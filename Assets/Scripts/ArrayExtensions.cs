using System;
using System.Collections.Generic;

public static class ArrayExtensions
{
    private static Random rng = new Random();

    public static void Shuffle<T>(this T[] array)
    {
        int n = array.Length;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);

            T temp = array[k];
            array[k] = array[n];
            array[n] = temp;
        }
    }
}

public static class ListExtensions
{
    private static readonly Random rng = new Random();

    public static void Shuffle<T>(this List<T> list)
    {
        if (list == null || list.Count <= 1)
            return;

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}