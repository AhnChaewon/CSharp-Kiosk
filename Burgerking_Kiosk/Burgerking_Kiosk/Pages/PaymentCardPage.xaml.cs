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

            //connect();
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
            OrderData.member = new MemberData() { };
            NavigationService.Navigate(new Uri("/Pages/FinishPaymentPage.xaml", UriKind.Relative));
        }

        private void connect()
        {
            string connStr = "Server=127.0.0.1;Database=csdb;Uid=root;Pwd=rbtjr0614!";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "Insert into csdb.sell (MenuId, Price, Day, Seat, Sale, Member) Values (2,100,\"20201010\",9, 10, \"ㅁㅁ\")";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
