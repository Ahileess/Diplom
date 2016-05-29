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
        private static Dictionary<int, string> portsTcpConnection;
        private static Dictionary<int, string> portsUdpConnection;
        private static DataBase db;

        static CheckingPorts()
        {
            portsTcpConnection = new Dictionary<int, string>();
            portsUdpConnection = new Dictionary<int, string>();
            AddPorts();
        }

        public static Dictionary<int, string> PortsTcpConnection
        {
            get { return portsTcpConnection; }
        }

        public static Dictionary<int, string> PortsUdpConnection
        {
            get { return portsUdpConnection; }
        }

        public static void Checking(List<PingReply> replys)
        {
            db = new DataBase();
            foreach (var reply in replys)
            {
                var address = reply.Address;
                Console.WriteLine("\n{0} адрес поддерживает службы:\n", address);
                Console.WriteLine("\n Tcp соединение");
                Parallel.ForEach(portsTcpConnection, (port) => 
                {
                    if (IsOnlineTcp(address, port.Key))
                    {
                        Console.WriteLine("{0} на {1} порту", port.Value, port.Key);
                        db.AddRecord(address.ToString(), port.Key.ToString(), port.Value, "TCP");
                    }
                });
                    
                    /*foreach (var port in portsTcpConnection)
                {
                    if (IsOnlineTcp(address, port.Key))
                    {
                        Console.WriteLine("{0} на {1} порту", port.Value, port.Key);
                        db.AddRecord(address.ToString(), port.Key.ToString(), port.Value, "TCP");
                    }
                }*/

                Console.WriteLine("\n Udp соединение");
                foreach (var port in portsUdpConnection)
                {
                    if (IsOnlineUdp(address, port.Key))
                    {
                        Console.WriteLine("{0} на {1} порту", port.Value, port.Key);
                        db.AddRecord(address.ToString(), port.Key.ToString(), port.Value, "UDP");
                    }
                }
            }
        }

        public static void ShowSelectedPorts()///нет Udp
        {
            foreach (var port in portsTcpConnection)
            {
                Console.WriteLine("{0} on {1} port", port.Value, port.Key);
            }
        }

        public static void AddPort(int port, string service)//нет Udp
        {
            foreach (int key in portsTcpConnection.Keys)             //можно использовать лямбда-выражение.
            {
                if (key == port)
                {
                    Console.WriteLine("Этот порт уже внесен в список");
                    return;
                }
            }                                           //конец
            portsTcpConnection.Add(port, service);
        }

        private static void AddPorts()
        {
            portsTcpConnection.Add(20, "FTP-DATA");
            portsTcpConnection.Add(21, "FTP");
            portsTcpConnection.Add(22, "SSH");
            portsTcpConnection.Add(23, "Telnet");
            portsTcpConnection.Add(24, "PRIV-MAIL");
            portsTcpConnection.Add(25, "SMTP");
            portsTcpConnection.Add(53, "Domain Name System");
            portsTcpConnection.Add(80, "HTTP");
            portsTcpConnection.Add(8080, "HTTP alternate");
            portsTcpConnection.Add(110, "POP3");
            portsTcpConnection.Add(119, "NNTP");
            portsTcpConnection.Add(135, "EPMAP");
            portsTcpConnection.Add(137, "NETBIOS-NS");
            portsTcpConnection.Add(138, "NETBIOS-DGM");
            portsTcpConnection.Add(139, "NETBIOS-SSN");
            portsTcpConnection.Add(143, "IMAP");
            portsTcpConnection.Add(443, "HTTPS");
            portsTcpConnection.Add(3389, "Microsoft Terminal Server");

            portsUdpConnection.Add(161, "SNMP");
            portsUdpConnection.Add(162, "SNMPTRAP");
        }

        private static bool IsOnlineTcp(IPAddress ip, int port)
        {
            var clientTcp = new TcpClient();
            try
            {
                clientTcp.Connect(ip, port);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsOnlineUdp(IPAddress ip, int port)
        {
            var clientUdp = new UdpClient();
            try
            {
                clientUdp.Connect(ip, port);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
