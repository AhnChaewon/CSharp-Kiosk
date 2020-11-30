using Burgerking_Kiosk.Network;
using Burgerking_Kiosk.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Burgerking_Kiosk.Pages
{
    /// <summary>
    /// HomePage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            this.startMedia.Play();
            this.startMedia.MediaEnded += new RoutedEventHandler(startMedia_MediaEnded);
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            //var window = Window.GetWindow(this);
            //window.KeyDown += keyDownEvent;

            if (!(Settings.Default.AutoLogin) && !(NavigationService.CanGoForward))
            {
                NavigationService.Navigate(new Uri("/Pages/LoginPage.xaml", UriKind.Relative));
            }

        }

        private void startMedia_MediaEnded(object sender, RoutedEventArgs e)
        {

            this.startMedia.Stop();

            this.startMedia.Position = TimeSpan.FromSeconds(0);

            this.startMedia.Play();

        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/OrderPage.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/StatisticsPage.xaml", UriKind.Relative));
        }

        private void keyDownEvent(object sender, KeyEventArgs e)
        {
            //if(e.Key == Key.F2)
            //{
            //    NavigationService.Navigate(new Uri("/Pages/StatisticsPage.xaml", UriKind.Relative));
            //}
        }
    }
}
