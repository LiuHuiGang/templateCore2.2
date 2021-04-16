using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Web.Filter
{
    /// <summary>
    /// 过滤接口部分接口，统计接口访问
    /// </summary>
    public class StatisticsAttribute : Attribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
