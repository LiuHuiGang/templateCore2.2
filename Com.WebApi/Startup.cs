using Com.IService;
using Com.Models;
using Com.Models.DTO;
using Com.Models.VO;
using Com.Service;
using Com.WebApi.Filter;
using Common.Core;
using Common.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Tools.StartupExtenions;

namespace Com.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region 注册数据库上下文
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => options.UseMySql(connection), ServiceLifetime.Transient);
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

            #region 注册CORS跨域处理
            services.AddCors(c =>
            {
                c.AddPolicy("AllowAnyOrigin", policy =>
                {
                    policy.AllowAnyOrigin()//允许任何源
                    .AllowAnyMethod()//允许任何方式
                    .AllowAnyHeader()//允许任何头
                    .AllowCredentials();//允许cookie
                });
                c.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins("http://localhost:8083")//允许8083端口
                    .WithMethods("GET", "POST", "PUT", "DELETE")//允许get、post、put、delete
                    .WithHeaders("authorization");
                });
            });
            #endregion

            #region 注册Swagger API测试插件
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = " API 文档"
                });
                //// 设置SWAGER JSON和UI的注释路径。
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
            });
            #endregion

            #region 身份验证Jwt配置
            //将appsettings.json中的JwtSettings部分文件读取到JwtSettings中，这是给其他地方用的
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            //由于初始化的时候我们就需要用，所以使用Bind的方式读取配置
            //将配置绑定到JwtSettings实例中
            var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings);


            //授权角色
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                options.AddPolicy("AdminOrSystem", policy => policy.RequireRole("Admin", "System").Build());
            });
            //密钥加密
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            SigningCredentials signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            // 令牌验证参数
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = false,
                ValidIssuer = jwtSettings.Issuer,//发行人
                ValidateAudience = false,
                ValidAudience = jwtSettings.Audience,//订阅人
                ValidateLifetime = false,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = false,
            };
            // 认证jwt
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
                options.Events = new JwtBearerEvents
                {
                    //认证失败自定义返回结果
                    //此处为权限验证失败后触发的事件
                    OnChallenge = context =>
                    {
                        //此处代码为终止.Net Core默认的返回类型和数据结果，这个很重要，必须
                        context.HandleResponse();
                        //自定义自己想要返回的数据结果，我这里要返回的是Json对象，通过引用Newtonsoft.Json库进行转换
                        var payload = JsonConvert.SerializeObject(Result.FromCode(ResultCode.Unauthorized));
                        //自定义返回的数据类型
                        context.Response.ContentType = "application/json";
                        //自定义返回状态码，默认为401 
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //输出Json数据结果
                        context.Response.WriteAsync(payload);
                        return Task.FromResult(0);
                    }
                };
            });
            #endregion

            #region 配置全局路由
            //在各个控制器添加前缀（没有特定的路由前面添加前缀）
            services.AddMvc(opt =>
            {
                opt.UseCentralRoutePrefix(new RouteAttribute("api/[controller]/[action]"));
            });
            #endregion

            #region 依赖注册
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            //按程序集名称批量注册
            services.BatchRegisterService("Com.Service");
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region 访问静态文件配置
            //设置默认文件
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options); //1
            //提供静态文件
            app.UseStaticFiles(); //2
            #endregion

            #region Swagger api测试插件
            app.UseSwagger();
            //使中间件能够服务于轻量级用户界面（HTML、JS、CSS等），并指定SWAGJER JSON端点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                // 要在应用程序的根处提供Swagger UI ，请将该RoutePrefix属性设置为空字符串
                //c.RoutePrefix = string.Empty;
            });
            #endregion

            #region 启用JWT身份授权认证
            app.UseAuthentication();
            #endregion

            #region nlog日志配置
            loggerFactory.AddNLog();//添加NLog
            env.ConfigureNLog("nlog.config"); //引入Nlog配置文件
            #endregion
            app.UseMvc();
        }
    }
}
