using System;
using System.Collections.Generic;
using System.Text;

namespace webapiclient1
{
    public class AjaxResponse<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public T data { get; set; }
        public int count { get; set; }
    }
}
