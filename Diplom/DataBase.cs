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

        public DataBase()
        {
            ConnectDB();
        }

        public void ConnectDB()
        {
            object obj;
            cnn = null;
            cnn = new Connection();
            var path_db = "db.accdb";
            try
            {
                cnn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path_db + ";Persist Security info=false";
                cnn.Open();
            }
            catch 
            { Console.WriteLine("ПОпытка соединения провалилась");  }
            if (cnn.State == 1)

                Console.WriteLine("Установлено");
            else
                Console.WriteLine("Не установлено");

            string sql = "Delete  * from TServices;";
            cnn.Execute(sql, out obj, 0);
        }

        public void AddRecord(string ipAddress, string port, string service, string connection)
        {
            string sql = string.Format("insert into TServices values ('{0}', '{1}', '{2}', '{3}')", 
                                        ipAddress, port, service, connection);
            try
            {
                object obj;
                cnn.Execute(sql, out obj, 0);
                Console.WriteLine("Запись добавлена в БД");
            }
            catch(Exception e)
            {
                Console.WriteLine("Запись не добавлена в БД. Причина:" + e.ToString());
            }
        }
    }
}
