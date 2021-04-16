using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Common.Tools.StartupExtenions
{
    public static class IServiceCollectionExtenions
    {
        /// <summary>
        /// 获取程序集中的类
        /// </summary>
        /// <param name="assemblyName">程序集名</param>
        /// <returns></returns>
        private static Dictionary<Type, Type> GetClassName(string assemblyName)
        {
            var result = new Dictionary<Type, Type>();
            if (!string.IsNullOrWhiteSpace(assemblyName))
            {
                //排除程序程序集中的接口、私有类、抽象类
                Assembly assembly = Assembly.Load(assemblyName);
                var typeList = assembly.GetTypes().Where(t => !t.IsInterface && !t.IsSealed && !t.IsAbstract).ToList();
                //历遍程序集中的类
                foreach (var item in typeList)
                {
                    //查找当前类继承的且包含当前类名的接口
                    var interfaceType = item.GetInterfaces().Where(o => o.Name.Contains(item.Name)).FirstOrDefault();
                    if (interfaceType != null)
                    {
                        //把当前类和继承接口加入Dictionary
                        result.Add(item, interfaceType);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 批量注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName">程序集名</param>
        /// <param name="lifetime">生命周期</param>
        public static void BatchRegisterService(this IServiceCollection services, string assemblyName, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            foreach (var item in GetClassName(assemblyName))
            {
                //根据生命周期注册
                switch (lifetime)
                {
                    case ServiceLifetime.Scoped:
                        services.AddScoped(item.Value, item.Key);
                        break;
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(item.Value, item.Key);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(item.Value, item.Key);
                        break;
                }
            }
        }
    }
}
