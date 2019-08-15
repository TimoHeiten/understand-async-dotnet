using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace threads
{
    // start from this directory with 'dotnet run threads.csproj <key>'
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
                    s_runner[key].Invoke();
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
            s_runner = new Dictionary<string, Action>
            {
                ["thread.start"] = ThreadApi.StartAThread,
                ["thread.join"] = ThreadApi.JoinAThread,
                ["thread.stop"] = ThreadApi.StopAThread,
                ["thread.signaling"] = ThreadApi.Signaling,
                ["thread.exception.crash"] = ThreadApi.Exception,
                ["thread.exception.handle"] = ThreadApi.ExceptionHandling,

                ["thread.signal.turnstile"] = Signaling.Run,
                

            };
        }

        private static Dictionary<string, Action> s_runner;
    }
}