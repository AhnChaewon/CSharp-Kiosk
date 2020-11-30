using Burgerking_Kiosk.Data;
using Burgerking_Kiosk.DBManger;
using Burgerking_Kiosk.Network;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
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
        double time = 0;
        String computer = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[0];
        Boolean check = false;

        public static List<Menu> burgerList = new List<Menu>();
        public static List<Menu> sideList = new List<Menu>();
        public static List<Menu> drinkList = new List<Menu>();

        public App()
        {
            this.Startup += App_Startup;
            this.Exit += App_Exit;
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
                    if (reader["Computer"].Equals(computer))
                    {
                        check = true;
                    }
                }
                Console.WriteLine(time);
                db.closeConnection();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            ClientManager.start();
            ClientManager.sendMessage("", 0);

            
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            ClientManager.close();
            sw.Stop();
            time = sw.ElapsedMilliseconds / 1000;

            string sql;
            if (check)
            {
                sql = "UPDATE csdb.time SET Time = Time + " + time + " WHERE Computer = \"" + computer+"\"";
            }
            else
            {
                sql = "INSERT INTO csdb.time (Time, Computer) VALUE("+time+",\""+computer+"\")";
            }

            try
            {
                DBConnection db = new DBConnection();
                db.connectDB();
                
                db.setCommand(sql);
                db.execute();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        

        

        
    }
}
