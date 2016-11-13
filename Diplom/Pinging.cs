using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace Diplom
{
    class Pinging
    {
        private static List<PingReply> replyAllIP;

        public static List<PingReply> ReplyAllIP
        {
            get { return replyAllIP; }
        }

        public Pinging(List<IPAddress> addresses)
        {
            replyAllIP = new List<PingReply>();
            foreach (var ip in addresses)
            {
                PingIPAsync(ip);
            }
            Console.WriteLine("Complete ping");
        }




        public static void PingIPAsync(IPAddress address)
        {
            byte[] buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            Console.WriteLine("Ping begin: " + address.ToString());
            AutoResetEvent waiter = new AutoResetEvent(false);
            Ping pingSender = new Ping();
            pingSender.PingCompleted += new PingCompletedEventHandler (PingCompletedCallack);
            PingOptions options = new PingOptions(128, true);
            pingSender.SendAsync(address, 200, buffer, waiter);
            waiter.WaitOne();
        }

        private static void PingCompletedCallack(object sender, PingCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Console.WriteLine("Ping canseled");
                ((AutoResetEvent)e.UserState).Set();
            }
            if (e.Error != null)
            {
                Console.WriteLine("Ping Error: " + e.Error.ToString());
                ((AutoResetEvent)e.UserState).Set();
            }
            PingReply reply = e.Reply;

            Console.WriteLine("ping Completed. Answer from " + reply.Address.ToString());
            if (reply.Status == IPStatus.Success)
            {
                replyAllIP.Add(reply);
                
            }
            Console.WriteLine("Status:  " + reply.Status.ToString());
            Console.WriteLine();
            ((AutoResetEvent)e.UserState).Set();
        }



    }
}
