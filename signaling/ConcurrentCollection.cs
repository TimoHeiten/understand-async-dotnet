using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace signaling
{

    // just as reference
    internal class ConcurrentCollections
    {
        Queue<int> queue = new Queue<int>();

        //...

        private int GetNext()
        {
            if (queue.Count > 0)
                return queue.Dequeue();
            else
                return -1;
        }

        ConcurrentQueue<int> con_queue = new ConcurrentQueue<int>();

        private int ThreadSafeNext()
        {
            int val;
            if (con_queue.TryDequeue(out val))
                return val;
            else
                return -1;
        }
    }
}