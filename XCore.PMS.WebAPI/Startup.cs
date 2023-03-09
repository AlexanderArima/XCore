using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

            // 注册Identity框架
            services.AddDbContext<IdDbContext>(opt =>
            {
                string connStr = this.Configuration.GetConnectionString("IdentityConfigServer");
                opt.UseMySql(connectionString: connStr, serverVersion: ServerVersion.AutoDetect(connStr));
            });

            services.AddDataProtection();
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;    // 密码必须包含数字
                options.Password.RequireLowercase = false;    // 密码必须包含小写字母
                options.Password.RequireNonAlphanumeric = false;    // 密码必须包含特殊符号
                options.Password.RequireUppercase = false;    // 密码必须包含大写字母
                options.Password.RequiredLength = 6;    // 密码长度至少6位
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;    // 重置密码采用邮件的形式
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;    // 确认邮件验证码采用邮件的形式
            });

            var idbuilder = new IdentityBuilder(typeof(User), typeof(Role), services);
            idbuilder.AddEntityFrameworkStores<IdDbContext>().AddDefaultTokenProviders().AddRoleManager<RoleManager<Role>>().AddUserManager<UserManager<User>>();

            // 加载用户机密
            GlobalConfig.DbConnectionString = this.Configuration["configServer"];

            // 将Swagger相关的服务注册到容器中
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "测试标题",
                    Description = "XCore.WebAPI的描述",
                });

                // 通过对OpenAPI的配置实现从Swagger中发送Authorization报文头
                var scheme = new OpenApiSecurityScheme()
                {
                    Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Authorization"
                    },
                    Scheme = "oauth2",
                    Name = "Authorization",
                    In = ParameterLocation.Header,Type = SecuritySchemeType.ApiKey,
                };

                c.AddSecurityDefinition("Authorization", scheme);
                var requirement = new OpenApiSecurityRequirement();
                requirement[scheme] = new List<string>();
                c.AddSecurityRequirement(requirement);

                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）

                var xmlPath = Path.Combine(basePath, "XCore.PMS.WebAPI.xml");
                c.IncludeXmlComments(xmlPath);
            });

            // 配置JWT
            services.Configure<JWTOptions>(Configuration.GetSection("JWT"));    // 注册JWT对象
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var jwtOption = Configuration.GetSection("JWT").Get<JWTOptions>();
                var secretByte = Encoding.UTF8.GetBytes(jwtOption.SigningKey);
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //只有配置的发布者donesoft.cn才会被接受
                    ValidateIssuer = false,

                    //只有配置的使用者donesoft.cn才会被接受
                    ValidateAudience = false,

                    //验证token是否过期
                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,

                    //对密码进行加密
                    IssuerSigningKey = new SymmetricSecurityKey(secretByte)
                };
            });

            // 注入Log4Net
            services.AddLogging(m =>
            {
                // 默认的配置文件路径是在根目录，且文件名为log4net.config
                m.AddLog4Net();
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
