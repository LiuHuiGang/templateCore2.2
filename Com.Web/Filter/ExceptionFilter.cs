using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Web.Filter
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is ApplicationException))
            {
                string Exception = context.Exception.Message;
                var logger = LogManager.GetCurrentClassLogger();
                var Request = context.HttpContext.Request;
                StringBuilder Error = new StringBuilder();
                Error.Append("路由地址：{" + Request.Path + "}");
                Error.Append("  Authorization：{" + Request.Headers["Authorization"] + "}");
                if (Request.Method.ToLower() == "get")
                {
                    Error.Append("  传参方式：{" + Request.Method + "}");
                    Error.Append("  参数：{" + Request.QueryString.Value + "}");
                }
                else
                {
                    Error.Append("  传参方式：{" + Request.Method + "}");
                }
                Error.Append(" 异常信息：{" + Exception + "}");
                logger.Error(Error.ToString());
            }
            //context.Result = BuildExceptionResult(context.Exception);
            context.ExceptionHandled = true;
        }
        /// <summary>
        /// 包装处理异常格式
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        //private JsonResult BuildExceptionResult(Exception ex)
        //{
        //    //应用程序业务级异常
        //    if (ex is ApplicationException)
        //    {
        //        return new JsonResult(Result.FromCode(ResultCode.Fail, ex.Message));
        //    }
        //    return new JsonResult(Result.FromCode(ResultCode.ServerError));
        //}
    }
}
