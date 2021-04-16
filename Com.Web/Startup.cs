using Com.IService;
using Com.Models;
using Com.Service;
using Com.Web.Filter;
using Common.Core;
using Common.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Common.Tools.StartupExtenions;

namespace Com.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 注册数据库上下文
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => options.UseMySql(connection), ServiceLifetime.Scoped);
            #endregion

            #region 依赖注册
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
          services.AddScoped<IRepositoryFactory,RepositoryFactory>();
            //按程序集名称批量注册
            services.BatchRegisterService("Com.Service");
            #endregion

            #region  注册全局过滤器
            services.AddMvc(options =>
            {
                options.Filters.Add<ExceptionFilter>();
                options.Filters.Add<LogFilter>();
                options.Filters.Add<AuthorizationFilter>();
                options.Filters.Add<ResourceFilter>();
                options.Filters.Add<ResultFilter>();

            });
            #endregion

            #region 内存缓存
            services.AddMemoryCache();
            #endregion

            #region 登录验证
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie(options =>
                            {
                                options.LoginPath = "/User/Login";
                                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                            });
            #endregion

            #region 解决Razor视图渲染中文乱码
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });
            #endregion

            services.AddHttpClient();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logFac)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            #region nlog日志配置
            logFac.AddNLog();
            env.ConfigureNLog("nlog.config"); //配置文件
            #endregion

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
