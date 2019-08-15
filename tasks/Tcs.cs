using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace tasks
{
 
    internal class Tcs
    {
    	public static Task SynthTasks()
        {
            var tcs = new TaskCompletionSource<int>();
            Task<int> synth = tcs.Task;
            synth.ContinueWith(t => System.Console.WriteLine($"from synth task: {t.Result}"));
            
            System.Console.WriteLine("tap a key to return task result");
            Console.ReadKey();
            tcs.SetResult(42);
            return tcs.Task;
        }

        public static Task<TResult> CustomRun<TResult> (Func<TResult> func)
        {
            var tcs = new TaskCompletionSource<TResult>(); 
            new Thread (() =>
            {
                try 
                {
                    tcs.SetResult (func());
                }
                catch (Exception ex)
                {
                    tcs.SetException (ex); 
                }
            })
            { IsBackground = true }
            .Start();

            return tcs.Task;
        }
    }
}