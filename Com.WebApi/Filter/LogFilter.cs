using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.WebApi.Filter
{
    /// <summary>
    /// 方法前后执行过滤器，日志过滤器
    /// </summary>
    public class LogFilter : IActionFilter
    {
        //action执行之后
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
        //action执行之前
        public void OnActionExecuting(ActionExecutingContext context)
        {
            AddLog(context);
        }
        //不往下执行方法
        //context.Result = new EmptyResult();
        //不往下执行方法，跳转
        //context.Result = new RedirectResult("/admin/login/");
        void AddLog(ActionExecutingContext context)
        {
            var logger = LogManager.GetCurrentClassLogger();
            var Request = context.HttpContext.Request;
            StringBuilder info = new StringBuilder();
            info.Append("路由地址：{" + Request.Path + "}");
            info.Append("  Authorization：{" + Request.Headers["Authorization"] + "}");
            if (Request.Method.ToLower() == "get")
            {
                info.Append("  传参方式：{" + Request.Method + "}");
                info.Append("  参数：{" + Request.QueryString + "}");
            }
            else
            {
                info.Append("  传参方式：{" + Request.Method + "}");
                info.Append("  参数：{" + JsonConvert.SerializeObject(context.ActionArguments.Values) + "}");
            }
            logger.Info(info.ToString());
        }
    }
}
