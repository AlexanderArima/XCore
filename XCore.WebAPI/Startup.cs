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

        // ע�����
        public void ConfigureServices(IServiceCollection services)
        {
            // �����������ã�ӳ�䵽����
            var notificationMetadata = Configuration.GetSection("NotificationMetadata").Get<NotificationMetadata>();
            services.AddSingleton(notificationMetadata);
            GlobalVariable.EmailModel = notificationMetadata;

            // ����Ŀ�еĿ�����ע�ᵽ������
            services.AddControllers();

            // �����û�����
            var configServer = this.Configuration.GetConnectionString("configServer");

            // ��Swagger��صķ���ע�ᵽ������
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "���Ա���",
                    Description = "XCore.WebAPI������",
                });

                // Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����

                var xmlPath = Path.Combine(basePath, "XCore.WebAPI.xml");
                c.IncludeXmlComments(xmlPath);
            });

            // ע����һ���Զ���ķ���
            services.AddScoped<MyService>();
            services.AddScoped<LoginService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var secretByte = Encoding.UTF8.GetBytes(Configuration["Authentication:secretKey"]);
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //ֻ�����õķ�����donesoft.cn�Żᱻ����
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Authentication:issuer"],

                    //ֻ�����õ�ʹ����donesoft.cn�Żᱻ����
                    ValidateAudience = true,
                    ValidAudience = Configuration["Authentication:audience"],

                    //��֤token�Ƿ����
                    ValidateLifetime = true,

                    //��������м���
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
            //    options.Password.RequireDigit = false;  // �����Ƿ�����������
            //    options.Password.RequireLowercase = false;  // �����Ƿ�������Сд��ĸ
            //    options.Password.RequireNonAlphanumeric = false;  // �����Ƿ�����������ĸ�����ַ�������ĸ�����ַ�����ĸ�����ַ�������ַ��� �ո񣬰ٷֺţ��»��ߣ����ߣ�ð�ţ��ֺŵȾ�Ϊ����ĸ�����ַ���
            //    options.Password.RequireUppercase = false;  // �����Ƿ���������д��ĸ
            //    options.Password.RequiredLength = 6;    // �������󳤶�Ϊ6
            //    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;  // ���������������õ����ʼ���ʹ�õ����ơ�
            //    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;  // �����������ʻ�ȷ�ϵ����ʼ���ʹ�õ����ơ�
            //});

            //var idBuilder = new IdentityBuilder(typeof(User), typeof(Role), services);
            //idBuilder.AddEntityFrameworkStores<IdDbContext>().AddDefaultTokenProviders().AddRoleManager<RoleManager<Role>>().AddUserManager<UserManager<User>>();
        }

        // �����м��
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                Debug.WriteLine("Ŀǰ�Ļ����ǿ�������");

                // �����������
                app.UseDeveloperExceptionPage();

                // �����м����������Swagger��ΪJSON�ս��
                app.UseSwagger();

                // �����м�������swagger - ui��ָ��Swagger JSON�ս��
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            if (env.IsProduction())
            {
                Console.WriteLine("Ŀǰ�Ļ�������������");
            }

            app.UseHttpsRedirection();

            //ʹ��·��,����ȥ����?
            app.UseRouting();

            // �ܷ��¼�ɹ�
            app.UseAuthentication();

            // ����Щ����Ȩ��
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
