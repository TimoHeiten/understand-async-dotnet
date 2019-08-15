using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace tasks
{
    internal static class HelloTask
    {
        public static async Task RunTask()
        {
            var task = new Task(() => System.Console.WriteLine("from task"));
            await task;
            //task.Wait();
        }

        public static Task RunTaskForeground()
        {
            var tcs = new TaskCompletionSource<object>();
            var thread = new Thread(() => System.Console.WriteLine("Hello foreground Task"));
            thread.Start();
            thread.Join();
            return tcs.Task;
        }

        public static Task InlineStart()
        {
            Task t = Task.Factory.StartNew(() => System.Console.WriteLine("ez starter"));
            // or even easier
            //Task t2 = Task.Run(() => System.Console.WriteLine("running like the wind"));

            return t;
        }
        private static void xx()
        {
            var t = new Task(() => System.Console.WriteLine("run 1"));
            t.Start();

            Task.Factory.StartNew(() => System.Console.WriteLine("run with factory"));
            Task.Run(() => System.Console.WriteLine("run 2"));
        }

        public static Task WhatIsMyHome()
        {
            Task t = Task.Factory.StartNew(() => 
            {
                bool isThreadPool = Thread.CurrentThread.IsThreadPoolThread;
                string threadType =isThreadPool ? "Threadpool" : "Custom";
                System.Console.WriteLine($"I am a {threadType} thread");
            });
            return t;
        }
    }
}