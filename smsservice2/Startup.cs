﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace smsservice2
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            string ip = Configuration["ip"]; //部署到不同服务器的时候不能写成127.0.0.1 或者 0.0.0.0，因为这是让服务消费者调用的地址
            int port = int.Parse(Configuration["port"]);

            //向consul注册服务
            ConsulClient client = new ConsulClient(config =>
            {
                config.Address = new Uri("http://127.0.0.1:8500");
            });

            Task<WriteResult> result = client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "duanxin2" + Guid.NewGuid(),//服务编号，不能重复，用Guid最简单
                Name = "duanxin2",//服务的名字
                Address = ip,//我的ip地址（可以被其他应用访问的地址，本地测试用127.0.0.1，机房环境中一定要写自己的内网ip地址）
                Port = port,//我的端口
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务停止多久后反注册
                    Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                    HTTP = $"http://{ip}:{port}/api/health",//健康检查地址
                    Timeout = TimeSpan.FromSeconds(5),
                }
            });
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
            app.UseMvc();
        }
    }
}
