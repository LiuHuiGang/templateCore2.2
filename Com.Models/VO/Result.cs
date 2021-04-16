using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Models.VO
{
    public class Result : IResult
    {
        private string _msg;

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success => code == ResultCode.Ok;

        /// <summary>
        /// 结果码
        /// </summary>
        public ResultCode code { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string msg
        {
            get { return _msg ?? code.DisplayName(); }
            set { _msg = value; }
        }

        /// <summary>
        /// 返回结果，默认成功
        /// </summary>
        public Result()
        {
            code = ResultCode.Ok;
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="message">提示信息</param>
        public Result(ResultCode code, string msg = null)
        {
            this.code = code;
            this.msg = msg;
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        public static Result Ok(string msg = null)
        {
            return FromCode(ResultCode.Ok, msg);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        public static Result1 Ok(dynamic data)
        {
            return FromData(data);
        }

        /// <summary>
        /// 返回指定 Code
        /// </summary>
        public static Result FromCode(ResultCode code, string msg = null)
        {
            return new Result(code, msg);
        }
        /// <summary>
        /// 返回指定 Code和提示信息
        /// </summary>
        public static Result1 FromCode(ResultCode code, dynamic data, string msg = null)
        {
            return new Result1(code, msg, data);
        }

        /// <summary>
        /// 返回错误信息
        /// </summary>
        public static Result FromError(string msg, ResultCode code = ResultCode.Fail)
        {
            return new Result(code, msg);
        }

        /// <summary>
        /// 返回数据
        /// </summary>
        public static Result1 FromData(dynamic data)
        {
            if (data == null)
            {
                return new Result1("");
            }
            return new Result1(data);
        }
        /// <summary>
        /// 返回数据和提示信息
        /// </summary>
        public static Result1 FromData(dynamic data, string msg)
        {
            if (data == null)
            {
                return new Result1("");
            }
            return new Result1(ResultCode.Ok, msg, data);
        }
        public static Result2 FromData(dynamic data, dynamic info)
        {
            if (data == null)
            {
                return new Result2("", info);
            }
            return new Result2(data, info);
        }
    }
    /// <summary>
    /// 返回结果
    /// </summary>
    public class Result1 : Result, IResult1
    {
        /// <summary>
        /// 返回业务数据
        /// </summary>
        public dynamic data { get; set; }

        /// <summary>
        /// cotr
        /// </summary>
        public Result1() { }

        /// <summary>
        /// 返回结果
        /// </summary>
        public Result1(dynamic data)
            : base(ResultCode.Ok)
        {
            this.data = data;
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        public Result1(ResultCode code, string msg = null, dynamic data = null)
            : base(code, msg)
        {
            this.data = data;
        }
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    public class Result2 : Result1, IResult2
    {
        /// <summary>
        /// 返回业务数据
        /// </summary>
        public dynamic info { get; set; }

        /// <summary>
        /// cotr
        /// </summary>
        public Result2() { }
        /// <summary>
        /// 返回结果
        /// </summary>
        public Result2(dynamic data, dynamic info)
        {
            this.data = data; //属性继承Result1
            this.info = info;
        }
    }
}
