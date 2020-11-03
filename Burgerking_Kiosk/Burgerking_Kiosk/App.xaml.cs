using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Burgerking_Kiosk
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        Stopwatch sw = new Stopwatch();
        public static double time = 0;
        public static int originTime = 0;

        public App()
        {
            this.Startup += App_Startup;
            this.Exit += App_Exit;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            sw.Stop();
            time = sw.ElapsedMilliseconds / 1000;
            Console.WriteLine(time);

            string connStr = "Server=10.80.161.167;Database=csdb;Uid=root;Pwd=rbtjr0614!";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                string sql = "UPDATE csdb.time SET Time = " + (originTime + time);
                Console.WriteLine(sql);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("구동시간 : "+time);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            sw.Reset();
            sw.Start();
            string connStr = "Server=10.80.161.167;Database=csdb;Uid=root;Pwd=rbtjr0614!";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                string sql = "SELECT * FROM csdb.time";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    originTime = Convert.ToInt32(reader["Time"]);
                }
                Console.WriteLine(originTime);
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        

        
    }
}
