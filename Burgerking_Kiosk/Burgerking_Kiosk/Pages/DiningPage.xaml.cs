using Burgerking_Kiosk.Data;
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
    /// DiningPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DiningPage : Page
    {
        public DiningPage()
        {
            InitializeComponent();
        }

        private void PaymentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (table.Text == null || table.Text.Equals(""))
            {
                MessageBox.Show("선택해주시기 바랍니다!");
            }
            else
            {
                OrderData.table = Convert.ToInt32(table.Text);
                NavigationService.Navigate(new Uri("/Pages/PaymentPage.xaml", UriKind.Relative));
            }
            
        }

        private void BackChooseDiningBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            
        }


    }
}
