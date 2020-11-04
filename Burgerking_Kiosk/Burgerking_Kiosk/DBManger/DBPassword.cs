using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerking_Kiosk.DBManger
{
    class DBPassword
    {
        private static String serverIP = "10.80.161.167";
        private static String database = "csdb";
        private static String userId = "root";
        private static String password = "rbtjr0614!";

        public static String connStr = String.Format("Server={0};Database={1};Uid={2};Pwd={3}", serverIP,database,userId,password);

    }
}
