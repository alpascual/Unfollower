using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;

namespace ConsoleProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient clientList = new WebClient();
            string sMyList = clientList.DownloadString("http://tweet.alsandbox.us/tweeps");
            clientList.Dispose();

            Console.WriteLine("Downloaded Users " + sMyList.Split(',').Length.ToString());

            int iCount = 0;
            foreach(string sUsername in sMyList.Split(','))
            {
                Console.WriteLine("Processing User " + sUsername + " is count " + iCount++);

                try
                {
                    WebClient client = new WebClient();
                    client.DownloadString("http://tweet.alsandbox.us/tweeps/ProcessOne?sUserName=" + sUsername);
                    Console.WriteLine("Finished with User " + sUsername);
                }
                catch
                {
                    Console.WriteLine("Exception happen with User " + sUsername);
                }
            }
        }
    }
}
