using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace tasks
{
 
    internal class Error
    {
        public static async Task RunError()
        {
            var t = Task.Run(() => throw new ArgumentNullException());

            try
            {
                await t;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"exception of type {ex.GetType()}");
            }
        }

    	public static async Task RunErrorCorrectName()
        {
             var t = Task.Run(() => throw new ArgumentNullException());

            try
            {
                await t;
            }
            catch (System.AggregateException ex)
            {
                foreach (var item in ex.Flatten().InnerExceptions)
                {
                    System.Console.WriteLine($"exception of type {item.GetType()}");
                }
            }
        }
    }
}