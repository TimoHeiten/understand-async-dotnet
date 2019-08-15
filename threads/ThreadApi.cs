using System;
using System.Threading;

namespace threads
{
    public class ThreadApi
    {
        private static readonly Action<string> write = Console.WriteLine;
        public static void StartAThread()
        {
            var thread = new Thread(RunAThread);
            var lambda = new Thread(() => write("from lambda thread"));

            // also useful to push data into a thread
            int _42 = 42;
            var lambda2 = new Thread(() => write(_42.ToString()));
            lambda.Start();
            lambda2.Start();
            thread.Start();
            write("done");
            // foreground and background 
            // Threads created with the thread class are by default foreground tasks
            // --> IsBackground property needs to be set to True before starting the thread

            Thread worker = new Thread ( () => Console.ReadLine());
            worker.IsBackground = true;
            worker.Start();

            // while waiting with sleep or on join a thread is BLOCKED!
            // execution is paused for some reason. yields its processor time slice
            // test for it with
            // bool blocked = (t.ThreadState & ThreadState.WaitSleepJoin) != 0;
            // bit flag
        }

        private static void RunAThread() => write("from the created thread");

        public static void JoinAThread()
        {
            // Join
            ParameterizedThreadStart _doWork = (param) => 
            {
                 for (int i=0; i< 100; i++) 
                 {
                     write(i.ToString()); 
                 }
            };
            
            var t = new Thread(_doWork);
            t.Start();
            write("next up main thread gets blocked");
            t.Join();
            write("join happened");
            // Join with Timeout

            var t2 = new Thread(() => Thread.Sleep(10)); //10000
            t2.Start();
            bool completed = t2.Join(millisecondsTimeout: 4); // 400

            string inline = completed ? "ran to end" : "timed out";
            write($"join {inline}");
            
            // Sleep (pause current thread)
            // Thread.Sleep(TimeSpan.FromSeconds(2));
            // Thread.Sleep(0); // --> preempts threads current time slice immediately
            // Thread.Yield(); // does the same yet only on same processor core and is helpful for bugs, if this breaks your code
            // you certainly have a bug somewhere
        }


        public static void StopAThread()
        {
            // 5 ways
            // stop process
            // stop all foreground tasks ( which in turn stops the process and all tasks)
            // Interrupt --> no reason and ill advised because indeterministic
                // releases a blocked thread
            // Abort also indeterministic, tries to force to end another thread. needs also to be Thread.ResetAbort in catch else it is rethrown
            // most common: function delegate simply returns.
            // Poll for flag
            var stopper = new ThreadStopper();
            var rand = new Random();
            int exit = 0;
            while(!stopper.Terminate)
            {
                exit = rand.Next(0,10);
                if (exit == 5)
                {
                    stopper.Terminate = true;
                }
                write("another round! before termination");
            }
            write("stopped!");
        }

        public class ThreadStopper
        {
            public bool Terminate { get; set; }
            public ThreadStopper()
            {
                var monitored = new Thread(new ThreadStart(Supply));
                monitored.Start();
            }
            private void Supply() 
            {
                while(!Terminate)
                {
                    if (Terminate)
                        return;
                    Thread.Sleep(300);
                }
                System.Console.Write("Terminated the thread"); 
            }
        }

        public static void Signaling()
        {
            // we will look closely at this in upcoming videos, just as a teaser:
            var signal = new ManualResetEvent (false);
            new Thread (() =>
            {
                Console.WriteLine ("Waiting for signal...");
                signal.WaitOne();
                signal.Dispose();
                Console.WriteLine ("Got signal!");
            })
            .Start();
            Thread.Sleep(2000);
            signal.Set(); // "Open" the signal
        }

        public static void ExceptionHandling()
        {
            try
            {
                new Thread((par) => throw null).Start();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("will never be called");
                throw;
            }
        }

        internal static void Exception()
        {
            new Thread(Go).Start();
        }

        private static void Go()
        {
            try
            {
                throw null;
            }
            catch (System.Exception ex)
            {
                // do something useful in general scenario
                System.Console.WriteLine($"this time it is called {ex.Message}");
            }
        }


        public static void UseThePooling()
        {
            // 1
            ThreadPool.QueueUserWorkItem(someParam => 
                System.Console.WriteLine("this runs as background on thread pool"));
            // 2
            System.Threading.Tasks.Task.Run(() => write("we will later see how tasks work"));

            // 3 
            // next video deals with APM extensively
        }
    }
}