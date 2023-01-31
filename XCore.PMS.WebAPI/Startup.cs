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

            // �����û�����
            GlobalConfig.DbConnectionString = this.Configuration.GetConnectionString("configServer");

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

                var xmlPath = Path.Combine(basePath, "XCore.PMS.WebAPI.xml");
                c.IncludeXmlComments(xmlPath);
            });

            // ��Ȩ
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
