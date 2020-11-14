using Burgerking_Kiosk.Properties;
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
    /// LoginPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginPage : Page
    {
        String id = "1234";
        String pw = "1234";

        public LoginPage()
        {
            InitializeComponent();


            if (Settings.Default.AutoLogin)
            {
                NavigationService.Navigate(new Uri("/Pages/HomePage.xaml", UriKind.Relative));
            }
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if(idBox.Text.Equals(id) && pwBox.Text.Equals(pw))
            {
                if ((bool)autoLoginCheck.IsChecked)
                {
                    Settings.Default.AutoLogin = true;
                    
                }
                NavigationService.Navigate(new Uri("/Pages/HomePage.xaml", UriKind.Relative));
            }
        }
    }
}
