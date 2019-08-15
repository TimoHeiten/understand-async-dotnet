using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace signaling
{
    internal class LazyDude
    {
        public string Name { get; set; }
        public LazyDude()
        {
            // simulate long duration of creation
            Thread.Sleep(2000);
        }
    }

    public static class LazyOne
    {
        public static void CallTheLazyDude()
        {
            var lazy = new Lazy<LazyDude>();
            System.Console.WriteLine("did not wait by creating lazy<T>");
            System.Console.ReadKey();

            var timer = new Stopwatch();
            timer.Start();
            lazy.Value.Name = "Now this lazy dude has a name";
            timer.Stop();
            System.Console.Write("we actually had to wait 2 seconds now");
            System.Console.WriteLine($"stopwatch shows: {timer.ElapsedMilliseconds/1000} seconds");


            //2nd version
            var lazy2 = new Lazy<LazyDude>(() => new LazyDude{ Name = "Inital name from factory"});
            System.Console.WriteLine(lazy2.Value.Name);
        }
    }
}