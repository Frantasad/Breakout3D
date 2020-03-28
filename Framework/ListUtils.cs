using System;
using System.Collections.Generic;

namespace Breakout3D.Framework
{
    public static class ListUtils
    {
        public static void Add<T>(this List<T> list, params T[] items)
        {
            list.AddRange(items);
        }
        public static void Dispose(this IEnumerable<IDisposable> list)
        {
            foreach (var item in list)
            {
                item.Dispose();
            }
        }
    }
}