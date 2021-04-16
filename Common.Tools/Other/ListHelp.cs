using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Tools.Other
{
    public static class ListHelp
    {
        #region 判断集合是否存在交集
        public static bool IsArrayIntersection(this List<string> list1, List<string> list2)
        {
            return list1.Intersect(list2).Any();
        }
        public static bool IsArrayIntersection(this List<string> list1, string list)
        {
            if (string.IsNullOrWhiteSpace(list))
            {
                return false;
            }
            return list1.Intersect(list.Split(',').ToList()).Any();
        }
        public static bool IsArrayIntersection(this List<string> list1, string list, char separator)
        {
            if (string.IsNullOrWhiteSpace(list))
            {
                return false;
            }
            return list1.Intersect(list.Split(separator).ToList()).Any();
        }
        public static bool IsArrayIntersection(this string list1, string list)
        {
            if (string.IsNullOrWhiteSpace(list1) || string.IsNullOrWhiteSpace(list))
            {
                return false;
            }
            return list1.Split(',').Intersect(list.Split(',')).Any();
        }
        public static bool IsArrayIntersection(this string list1, string list, char separator, char separator2)
        {
            if (string.IsNullOrWhiteSpace(list1) || string.IsNullOrWhiteSpace(list))
            {
                return false;
            }
            return list1.Split(separator).Intersect(list.Split(separator2).ToList()).Any();
        }
        public static bool IsArrayIntersection(this List<int> list1, List<int> list2)
        {
            return list1.Intersect(list2).Any();
        }
        #endregion
    }
}
