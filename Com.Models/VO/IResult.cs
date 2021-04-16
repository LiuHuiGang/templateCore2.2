using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Models.VO
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// 结果状态码
        /// </summary>
        ResultCode code { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <example>操作成功</example>
        string msg { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        bool success { get; }
    }

    /// <summary>
    /// 返回的附带泛型数据
    /// </summary>
    public interface IResult1 : IResult
    {
        /// <summary>
        /// 返回的附带数据
        /// </summary>
        dynamic data { get; set; }
    }
    /// <summary>
    /// 返回的附带泛型数据
    /// </summary>
    public interface IResult2 : IResult1
    {
        dynamic info { get; set; }
    }
}
