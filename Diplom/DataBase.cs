using ADODB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/*
    по службам узнавать устройство, если устройство не опознано, возможность самому задать тип устройства.
    Для отчета: постановка задачи, иследование, теоритическая база, реализация.
*/

namespace Diplom
{
    class DataBase
    {
        public Connection cnn;
        public Recordset rst, rst_order;
        public OleDbDataAdapter odb;

        public DataBase(bool flag)
        {
            
            ConnectDB(flag);
        }

        public void ConnectDB(bool flag)
        {
            object obj;
            cnn = new Connection();
            
            var path_db = "db.accdb";
            try
            {
                cnn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path_db + ";Persist Security info=false";
                cnn.Open();
            }
            catch 
            { Console.WriteLine("Попытка соединения провалилась");  }
            if (cnn.State == 1)

                Console.WriteLine("Установлено");
            else
                Console.WriteLine("Не установлено");

            if (flag)
            {
                string sql = "Delete  * from TServices;";
                cnn.Execute(sql, out obj, 0);
                sql = "Delete  * from TDevice;";
                cnn.Execute(sql, out obj, 0);
            }

        }

        public void AddRecordServices(string ipAddress, string port, string service, string connection)
        {
            string sql = string.Format("insert into TServices values ('{0}', '{1}', '{2}', '{3}')", 
                                        ipAddress, port, service, connection);
            try
            {
                object obj;
                cnn.Execute(sql, out obj, 0);
                //Console.WriteLine("Запись добавлена в БД");
            }
            catch(Exception e)
            {
                Console.WriteLine("Запись не добавлена в БД. Причина:" + e.ToString());
            }
        }

        public void SelectData(string ip, List<string> list)
        {
            rst = new Recordset();
            var sql = "Select service from TServices where [ip address] = '" + ip + "'";
            rst.Open(sql, cnn, CursorTypeEnum.adOpenStatic, LockTypeEnum.adLockOptimistic);
            
            rst.MoveFirst();

            for (int i = 0; !rst.EOF; i++)
            {
                list.Add(Convert.ToString(rst.Fields[0].Value));
                Console.WriteLine("DB   " + Convert.ToString(rst.Fields[0].Value));
                rst.MoveNext();
            }
        }

        public void AddRecordTypeDevice(string ipAddress, string type)
        {
            string sql = string.Format("insert into TDevice values ('{0}', '{1}');",
                                        ipAddress, type);
            try
            {
                object obj;
                cnn.Execute(sql, out obj, 0);
                Console.WriteLine("Запись добавлена в БД");
            }
            catch (Exception e)
            {
                Console.WriteLine("Запись не добавлена в БД. Причина:" + e.ToString());
            }
        }
        public void Close()
        {
            Console.WriteLine("БД закрыта");
           cnn.Close();
        }
    }
}
