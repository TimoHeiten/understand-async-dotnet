using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace tasks
{
 
    internal class Relations
    {
    	public static Task ChildTasks()
        {
            var t = Task.Factory.StartNew(() => 
            {
                for (int i = 0; i < 3; i++)
                {
                    int next = i;
                    Task nested = Task.Factory.StartNew(() => System.Console.WriteLine($"nested task number:{next}"),
                    TaskCreationOptions.AttachedToParent);
                }
            });

            return t;
        }

    	public static Task Continue()
        {
            Task<string> first = Task.Run(() => { System.Console.WriteLine("from first task"); return "done"; });

            Task snd = first.ContinueWith(ft => System.Console.WriteLine($"text from first: {ft.Result}"));
            return snd;
        }

        public static Task ContinueOnState()
        {
            Task<string> first = Task.Run(() => { System.Console.WriteLine("from first task"); return "done"; });

            Task snd = first.ContinueWith(ft => System.Console.WriteLine($"text from first: {ft.Result}"), TaskContinuationOptions.OnlyOnCanceled);
            Task thrd = first.ContinueWith(ft => System.Console.WriteLine($"ran to completion {ft.Result}"), TaskContinuationOptions.OnlyOnRanToCompletion);
            snd.Wait();
            return thrd;
        }
    }
}