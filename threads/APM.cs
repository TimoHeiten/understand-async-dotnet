using System;
using System.Threading;

namespace threads
{
    // just as reference
    public static class APM
    {
        static int GetResult(int input) => 42;
        static IAsyncResult BeginAsync(int input, out int result, AsyncCallback callback, object state)
        {
            result = input;
            return null;
        }
        static int EndAsync(out int result, IAsyncResult var)
        {
            result = 00;
            return 0;
        }


        public static void DemonstratePolling(){
            IAsyncResult iar = BeginAsync(42, out int result, null, null);
            var timer = new Timer((cb) => 
                {
                    if (iar.IsCompleted)
                    {
                        try
                        {
                            int output = EndAsync(out result, iar);
                            System.Console.WriteLine(output);
                        }
                        catch
                        {
                            // log or something
                        }
                    }
                }
            );
        }
        public static void DemonstrateNotification(){
            BeginAsync(22, out int result, new AsyncCallback(Callback), new { Name = "state" });
        }

        private static void Callback(IAsyncResult iar)
        {
            // do something here
            try
            {
                int result = EndAsync(out int output, iar);
                System.Console.WriteLine(result);
            }
            catch(Exception)
            {
                // always necessary 
            }

        }

    }
}