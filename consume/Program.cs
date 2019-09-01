using RestTools;
using System;

namespace consume
{
    class Program
    {
        static void Main(string[] args)
        {
            RestTemplate rest = new RestTemplate("http://127.0.0.1:8500");
            //RestTemplate把服务的解析和发请求以及响应反序列化帮我们完成
            ResponseEntity<string[]> resp = rest.GetForEntityAsync<string[]>("http://apiservice1/api/values").Result;
            Console.WriteLine(resp.StatusCode);
            Console.WriteLine(string.Join(",", resp.Body));

            Console.ReadKey();
        }
    }
}
