using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Com.Models.VO
{
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举说明
        /// </summary>
        public static string DisplayName(this Enum val)
        {
            return val.Display() as string;
        }
        /// <summary>
        /// 获取枚举指定的显示内容
        /// </summary>
        public static object Display(this Enum val)
        {
            Type type = val.GetType();
            FieldInfo fd = type.GetField(val.ToString());
            string name = null;
            if (fd != null)
            {
                object[] attrs = fd.GetCustomAttributes(typeof(RemarkAttribute), false);
                foreach (RemarkAttribute attr in attrs)
                {
                    name = attr.Remark;
                }
            }
            return name;
        }
    }
}
