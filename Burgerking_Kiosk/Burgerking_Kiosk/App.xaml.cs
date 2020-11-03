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

        public App()
        {
            this.Startup += App_Startup;
            this.Exit += App_Exit;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            sw.Stop();
            time = sw.ElapsedMilliseconds / 1000;
            Console.WriteLine("구동시간 : "+time);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            sw.Reset();
            sw.Start();
        }

        

        
    }
}
