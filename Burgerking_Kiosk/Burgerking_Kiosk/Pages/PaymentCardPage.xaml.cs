using Burgerking_Kiosk.Data;
using KQRCode;
using MySql.Data.MySqlClient;
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
    /// PaymentCardPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PaymentCardPage : Page
    {
        public PaymentCardPage()
        {
            InitializeComponent();
            webcam.CameraIndex = 0;

            OrderData.payment = "card";
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void webcam_QrDecoded(object sender, string e)
        {
            OrderData.member = e;
            NavigationService.Navigate(new Uri("/Pages/FinishPaymentPage.xaml", UriKind.Relative));
        }

        
    }
}
