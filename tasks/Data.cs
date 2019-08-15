using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace tasks
{
 
    internal class Data
    {
        public static void Supply(string myData)
        {
            var t = Task.Factory.StartNew(() =>
                System.Console.WriteLine(myData) 
            );

            for (int i = 0; i < 10; i++)
            {
                //int captured = i;
                var task = Task.Factory.StartNew(() => System.Console.WriteLine(i));
            }
            Console.ReadLine();
        }

        public static int ReturnMultipliedByTwo(int input)
        {
            Task<int> t = Task.Factory.StartNew<int>(() => input*2);
            return t.Result;
        }
    }
}