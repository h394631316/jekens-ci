using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace smsservice2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        IConfiguration Configuration;
        public SMSController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        [Route("Send")]
        public bool Send(string msg)
        {
            string ip = Configuration["ip"]; //部署到不同服务器的时候不能写成127.0.0.1 或者 0.0.0.0，因为这是让服务消费者调用的地址
            int port = int.Parse(Configuration["port"]);

            string value = Request.Headers["X-Hello"];
            Console.WriteLine($"x-hello={value}");
            Console.WriteLine($"发送短信 {msg}, power by ip={ip}, port={port} .");
            return true;
        }
    }
}