using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace tasks
{
    // start from this directory with 'dotnet run tasks.csproj <key>'
    class Program
    {
        static void Main(string[] args)
        {
            string key = string.Empty;
            try
            {   
                key = args[1];
                if (s_runner.ContainsKey(key) == false)
                {
                    string msg = "Key {0} not found! Try any of the following keys: {1}";
                    msg = string.Format(msg, key, "\n" + string.Join(",", s_runner.Select(x => $"'{x.Key}'")));
                    System.Console.WriteLine(msg);
                }
                else 
                {
                    Task task = s_runner[key].Invoke();
                    task.Wait();
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            System.Console.WriteLine("-".PadRight(50));
            System.Console.WriteLine($"ran with key: {key}");
            Thread.Sleep(500);
        }

         static Program()
        {
            s_runner = new Dictionary<string, Func<Task>>
            {
                ["task.cancel"] = Cancellation.Cancel,
                ["awaiter"] = DoAwaiter,
                ["data"] = DataSupply,
                ["download"] = Donwload.DownloadAsync,
                ["error"] = Error.RunError,
                ["error.correct"] = Error.RunErrorCorrectName,
            
                ["hello"] = HelloTask.RunTask,
                ["hello.foreground"] = HelloTask.RunTaskForeground,
                ["hello.inline"] = HelloTask.InlineStart,
                ["hello.hometurf"] = HelloTask.WhatIsMyHome,

                ["io"] = async () => 
                {
                     string s = await IOTask.WebPageAsync();
                },

                ["relation.child"] = Relations.ChildTasks,
                ["relation.continue"] = Relations.Continue,
                ["relation.continue.state"] = Relations.ContinueOnState,

                ["tcs"] = Tcs.SynthTasks

            };
        }

        private static Task DataSupply()
        {
            var s = "supplied data";
            var i = 42;

            Data.ReturnMultipliedByTwo(42);
            Data.Supply(s);

            return Task.CompletedTask;
        }   

        private static async Task DoAwaiter()
        {
            var tcs = new TaskCompletionSource<object>();
            Task t = tcs.Task;
            while (!t.IsCompleted)
            {
                int number = await AwaitIt.DoAwaitWithResult();
                System.Console.WriteLine($"number was {number}");

                if (number == 5)
                    tcs.SetResult(new object());
            }
        }

        private static Dictionary<string, Func<Task>> s_runner;
    }
}