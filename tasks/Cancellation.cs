using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace tasks
{
     internal class Cancellation
    {
    	public static async Task Cancel()
        {
            var tcs = new CancellationTokenSource();
            CancellationToken token = tcs.Token;

            Task task = RunAsync(token);
            Thread.Sleep(220);
            try
            {
                tcs.Cancel();
                await task;
            }
            catch (System.Exception)
            {
                System.Console.WriteLine($"state of task is {task.Status}");
            }
        }

        private static Task RunAsync(CancellationToken token)
        {
            return Task.Run (() => {
                while (!token.IsCancellationRequested)
                {
                    Thread.Sleep(70);
                    System.Console.WriteLine("no cancel yet");
                }
                System.Console.WriteLine("was canceled");
                token.ThrowIfCancellationRequested();
                // or do
                throw new OperationCanceledException("cancelled", token);
            });
        }
    }
}