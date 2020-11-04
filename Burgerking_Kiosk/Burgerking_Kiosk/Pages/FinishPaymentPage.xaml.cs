using Burgerking_Kiosk.Data;
using Burgerking_Kiosk.DBManger;
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
using System.Xml;

namespace Burgerking_Kiosk.Pages
{
    /// <summary>
    /// FinishPaymentPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FinishPaymentPage : Page
    {

        int orderNum = 0;
        public FinishPaymentPage()
        {
            InitializeComponent();
            
        }

        private void finishBtn_Click(object sender, RoutedEventArgs e)
        {
            while (NavigationService?.CanGoBack == true)
            {
                NavigationService?.RemoveBackEntry();
            }

            NavigationService.Navigate(new Uri("/Pages/HomePage.xaml", UriKind.Relative));
        }

        private void takeOrderNum()
        {
            try
            {
                DBConnection db = new DBConnection();
                db.connectDB();
                string sql = "SELECT OrderId FROM csdb.sell ORDER BY OrderId desc LIMIT 1";
                db.setCommand(sql);
                MySqlDataReader reader = db.executeReadQuery();

                while (reader.Read())
                {
                    orderNum = Convert.ToInt32(reader["OrderId"]) + 1;
                    order.Text = "주문번호 : " + orderNum.ToString();
                }

                db.closeConnection();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void takeUser(MySqlConnection conn)
        {
            if (OrderData.payment.Equals("card"))
            {
                card.Text = "인식된 카드 번호 : " + OrderData.member;
                name.Text = "회원명 : " + OrderData.member;
            }
            else
            {
                try
                {
                    DBConnection db = new DBConnection();
                    db.connectDB();
                    string sql = "SELECT Name FROM csdb.user WHERE Barcode = \"" + OrderData.member +"\"";
                    db.setCommand(sql);
                    MySqlDataReader reader = db.executeReadQuery();

                    while (reader.Read())
                    {
                        OrderData.member = reader["Name"].ToString();
                        name.Text = "회원명 : " + OrderData.member;
                    }
                    db.closeConnection();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            
        }
    }
}
