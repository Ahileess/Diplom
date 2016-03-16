using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Diplom
{
    class Program
    {
        static void Main(string[] args)
        {
            string mask = "255.255.255.0";
            string subNet = IPHelper.IPSubNet("192.168.0.3", mask);
            Console.WriteLine(subNet);

            string lastIP = IPHelper.LastIPAdress(subNet, mask);
            Console.WriteLine(lastIP);

            List<IPAddress> list = IPHelper.IPAdressesList(subNet, lastIP);

            foreach (IPAddress ip in list)
            {
                Console.WriteLine(ip.ToString());
            }

        }
    }
}
