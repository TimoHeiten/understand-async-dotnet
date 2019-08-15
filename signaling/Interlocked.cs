using System.Threading;

namespace signaling
{
    public static class InterlockedExample
    {
        public static void IncrementValueAtomic()
        {
            int j = 42;
            j++; // is not atomic
            for (int i = 0; i < 10; i++)
            {
                Interlocked.Increment(ref j);
            }
        }
    }
}