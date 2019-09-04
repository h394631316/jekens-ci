using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ID4.IdServer
{
    public class Config
    {
        public static IConfiguration Configuration;

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        /// <summary>
        /// Define which uses will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TestUser> GetUsers()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "10001",
                    Username = "edison@hotmail.com",
                    Password = "edisonpassword"
                },
                new TestUser
                {
                    SubjectId = "10002",
                    Username = "andy@hotmail.com",
                    Password = "andypassword"
                },
                new TestUser
                {
                    SubjectId = "10003",
                    Username = "leo@hotmail.com",
                    Password = "leopassword"
                }
            };
        }

        /// <summary> 
        /// 返回应用列表 
        /// </summary>         
        /// <returns></returns>         
        public static IEnumerable<ApiResource> GetApiResources()
        {
            List<ApiResource> resources = new List<ApiResource>();
            //ApiResource第一个参数是应用的名字，第二个参数是显示名字 
            resources.Add(new ApiResource("chatapi", "我的聊天软件"));
            resources.Add(new ApiResource("rpandroidapp", "安卓app"));
            resources.Add(new ApiResource("bdxcx", "百度小程序"));

            resources.Add(new ApiResource("clientservice", "CAS Client Service"));
            resources.Add(new ApiResource("productservice", "CAS Product Service"));
            resources.Add(new ApiResource("agentservice", "CAS Agent Service"));

            return resources;
        }

        /// <summary> 
        /// 返回账号列表 
        /// </summary>         
        /// <returns></returns> 
        public static IEnumerable<Client> GetClients()
        {
            Console.WriteLine($"http://{Configuration["Clients:MvcClient:IP"]}:{Configuration["Clients:MvcClient:Port"]}/signin-oidc");
            Console.WriteLine($"http://{Configuration["Clients:MvcClient:IP"]}:{Configuration["Clients:MvcClient:Port"]}/signout-callback-oidc");

            List<Client> clients = new List<Client>()
            {
                new Client
                {
                    ClientId = "wyt",//用户名 
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("123321".Sha256())//秘钥 
                    },
                    AllowedScopes = { "chatapi", "rpandroidapp" }//这个账号支持访问哪些应用 
                },
                new Client
                {
                    ClientId = "cas.mvc.client.implicit",
                    ClientName = "CAS MVC Web App Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { $"http://{Configuration["Clients:MvcClient:IP"]}:{Configuration["Clients:MvcClient:Port"]}/signin-oidc" },
                    PostLogoutRedirectUris = { $"http://{Configuration["Clients:MvcClient:IP"]}:{Configuration["Clients:MvcClient:Port"]}/signout-callback-oidc" },
                    AllowedScopes = new[] {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "agentservice", "clientservice", "productservice"
                    },
                    //AccessTokenLifetime = 3600, // one hour
                    AllowAccessTokensViaBrowser = true // can return access_token to this client
                },

                new Client
                {
                    ClientId = "client.api.service",
                    ClientSecrets = new [] { new Secret("clientsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "clientservice" }
                },
                new Client
                {
                    ClientId = "product.api.service",
                    ClientSecrets = new [] { new Secret("productsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "clientservice", "productservice" }
                },
                new Client
                {
                    ClientId = "agent.api.service",
                    ClientSecrets = new [] { new Secret("agentsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "agentservice", "clientservice", "productservice" }
                }
            };

            return clients;
        }

    }
}
