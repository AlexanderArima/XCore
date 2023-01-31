using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.PMS.WebAPI.Model;
using XCore.PMS.WebAPI.Model_ORM;

namespace XCore.PMS.WebAPI
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
            services.AddControllers();

            services.AddDbContext<db_hotelContext>();

            // 加载用户机密
            GlobalConfig.DbConnectionString = this.Configuration.GetConnectionString("configServer");

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

                var xmlPath = Path.Combine(basePath, "XCore.PMS.WebAPI.xml");
                c.IncludeXmlComments(xmlPath);
            });

            // 授权
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
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
