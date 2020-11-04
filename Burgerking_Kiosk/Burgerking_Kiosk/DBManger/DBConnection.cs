using MySql.Data.MySqlClient;
using Renci.SshNet.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerking_Kiosk.DBManger
{
    class DBConnection
    {

        private MySqlConnection connection = null;
        private MySqlCommand cmd = null;

        public void setCommand(string sql)
        {
            cmd = new MySqlCommand(sql, connection);
        }

        public void connectDB()
        {
            String DBConnectionPW = DBPassword.connStr;
            connection = new MySqlConnection(DBConnectionPW);
            connection.Open();
        }

        public void closeConnection()
        {
            connection.Close();
        }

        public int execute()
        {
            return cmd.ExecuteNonQuery();
        }

        public MySqlDataReader executeReadQuery()
        {
            return cmd.ExecuteReader();
        }

    }
}
