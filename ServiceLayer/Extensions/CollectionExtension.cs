﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.Extensions
{
    public static class CollectionExtension
    {
        public static void RemoveAllExtension<T>(this IList<T> iList, IEnumerable<T> itemsToRemove)
        {
            var set = new HashSet<T>(itemsToRemove);

            var list = iList as List<T>;
            if (list == null)
            {
                int i = 0;
                while (i < iList.Count)
                {
                    if (set.Contains(iList[i])) iList.RemoveAt(i);
                    else i++;
                }
            }
            else
            {
                list.RemoveAll(set.Contains);
            }
        }
    }
}
