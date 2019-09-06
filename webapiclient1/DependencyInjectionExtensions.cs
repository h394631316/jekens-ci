using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient;

namespace webapiclient1
{
    /// <summary>
    /// 基于DependencyInjection的扩展
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// 添加HttpApi
        /// 返回HttpApi工厂
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static HttpApiFactoryBuilder<TInterface> AddHttpApi<TInterface>(this IServiceCollection services)
            where TInterface : class, IHttpApi
        {
            return new HttpApiFactoryBuilder<TInterface>(services);
        }
    }
}
