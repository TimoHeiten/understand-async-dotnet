using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace signaling
{
    internal class ExclusiveLocking
    {
      private static int _val1 = 1;
      private static int _val2 = 2;
      private static void NotThreadSafe()
      {
        if (_val2 != 0) 
        {
            Console.WriteLine (_val1 / _val2);
        }
        
        _val2 = 0;
      }

      private static readonly object _locker = new object();
      private static void ThreadSafeWithLock()
      {
          try
            {
                lock (_stateGuard)
                {
                    if (_val2 != 0)
                    {
                        System.Console.WriteLine("val2 != 0");
                        if (wait)
                        {
                            Thread.Sleep(500);
                        }
                        var val = _val1/_val2;
                    }
                }
                System.Console.WriteLine("ran without error");
            }
            catch (System.DivideByZeroException divZero)
            {
                System.Console.WriteLine("threw divide by zero ex" + divZero.Message);
            }
            _val2 = 0;
            
      }
    }
}