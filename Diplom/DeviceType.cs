using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    static class DeviceType
    {
        static private DataBase db;
        static private List<string> LinuxServer;
        static private List<string> LinuxServer2;
        static private List<string> Windows8;
        static private List<string> WindowsServer2003;
        static private List<string> WindowsServer2012;
        static private List<string> PrinterXerox3300;
        //static private List<string> PrinterXerox3300_2;

        static DeviceType()
        {
            LinuxServer = new List<string>();
            LinuxServer.Add("NETBIOS-SSN");
            LinuxServer.Add("SNMP");
            LinuxServer.Add("SNMPTRAP");
            LinuxServer.Add("FTP");
            LinuxServer.Add("SSH");
            LinuxServer.Add("SMTP");
            LinuxServer.Add("HTTPS");
            LinuxServer.Add("Domain Name System");
            LinuxServer.Add("HTTP");

            LinuxServer2 = new List<string>();
            LinuxServer2.Add("HTTP");
            LinuxServer2.Add("SSH");
            LinuxServer2.Add("HTTPS");
            LinuxServer2.Add("SNMP");
            LinuxServer2.Add("SNMPTRAP");

            Windows8 = new List<string>();
            Windows8.Add("HTTP");
            Windows8.Add("HTTPS");
            Windows8.Add("EPMAP");
            Windows8.Add("Microsoft Terminal Server");
            Windows8.Add("NETBIOS-SSN");
            Windows8.Add("SNMP");
            Windows8.Add("SNMPTRAP");


            WindowsServer2003 = new List<string>();
            WindowsServer2003.Add("EPMAP");
            WindowsServer2003.Add("NETBIOS-SSN");
            WindowsServer2003.Add("SNMPTRAP");
            WindowsServer2003.Add("Microsoft Terminal Server");
            WindowsServer2003.Add("SNMP");
            WindowsServer2003.Add("HTTPS");
            WindowsServer2003.Add("HTTP");
            WindowsServer2003.Add("HTTP alternate");

            WindowsServer2012 = new List<string>();
            WindowsServer2012.Add("EPMAP");
            WindowsServer2012.Add("NETBIOS-SSN");
            WindowsServer2012.Add("SNMPTRAP");
            WindowsServer2012.Add("Microsoft Terminal Server");
            WindowsServer2012.Add("SNMP");
            WindowsServer2012.Add("SMTP");
            WindowsServer2012.Add("HTTP alternate");

            PrinterXerox3300 = new List<string>();
            PrinterXerox3300.Add("SNMPTRAP");
            PrinterXerox3300.Add("SNMP");
            PrinterXerox3300.Add("HTTPS");
            PrinterXerox3300.Add("HTTP");


        }

        static string TypeDefenition(List<string> services)
        {
            if (CheckType(services, LinuxServer)) return "LinuxServer";
            if (CheckType(services, LinuxServer2)) return "LinuxServer";
            if (CheckType(services, Windows8)) return "Windows8";
            if (CheckType(services, WindowsServer2003)) return "Windows Server 2003";
            if (CheckType(services, WindowsServer2012)) return "Windows Server 2012";
            if (CheckType(services, PrinterXerox3300)) return "Printer Xerox 3300";
            return "unknown";
        }

        static public void AddTypeToDB(string address)
        {
            List<string> services = new List<string>();
            
            db = new DataBase(false);
            db.SelectData(address, services);
            var type = TypeDefenition(services);
            
            db.AddRecordTypeDevice(address, type);
            db.Close();
        }


        static bool CheckType(List<string> first, List<string> second)
        {
            return new HashSet<string>(first).SetEquals(second);
        }



    }
}
