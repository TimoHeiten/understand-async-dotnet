using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace threads
{
    internal class Signaling
    {
       private static EventWaitHandle _turnstile = new AutoResetEvent(initialState: false);

       public static void Run()
       {
           new Thread(Waiter).Start();
           Thread.Sleep(1000);
           System.Console.WriteLine("OPEN THE GATES!!!");

           _turnstile.Set();
       }

        private static void Waiter(object state)
        {
            System.Console.WriteLine("Waiting...");
            _turnstile.WaitOne();
            System.Console.WriteLine("Notified");
        }
    }
}