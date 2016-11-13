using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    class ConvertHelper
    {
        static private byte[] bitValue = { 128, 64, 32, 16, 8, 4, 2, 1 };
        static private int ipLength = 32;
        /// <summary>
        ///  преобразование байта в битовую строку
        /// </summary>
        /// <param name="ipi">один разряд ип адреса</param>
        /// <returns>8-ми значная битовая строка </returns>
        static public string ByteToBinaryString(byte ipi)
        {
            string rc = "";
            ipi = Convert.ToByte(ipi);
            for (int i = 0; i < bitValue.Length; i++)
            {
                if (ipi >= bitValue[i])
                {
                    rc += "1";
                    ipi -= bitValue[i];
                }
                else
                {
                    rc += "0";
                }
            }
            return rc;
        }
        /// <summary>
        /// преобразование двоичной строки в IP адрес
        /// </summary>
        /// <param name="bstr">двоичное представление ип адреса</param>
        /// <returns></returns>
        static public string BinaryStringToIP(string bstr)
        {
            string rc = "";
            string[] sip = { "", "", "", "" };
            if (bstr.Length == ipLength)
            {
                for (int i = 0, j = 0; i < ipLength; i += 8)
                {
                    sip[j++] = Convert.ToString(BinaryStringToByte(bstr.Substring(i, 8)));
                }
            }
            rc = string.Join(".", sip);
            return rc;
        }
        /// <summary>
        /// преобразование битовой строки в байт
        /// </summary>
        /// <param name="eight"> двоичная 8-ми значная битовая строка </param>
        /// <returns></returns>
        static public byte BinaryStringToByte(string eight)
        {
            byte rc = 0;
            while (eight.Length < 8) eight.Insert(0, "0");

            for (int i = 0; i < bitValue.Length; i++)
            {
                if (eight[i] == '1')
                {
                    rc += bitValue[i];
                }
            }
            return rc;
        }

    }
}
