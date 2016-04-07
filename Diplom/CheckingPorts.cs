using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    static class CheckingPorts
    {
        private static Dictionary<int, string> ports;

        static CheckingPorts()
        {
            ports = new Dictionary<int, string>();
            AddPorts();
        }

        public static Dictionary<int, string> Ports
        {
            get { return ports; }
        }

        public static void Checking(List<PingReply> replys)
        {
            foreach (var reply in replys)
            {
                var address = reply.Address;
                Console.WriteLine("\n{0} адрес поддерживает службы:\n", address);
                foreach (var port in ports)
                {
                    if (IsOnline(address, port.Key))
                    {
                        Console.WriteLine("{0} на {1} порту", port.Value, port.Key);
                    }
                }
            }
        }

        public static void ShowSelectedPorts()
        {
            foreach (var port in ports)
            {
                Console.WriteLine("{0} on {1} port", port.Value, port.Key);
            }
        }

        public static void AddPort(int port, string service)
        {
            foreach (int key in ports.Keys)             //можно использовать лямбда-выражение.
            {
                if (key == port)
                {
                    Console.WriteLine("Этот порт уже внесен в список");
                    return;
                }
            }                                           //конец
            ports.Add(port, service);
        }

        private static void AddPorts()
        {
            ports.Add(20, "FTP-DATA");
            ports.Add(21, "FTP");
            ports.Add(22, "SSH");
            ports.Add(23, "Telnet");
            ports.Add(24, "PRIV-MAIL");
            ports.Add(25, "SMTP");
            ports.Add(53, "Domain Name System");
            ports.Add(80, "HTTP");
            ports.Add(8080, "HTTP alternate");
            ports.Add(110, "POP3");
            ports.Add(119, "NNTP");
            ports.Add(135, "EPMAP");
            ports.Add(137, "NETBIOS-NS");
            ports.Add(138, "NETBIOS-DGM");
            ports.Add(139, "NETBIOS-SSN");
            ports.Add(143, "IMAP");
            ports.Add(443, "HTTPS");
            ports.Add(3389, "Microsoft Terminal Server");
        }

        private static bool IsOnline(IPAddress ip, int port)
        {
            TcpClient client = new TcpClient();
            try
            {
                client.Connect(ip, port);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
