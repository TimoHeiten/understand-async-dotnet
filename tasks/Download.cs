using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Threading;

namespace tasks
{
    internal class Donwload
    {
        public static async Task DownloadAsync()
        {
            string url = "https://www.google.com/";
            var thread = Thread.CurrentThread;
            System.Console.WriteLine($"current thread: {thread.ManagedThreadId} and is threadpool {thread.IsThreadPoolThread}");
            
            var req = WebRequest.Create(url);
            var respo = await req.GetResponseAsync();
            
            thread = Thread.CurrentThread;
            System.Console.WriteLine($"current thread: {thread.ManagedThreadId} and is threadpool {thread.IsThreadPoolThread}");

            using (var reader = new StreamReader(respo.GetResponseStream()))
            {
                string result = await reader.ReadToEndAsync();
                System.Console.WriteLine($"write to console: {Chop(result)}");
            }
        }

        private static string Chop(string s)
        {
            return s.Substring(0, 50).PadRight(53, '.');
        }
    }
}