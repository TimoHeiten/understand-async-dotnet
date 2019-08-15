using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace signaling
{
 
    internal class NonExclusiveLocking
    {
        private static ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();
        public static void RunNewsChannel()
        {
           ParameterizedThreadStart onThread = item =>
            {
                int i = (int)item;
                if (i % 3 == 0)
                {
                    System.Console.WriteLine("writing");
                    WriteToLock($"news from {i}");
                }
                else
                {
                    System.Console.WriteLine("reading"); 
                    List<string> items = ReadNews();
                    System.Console.WriteLine(
                        string.Join(',', items)
                    );
                }
            };
            for (int i = 0; i < 18; i++)
            {
                new Thread(onThread).Start(i);
                Thread.Sleep(250);
            }
        }

        private static void WriteToLock(string next_news)
        {
            _rwLock.EnterWriteLock();
            try
            {
                news.Add(next_news);
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        }

          private static List<string> news = new List<string>()
        {
            "old news",
            "stale news",
            "fake news"
        };

          private static List<string> ReadNews()
        {
            bool result = _rwLock.TryEnterReadLock(40);
            try
            {
                if (result)
                    return news;
            }
            finally
            {
                _rwLock.ExitReadLock();
            }
            return new List<string>(){};
        }
    }
}