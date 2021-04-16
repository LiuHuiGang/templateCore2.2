using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Tools.Redis
{
    /// <summary>
    /// redis帮助类
    /// </summary>
    public class RedisHelper
    {
        private ConnectionMultiplexer redis { get; set; }
        private IDatabase db { get; set; }
        /// <summary>
        /// redis 的IP端口号
        /// </summary>
        /// <param name="connection">127.0.0.1:6379</param>
        public RedisHelper(string connection)
        {
            redis = ConnectionMultiplexer.Connect(connection);
            db = redis.GetDatabase();
        }

        /// <summary>
        /// 增加/修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(string key, string value)
        {
            return db.StringSet(key, value);
        }
        /// <summary>
        /// 设置过期时间（分钟）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public bool SetValue(string key, string value, int minute)
        {
            TimeSpan ts = new TimeSpan(0, minute, 0);
            return db.StringSet(key, value, ts);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            return db.StringGet(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteKey(string key)
        {
            return db.KeyDelete(key);
        }
    }
}
