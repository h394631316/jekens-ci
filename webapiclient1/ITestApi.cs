using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient;
using WebApiClient.Attributes;

namespace webapiclient1
{
    [TraceFilter(OutputTarget = OutputTarget.Console)]//这个设置可以将日志输入到控制台
    public interface ITestApi : IHttpApi
    {
        ///// <summary>
        ///// 创建xxx信息
        ///// </summary>
        ///// <param name="input">实体</param>
        ///// <returns></returns>
        //[Timeout(10000)]
        //[HttpPost("/api/v2/x/xxx")]
        //ITask<AjaxResponse<List<TestApiDto>>> GetxxxPagedList([JsonContent] TestDto input);

        ///// <summary>
        ///// 获取xxx信息
        ///// </summary>
        ///// <param name="pageIndex">页码</param>
        ///// <returns></returns>
        //[Timeout(10000)]
        //[HttpGet("/api/v2/x/xxx")]
        //ITask<AjaxResponse<List<TestApiDto>>> GetxxxPagedList(int pageIndex);

        [HttpGet("/api/values")]
        ITask<IEnumerable<string>> GetValues();
    }
}
