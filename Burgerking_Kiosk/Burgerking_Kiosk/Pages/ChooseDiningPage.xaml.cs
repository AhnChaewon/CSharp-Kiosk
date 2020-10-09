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
    /// ChooseDiningPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChooseDiningPage : Page
    {
        public ChooseDiningPage()
        {
            InitializeComponent();
        }

        private void DiningPlaceBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/DiningPage.xaml", UriKind.Relative));
        }

        private void PaymentBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/PaymentPage.xaml", UriKind.Relative));
        }

        private void BackMainBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            
        }
    }
}
