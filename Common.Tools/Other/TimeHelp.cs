using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Tools.Other
{
    public static class TimeHelp
    {
        #region 时间戳
        /// <summary>
        /// 十位数时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimestamp10()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000; //10位
        }
        /// <summary>
        /// 十三位数时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimestamp13()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000; //13位
        }

        /// <summary>
        /// 时间戳转时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime unixTimeToTime(this int? timeStamp)
        {
            //TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local,
            //                                                easternZone);
            //= TimeZoneInfo.Local.GetUtcOffset();
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime daTime = dtStart.Add(toNow);
            return daTime;
        }
        #endregion
    }
}
