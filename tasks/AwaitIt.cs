using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace tasks
{
    internal class AwaitIt
    {
    	public static async void FireAndForget()
        {
            await Task.Delay(200);
            System.Console.WriteLine("write to some not crucial system");
        }

        public static async Task<int> DoAwaitWithResult()
        {
            int next = new Random().Next(0,10);
            await Task.Delay(100);
            return next;
        }
    }
}