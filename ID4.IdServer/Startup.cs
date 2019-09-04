using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace ID4.IdServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            Config.Configuration = this.Configuration;

            services.AddIdentityServer()
                //.AddDeveloperSigningCredential() //系统提供的默认密钥对进行token签名，生产环境需要提供正确的证书 

                .AddSigningCredential(new X509Certificate2(Path.Combine(basePath,
                    Configuration["Certificates:CerPath"]),
                    Configuration["Certificates:Password"]))
               
               .AddTestUsers(Config.GetUsers().ToList())
               .AddInMemoryIdentityResources(Config.GetIdentityResources())
               .AddInMemoryApiResources(Config.GetApiResources())
               .AddInMemoryClients(Config.GetClients());

            // for QuickStart-UI
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            app.UseIdentityServer();

            // for QuickStart-UI
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine($"{basePath}");
            app.UseStaticFiles();

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(basePath),
            //    RequestPath = "/StaticFiles"
            //});

            app.UseMvcWithDefaultRoute();
        }
    }
}
