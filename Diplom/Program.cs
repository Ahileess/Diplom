using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace Diplom
{
    class Program
    {
        static void Main(string[] args)
        {
            string mask = "255.255.255.248";
            string subNet = IPHelper.IPSubNet("192.168.0.100", mask);

            string lastIP = IPHelper.LastIPAdress(subNet, mask);

            List<IPAddress> list = IPHelper.IPAdressesList(subNet, lastIP);

            foreach (IPAddress ip in list)
            {
                Console.WriteLine(ip.ToString());
            }

            Pinging pinging = new Pinging(list);


            if (Pinging.ReplyAllIP.Count != 0)
            {
                PingReply reply = Pinging.ReplyAllIP.Last();
                Console.WriteLine(reply.Address.ToString());
            }
            
            CheckingPorts.Checking(Pinging.ReplyAllIP);

            

        }
    }
}
