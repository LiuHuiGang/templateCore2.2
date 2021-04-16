using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Models.VO
{
    public enum ResultCode
    {
        /// <summary>
        /// 1操作成功
        ///</summary>
        [Remark("成功")]
        Ok = 1,

        /// <summary>
        /// 0操作失败
        ///</summary>
        [Remark("失败")]
        Fail = 0,

        /// <summary>
        /// 401访问被拒绝
        ///</summary>
        [Remark("访问被拒绝")]
        Unauthorized = 401,

        /// <summary>
        /// 403资源不可用
        ///</summary>
        [Remark("资源不可用")]
        Forbidden = 403,

        /// <summary>
        /// 404无法找到指定位置的资源
        ///</summary>
        [Remark("无法找到指定位置的资源")]
        NotFound = 404,

        /// <summary>
        /// 500系统错误
        ///</summary>
        [Remark("系统错误")]
        ServerError = 500
    }
}
