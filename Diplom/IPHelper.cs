using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Diplom
{
    internal class IPHelper
    {
        
        static public string IPSubNet(string address, string mask)
        {
            string ipAddress = "";
            string maskAddress = "";
            string subnet = "";

            var arrIpAddress = address.Split('.');
            var arrMaskAddres = mask.Split('.');

            for (int i = 0; i < arrIpAddress.Length; i++)
            { 
                ipAddress += ConvertHelper.ByteToBinaryString(Convert.ToByte(arrIpAddress[i]));
                maskAddress += ConvertHelper.ByteToBinaryString(Convert.ToByte(arrMaskAddres[i]));
            }

            for (int i = 0; i < ipAddress.Length; i++)
            {
                if (ipAddress[i] == '1' && maskAddress[i] == '1') subnet += "1";
                else subnet += 0;
            }

            return ConvertHelper.BinaryStringToIP(subnet);
        }

        static public List<IPAddress> IPAdressesList(string subNetIP, string lastIPAddress)
        {
            int count;
            var listOfIP = new List<IPAddress> ();
            var arrStartIPAddress = subNetIP.Split('.');
            var arrLastIPAddress = lastIPAddress.Split('.');

            int startIP = (Convert.ToInt32(arrStartIPAddress[0]) << 24 |
                           Convert.ToInt32(arrStartIPAddress[1]) << 16 |
                           Convert.ToInt32(arrStartIPAddress[2]) << 8  |
                           Convert.ToInt32(arrStartIPAddress[3]) );

            int lastIP =  (Convert.ToInt32(arrLastIPAddress[0]) << 24 |
                           Convert.ToInt32(arrLastIPAddress[1]) << 16 |
                           Convert.ToInt32(arrLastIPAddress[2]) << 8 |
                           Convert.ToInt32(arrLastIPAddress[3]));

            for (count = startIP; count <= lastIP; count++)
            {
                string ip = string.Format("{0}.{1}.{2}.{3}",
                           (count & 0xFF000000) >> 24,
                           (count & 0x00FF0000) >> 16,
                           (count & 0x0000FF00) >> 8,
                            count & 0x000000FF);
                listOfIP.Add(IPAddress.Parse(ip));
            }

            return listOfIP;
        }

        static public string LastIPAdress(string subNetIP, string maskIP)
        {
            string maskAddress = "";
            string subNetAddress = "";
            string maxIPAddress = "";
            int number = 0;

            var arrMaskAddress = maskIP.Split('.');
            var arrSubNetAddress = subNetIP.Split('.');

            for (int i = 0; i < arrMaskAddress.Length; i++)
            {
                maskAddress += ConvertHelper.ByteToBinaryString(Convert.ToByte(arrMaskAddress[i]));
                subNetAddress += ConvertHelper.ByteToBinaryString(Convert.ToByte(arrSubNetAddress[i]));
            }

            for (number = 0; number < maskAddress.Length; number++)
            {
                if (maskAddress[number] == '0') break;
            }

            maxIPAddress = subNetAddress.Substring(0, number);

            while (maxIPAddress.Length < 32) maxIPAddress += "1";

            return ConvertHelper.BinaryStringToIP(maxIPAddress);
        }

        
    }
}
