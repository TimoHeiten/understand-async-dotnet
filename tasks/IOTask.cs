using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace tasks
{
 
    internal class IOTask
    {
        public static string WebPage()
        {
            var request = WebRequest.Create("https://www.google.com");
            var response = request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }

        public static Task<string> WebPageAsync()
        {
            return Task.Factory.StartNew<string>(() => WebPage());
        }
    }
}