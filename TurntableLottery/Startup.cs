using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurntableLottery.AuthHelp;
using TurntableLottery.Configuration;
using TurntableLottery.Token;
using TurntableLottery.Token.Model;

namespace TurntableLottery
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //��ӱ���·����ȡ֧��
            services.AddSingleton(new AppSettingsHelper(Env.ContentRootPath));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TurntableLottery", Version = "v1" });
                //���ע�ͷ���
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "TurntableLottery.xml");
                c.IncludeXmlComments(xmlPath);
            });
            #region Token
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });

            //jwt��Ȩ֧��ע��
            services.AddAuthorizationSetup();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TurntableLottery v1"));
            }

            #region TokenAuth
            //app.UseMiddleware<TokenAuth>();
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();
            //���ڷ���wwwroot�µ��ļ�
            app.UseStaticFiles();
            // �ȿ�����֤
            app.UseAuthentication();
            // Ȼ������Ȩ�м��
            app.UseAuthorization();           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
