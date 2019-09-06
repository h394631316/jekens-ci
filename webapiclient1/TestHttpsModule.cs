using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient;

namespace webapiclient1
{
    public static class TestHttpsModule
    {
        public static IServiceCollection Register(IServiceCollection services, IConfigurationRoot appConfiguration)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            void config(HttpApiConfig c)
            {
                c.HttpHost = new Uri(appConfiguration["TestServer:Url"]);//AppSettings.json 中的服务器地址
                //c.GlobalFilters.Add(new DefaultHeaderAttribute(appConfiguration));
            }

            services.AddHttpApi<ITestApi>().ConfigureHttpApiConfig(config);//注入接口

            return services;
        }
    }
}
