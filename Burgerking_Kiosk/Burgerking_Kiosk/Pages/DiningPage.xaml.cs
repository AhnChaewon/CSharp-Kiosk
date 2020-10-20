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
using System.Timers;
using System.IO;
using System.Net;
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

            // 1. DB에서 테이블 주문일 데이터 불러옴

            // 2. 버튼에 테이블 주문일 넣기

        }

        private void PaymentBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/PaymentPage.xaml", UriKind.Relative));
        }

        private void BackChooseDiningBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            
        }

        void PrintText(object sender, SelectionChangedEventArgs args)
        {
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            tb.Text = "   You selected " + lbi.Content.ToString() + ".";
        }


    }
}
