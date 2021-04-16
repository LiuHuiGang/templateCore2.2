using NPinyin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Tools.Other
{
    public static class StringHelp
    {
        /// <summary>
        /// 汉字转全拼
        /// </summary>
        /// <param name="strChinese"></param>
        /// <param name="isToUpper">是否大写（默认大写）</param>
        /// <returns></returns>
        public static string ConvertToAllSpell(this string strChinese,bool isToUpper = true)
        {
            string result = string.Empty;
            foreach (char item in strChinese)
            {
                result += Pinyin.GetPinyin(item);
            }
            if (isToUpper)
            {
                return result.ToUpper();
            }
            return result.ToLower();
        }
    }
}
