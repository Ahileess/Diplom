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
            string subNet, mask;
            Console.Write("Введите ИП-адрес: ");
            subNet = Console.ReadLine();
            

            Console.Write("Введите маску: ");
            mask = Console.ReadLine();
            Console.WriteLine(mask);
            //ip - 172.16.8.228
            //mask - "255.255.254.0";
            subNet = IPHelper.IPSubNet(subNet, mask);

            string lastIP = IPHelper.LastIPAdress(subNet, mask);

            List<IPAddress> list = IPHelper.IPAdressesList(subNet, lastIP);

            foreach (IPAddress ip in list)
            {
                Console.WriteLine(ip.ToString());
            }

            Pinging pinging = new Pinging(list);


            /*if (Pinging.ReplyAllIP.Count != 0)
            {
                PingReply reply = Pinging.ReplyAllIP.Last();
                Console.WriteLine(reply.Address.ToString());
            }*/
            
            CheckingPorts.Checking(Pinging.ReplyAllIP);

            foreach (string ip in CheckingPorts.IpList)
            {
                Console.WriteLine("After Check  " + ip);
                DeviceType.AddTypeToDB(ip);
            }


            Console.ReadLine();
        }
    }
}
