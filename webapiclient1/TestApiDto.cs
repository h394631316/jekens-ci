using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient.DataAnnotations;

namespace webapiclient1
{
    [Serializable]
    public class TestApiDto
    {
        /// <summary>
        /// 序列号
        /// </summary>
        [AliasAs("serialNum")]
        public string SerialNumber { get; set; }
    }
}
