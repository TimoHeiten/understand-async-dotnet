using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace threads
{
    // just as reference
    internal static class ThreadResources
    {
        [ThreadStatic] static int x;
        //Each thread then sees a separate copy of _x.
        //Unfortunately, [ThreadStatic] doesn’t work with instance fields (it simply does nothing); nor does it play well with field initializers—they execute only once on the thread that’s running when the static constructor executes. If you need to work with instance fields—or start with a nondefault value—ThreadLocal<T> provides a better option.

        static ThreadLocal<int> x2 = new ThreadLocal<int>(() => 42);

        // third approach is to use GetData & SetData
        // Thread.GetData reads from threads isolated data store, Thread.SetData writes to it
        //  Both methods require a Local DataStoreSlot object to identify the slot. The same slot can be used across all threads and they’ll still get separate values

        // The same LocalDataStoreSlot object can be used across all threads.
       static LocalDataStoreSlot _secSlot = Thread.GetNamedDataSlot ("securityLevel");
       // other variant: LocalDataStoreSlot _secSlot = Thread.AllocateDataSlot();
       // This property has a separate value on each thread.
       static int SecurityLevel
       {
            get
            { 
                object data = Thread.GetData (_secSlot);
                return data == null ? 0 : (int) data;
            }
        }

        public static void Run()
        {
            
        }
    }
}