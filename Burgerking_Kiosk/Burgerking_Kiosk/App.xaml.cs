using Burgerking_Kiosk.DBManger;
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

            try
            {
                DBConnection db = new DBConnection();
                db.connectDB();
                string sql = "UPDATE csdb.time SET Time = " + (originTime + time);
                db.setCommand(sql);
                db.execute();
                db.closeConnection();
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

            try
            {
                DBConnection db = new DBConnection();
                db.connectDB();
                string sql = "SELECT * FROM csdb.time";
                db.setCommand(sql);

                MySqlDataReader reader = db.executeReadQuery();

                while (reader.Read())
                {
                    originTime = Convert.ToInt32(reader["Time"]);
                }
                Console.WriteLine(originTime);
                db.closeConnection();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        

        
    }
}
