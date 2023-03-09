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

            // ע��Identity���
            services.AddDbContext<IdDbContext>(opt =>
            {
                string connStr = this.Configuration.GetConnectionString("IdentityConfigServer");
                opt.UseMySql(connectionString: connStr, serverVersion: ServerVersion.AutoDetect(connStr));
            });

            services.AddDataProtection();
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;    // ��������������
                options.Password.RequireLowercase = false;    // ����������Сд��ĸ
                options.Password.RequireNonAlphanumeric = false;    // �����������������
                options.Password.RequireUppercase = false;    // ������������д��ĸ
                options.Password.RequiredLength = 6;    // ���볤������6λ
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;    // ������������ʼ�����ʽ
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;    // ȷ���ʼ���֤������ʼ�����ʽ
            });

            var idbuilder = new IdentityBuilder(typeof(User), typeof(Role), services);
            idbuilder.AddEntityFrameworkStores<IdDbContext>().AddDefaultTokenProviders().AddRoleManager<RoleManager<Role>>().AddUserManager<UserManager<User>>();

            // �����û�����
            GlobalConfig.DbConnectionString = this.Configuration["configServer"];

            // ��Swagger��صķ���ע�ᵽ������
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "���Ա���",
                    Description = "XCore.WebAPI������",
                });

                // ͨ����OpenAPI������ʵ�ִ�Swagger�з���Authorization����ͷ
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

                // Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����

                var xmlPath = Path.Combine(basePath, "XCore.PMS.WebAPI.xml");
                c.IncludeXmlComments(xmlPath);
            });

            // ����JWT
            services.Configure<JWTOptions>(Configuration.GetSection("JWT"));    // ע��JWT����
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var jwtOption = Configuration.GetSection("JWT").Get<JWTOptions>();
                var secretByte = Encoding.UTF8.GetBytes(jwtOption.SigningKey);
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //ֻ�����õķ�����donesoft.cn�Żᱻ����
                    ValidateIssuer = false,

                    //ֻ�����õ�ʹ����donesoft.cn�Żᱻ����
                    ValidateAudience = false,

                    //��֤token�Ƿ����
                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,

                    //��������м���
                    IssuerSigningKey = new SymmetricSecurityKey(secretByte)
                };
            });

            // ע��Log4Net
            services.AddLogging(m =>
            {
                // Ĭ�ϵ������ļ�·�����ڸ�Ŀ¼�����ļ���Ϊlog4net.config
                m.AddLog4Net();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
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
