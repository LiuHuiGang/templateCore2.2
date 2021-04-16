using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.WebApi.Filter
{
    public class ResultFilter : IResultFilter
    {
        /// <summary>
        /// 结果过滤器之后
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            //Console.WriteLine("结果过滤器之后");
        }
        /// <summary>
        /// 结果过滤器之前
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            //Console.WriteLine("结果过滤器之前");
        }
    }
}
