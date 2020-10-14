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
    /// FinishPaymentPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FinishPaymentPage : Page
    {
        public FinishPaymentPage()
        {
            InitializeComponent();
        }

        private void finishBtn_Click(object sender, RoutedEventArgs e)
        {
            while (NavigationService?.CanGoBack == true) { 
                NavigationService?.RemoveBackEntry(); 
            }

            /*OrderData.table = -1;
            OrderData.member = null;
            OrderData.menuList.RemoveRange(0, OrderData.menuList.Count);
            OrderData.member = null;*/

            NavigationService.Navigate(new HomePage());
        }
    }
}
