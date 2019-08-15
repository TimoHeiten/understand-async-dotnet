using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace signaling
{
    // start from this directory with 'dotnet run signaling.csproj <key>'
    class Program
    {
        static void Main(string[] args)
        {
            string key = string.Empty;
            System.Console.WriteLine("This project is mostly for reference only.");
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
                ["non.exclusive.news"] = NonExclusiveLocking.RunNewsChannel,
                ["lazy"] = LazyOne.CallTheLazyDude

            };
        }

        private static Dictionary<string, Action> s_runner;
    }
}