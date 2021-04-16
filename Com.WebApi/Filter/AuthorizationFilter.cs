using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.WebApi.Filter
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        /// <summary>
        /// 授权过滤器
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Console.WriteLine("授权过滤器");
        }
    }
}
