using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.WebApi.Filter
{
    public class ResourceFilter : IResourceFilter
    {
        /// <summary>
        /// 资源过滤器之后
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // Console.WriteLine("资源过滤器之后");
        }
        /// <summary>
        /// 资源过滤器之前
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //Console.WriteLine("资源过滤器之前");
        }
    }
}
