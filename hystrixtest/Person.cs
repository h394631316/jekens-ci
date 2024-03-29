﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hystrixtest
{
    public class Person
    {
        [HystrixCommand(nameof(HelloFallBackAsync))]
        public virtual async Task<string> HelloAsync(string name)//需要是虚方法
        {
            Console.WriteLine("hello" + name);

            //抛错
            string s = null;
            s.ToString();
            await Task.FromResult(0);
            return "ok";
        }

        public async Task<string> HelloFallBackAsync(string name)
        {
            await Task.FromResult(0);
            Console.WriteLine("执行失败" + name);
            return "fail";
        }

        [HystrixCommand(nameof(AddFall))]
        public virtual int Add(int i, int j)
        {
            //抛错
            string s = null;
            s.ToString();

            return i + j;
        }

        public int AddFall(int i, int j)
        {
            return 0;
        }
    }
}
