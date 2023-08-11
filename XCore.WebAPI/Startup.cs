using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Diagnostics;
using XCore.WebAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace XCore.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // 注册服务
        public void ConfigureServices(IServiceCollection services)
        {
            // 加载邮箱配置，映射到类中
            var notificationMetadata = Configuration.GetSection("NotificationMetadata").Get<NotificationMetadata>();
            services.AddSingleton(notificationMetadata);
            GlobalVariable.EmailModel = notificationMetadata;

            // 将项目中的控制器注册到容器中
            services.AddControllers();

            // 加载用户机密
            var configServer = this.Configuration.GetConnectionString("configServer");

            // 将Swagger相关的服务注册到容器中
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "测试标题",
                    Description = "XCore.WebAPI的描述",
                });

                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）

                var xmlPath = Path.Combine(basePath, "XCore.WebAPI.xml");
                c.IncludeXmlComments(xmlPath);
            });

            // 注册了一个自定义的服务
            services.AddScoped<MyService>();
            services.AddScoped<LoginService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var secretByte = Encoding.UTF8.GetBytes(Configuration["Authentication:secretKey"]);
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //只有配置的发布者donesoft.cn才会被接受
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Authentication:issuer"],

                    //只有配置的使用者donesoft.cn才会被接受
                    ValidateAudience = true,
                    ValidAudience = Configuration["Authentication:audience"],

                    //验证token是否过期
                    ValidateLifetime = true,

                    //对密码进行加密
                    IssuerSigningKey = new SymmetricSecurityKey(secretByte)
                };
            });

            //services.AddDbContext<IdDbContext>(opt =>
            //{
            //    string connStr = this.Configuration.GetConnectionString("configServer");
            //    opt.UseMySql("server=127.0.0.1;port=3306;database=db_hotel;uid=root;pwd=123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.34-mysql"));
            //});
            //services.AddDataProtection();
            //services.AddIdentityCore<User>(options =>
            //{
            //    options.Password.RequireDigit = false;  // 密码是否必须包含数字
            //    options.Password.RequireLowercase = false;  // 密码是否必须包含小写字母
            //    options.Password.RequireNonAlphanumeric = false;  // 密码是否必须包含非字母数字字符，非字母数字字符是字母数字字符以外的字符。 空格，百分号，下划线，竖线，冒号，分号等均为非字母数字字符。
            //    options.Password.RequireUppercase = false;  // 密码是否必须包含大写字母
            //    options.Password.RequiredLength = 6;    // 密码的最大长度为6
            //    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;  // 用于生成密码重置电子邮件中使用的令牌。
            //    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;  // 用于生成在帐户确认电子邮件中使用的令牌。
            //});

            //var idBuilder = new IdentityBuilder(typeof(User), typeof(Role), services);
            //idBuilder.AddEntityFrameworkStores<IdDbContext>().AddDefaultTokenProviders().AddRoleManager<RoleManager<Role>>().AddUserManager<UserManager<User>>();
        }

        // 配置中间件
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                Debug.WriteLine("目前的环境是开发环境");

                // 这个开发环境
                app.UseDeveloperExceptionPage();

                // 启用中间件服务生成Swagger作为JSON终结点
                app.UseSwagger();

                // 启用中间件服务对swagger - ui，指定Swagger JSON终结点
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            if (env.IsProduction())
            {
                Console.WriteLine("目前的环境是生产环境");
            }

            app.UseHttpsRedirection();

            //使用路由,访问去哪里?
            app.UseRouting();

            // 能否登录成功
            app.UseAuthentication();

            // 有哪些访问权限
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
