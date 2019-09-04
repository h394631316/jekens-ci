using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

namespace ocelotserver2
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services
                .AddOcelot(Configuration)
                .AddConsul()
                .AddPolly();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            //app.UseMvc();

            var configuration = new OcelotPipelineConfiguration
            {
                PreErrorResponderMiddleware = async (ctx, next) =>
                {
                    Console.WriteLine("in OcelotPipelineConfiguration");
                    //String token = ctx.HttpContext.Request.Headers["token"].FirstOrDefault();//这里可以进行接收的客户端token解析转发
                    ctx.HttpContext.Request.Headers.Add("X-Hello", "666");
                    await next.Invoke();
                    Console.WriteLine("out OcelotPipelineConfiguration");
                }
            };

            app.UseOcelot(configuration).Wait(); // 不要忘记了写 wait
        }
    }
}
