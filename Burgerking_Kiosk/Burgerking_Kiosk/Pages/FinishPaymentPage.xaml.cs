using Burgerking_Kiosk.Data;
using Burgerking_Kiosk.DBManger;
using Burgerking_Kiosk.Network;
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
            this.Loaded += FinishPaymentPage_Loaded;
            InitializeComponent();
            
        }

        private void FinishPaymentPage_Loaded(object sender, RoutedEventArgs e)
        {
            money.Text = "총 금액 : " + OrderData.sumMoney;
            takeOrderNum();
            takeUser();
            sellDB();
            sellInfoSendServer();
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

        private void takeUser()
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
                    string sql = "SELECT Name, BarCode FROM csdb.user WHERE Barcode = \"" + OrderData.member +"\"";
                    db.setCommand(sql);
                    MySqlDataReader reader = db.executeReadQuery();

                    while (reader.Read())
                    {
                        OrderData.member = reader["Name"].ToString();
                        name.Text = "회원명 : " + OrderData.member;
                        card.Text = "인식된 카드 번호 : " + reader["BarCode"].ToString();
                    }
                    db.closeConnection();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            
        }

        private void sellDB()
        {
            String time = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
            Console.WriteLine(OrderData.menuList.Count);
            foreach(Data.Menu m in OrderData.menuList)
            {
                try
                {
                    DBConnection db = new DBConnection();
                    db.connectDB();
                    string sql = String.Format("INSERT INTO csdb.sell(OrderId, Price, Seat, Sale, Member, BuyTime, Menu, Payment, Num) VALUE({0}, {1}, {2}, {3}, \"{4}\", \"{5}\", \"{6}\", \"{7}\", {8})",
                        orderNum, m.price, OrderData.table, m.sale, OrderData.member, time, m.name, OrderData.payment, m.count);
                    db.setCommand(sql);
                    db.execute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
           
        }

        private void sellInfoSendServer()
        {
            String o = "";
            int tempOrder = orderNum % 100;

            if (orderNum < 10)
            {
                o = "00" + tempOrder.ToString();
            }
            else
            {
                o = "0" + tempOrder.ToString();
            }

            List<ServerMenu> sMenu = new List<ServerMenu>();
            foreach(Data.Menu m in OrderData.menuList)
            {
                ServerMenu s = new ServerMenu();
                s.Name = m.name;
                s.Count = m.count;
                s.Price = m.price;
                sMenu.Add(s);
            }


            ClientManager.sendMessage("", 2, "버거왕", o, true, sMenu);
        }

        
    }
}
