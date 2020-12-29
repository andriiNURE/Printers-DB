using System;
using MySql.Data.MySqlClient;

namespace Printers
{
    class Connection : IDisposable
    {
        static Connection _instance = null;
        private MySqlConnection connect = null;

        private Connection()
        {
            connect =
                new MySqlConnection("server=localhost;port=3306;userid=root;password=Novuyparol_2019;database=mydb");
        }

        public static Connection GetInstance()
        {
            if(_instance == null)
            {
                _instance = new Connection();
            }

            return _instance;
        }
        
        public void OpenConnection()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
                connect.Open();
        }

        public void CloseConnection()
        {
            if (connect.State == System.Data.ConnectionState.Open)
                connect.Close();
        }

        public MySqlConnection GetConnection()
        {
            return connect;
        }

        public void Dispose()
        {
            connect.Close();
        }

    }
}
