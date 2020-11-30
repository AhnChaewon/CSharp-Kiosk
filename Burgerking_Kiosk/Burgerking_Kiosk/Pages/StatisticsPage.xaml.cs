using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using LiveCharts;
using LiveCharts.Wpf;
using Burgerking_Kiosk.DBManger;
using System.Collections;
using Burgerking_Kiosk.Data;
using System.Data.OleDb;

namespace Burgerking_Kiosk.Pages
{
    /// <summary>
    /// StatisticsPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StatisticsPage : Page
    {
        List<MemberData> member = new List<MemberData>();
        String computer = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[0];
        public StatisticsPage()
        {
            InitializeComponent();
            drivingTime();
            callMember();
            callSales();
        }

        private void drivingTime()
        {
            Console.WriteLine(computer);
            int time = 0;
            int day = 0;
            int hour = 0;
            int min = 0;
            try
            {
                DBConnection db = new DBConnection();
                db.connectDB();
                string sql = "SELECT * FROM csdb.time WHERE Computer = \""+ computer+"\"";
                db.setCommand(sql);

                MySqlDataReader reader = db.executeReadQuery();

                while (reader.Read())
                {
                    time = Convert.ToInt32(reader["Time"]);
                }
                Console.WriteLine(time);
                db.closeConnection();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


            if(time >= 86400)
            {
                day = time / 86400;
                time = time % 86400;
            }
            if(time >= 3600)
            {
                hour = time / 3600;
                time = time % 3600;
            }
            if(time >= 60)
            {
                min = time / 60;
                time = time % 60;
              
            }
            int sec = time;
            dirvingTimeText.Text = String.Format("구동 시간 : {0}일 {1}시간 {2}분 {3}초", day, hour, min, sec);
        } //시간 불러오기

        private void callMember()
        {
            try
            {
                DBConnection db = new DBConnection();
                db.connectDB();
                string sql = "SELECT * FROM csdb.user";
                db.setCommand(sql);

                MySqlDataReader reader = db.executeReadQuery();

                while (reader.Read())
                {
                    MemberData m = new MemberData { name = reader["Name"].ToString(), barcode = reader["BarCode"].ToString(), card = reader["QRCode"].ToString() };
                    member.Add(m);
                 
                }
                db.closeConnection();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            memberList.ItemsSource = member;
        }//회원 불러오기

        private void callSales()
        {
            int sum = 0;
            int netSum = 0;
            int saleSum = 0;
            int cardSum = 0;
            int moneySum = 0;
            try
            {
                DBConnection db = new DBConnection();
                db.connectDB();
                string sql = "SELECT * FROM csdb.sell";
                db.setCommand(sql);

                MySqlDataReader reader = db.executeReadQuery();

                while (reader.Read())
                {
                    int price = Convert.ToInt32(reader["Price"]);
                    int sale = Convert.ToInt32(reader["Sale"]);
                    String payment = Convert.ToString(reader["Payment"]);

                    netSum += price;
                    if(sale == price)
                    {
                        sum += price;
                    }
                    else
                    {
                        saleSum += price - sale;
                    }

                    if (payment.Equals("money"))
                    {
                        moneySum += price;
                    }
                    else
                    {
                        cardSum += price;
                    }

                }
                db.closeConnection();

                salesText.Text = String.Format("총 매출액 : {0} \n순수 매출액 : {1} \n할인 금액 : {2} \n\n\n\n카드 매출액 : {3} \n현금 매출액 : {4}",sum, netSum, saleSum, cardSum, moneySum);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        } //할인율 불러오기

        private void saleMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            int menuPrice1;
            int menuPrice2;
          
            if (menu1.Text == menu2.Text)
            {
                MessageBox.Show("서로 다른 메뉴를 선택하십시오!", "경고", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else if(menu1.Text == "" || menu2.Text == "")
            {
                MessageBox.Show("메뉴를 선택하십시오!", "경고", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else if(sale1.Text == "" || sale2.Text == "")
            {
                MessageBox.Show("할인율을 적어주십시오!", "경고", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else if((Convert.ToInt32(sale1.Text) <= 0 || Convert.ToInt32(sale1.Text) >= 100) || (Convert.ToInt32(sale2.Text) <= 0 || Convert.ToInt32(sale2.Text) >= 100))
            {
                MessageBox.Show("할인율은 1~99 사이의 숫자를 적어주십시오!", "경고", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            menuPrice1 = originalPrice(menu1.Text);
            menuPrice2 = originalPrice(menu2.Text);

            double salePrice1 = menuPrice1 - ((double)menuPrice1 * (Convert.ToDouble(sale1.Text) / 100));
            double salePrice2 = menuPrice2 - ((double)menuPrice2 * (Convert.ToDouble(sale2.Text) / 100));

            Console.WriteLine(salePrice1 + " " + salePrice2);


            clearDB();
            try
            {
                DBConnection db = new DBConnection();
                db.connectDB();
                string sql = "UPDATE csdb.menu SET Sale = CASE Name WHEN \'" + menu1.Text + "\' THEN " + salePrice1 + " ELSE Sale END," +
                    "Sale = CASE Name WHEN \'" + menu2.Text + "\' THEN " + salePrice2 + " ELSE Sale END WHERE Name IN (\'" + menu1.Text + "\',\'" + menu2.Text + "\')";
                db.setCommand(sql);
                db.execute();
                db.closeConnection();

                MessageBox.Show("적용되었습니다!", "띵동", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }//할인율 설정

        private void clearDB()
        {
            try
            {
                DBConnection db = new DBConnection();
                db.connectDB();
                string sql = "UPDATE csdb.menu SET Sale = Price";
                db.setCommand(sql);
                db.execute();
                db.closeConnection();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        } // 할인율 초기화

        private int originalPrice(String menu)
        {
            int price = 0;
            try
            {
                DBConnection db = new DBConnection();
                db.connectDB();
                String sql = "SELECT Price FROM csdb.menu WHERE Name = \"" + menu + "\"";
                db.setCommand(sql);
                MySqlDataReader reader = db.executeReadQuery();

                while (reader.Read())
                {
                    price = Convert.ToInt32(reader["Price"]);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return price;
        } // 원래 가격

        private void sale_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); 
            e.Handled = regex.IsMatch(e.Text);

        } //숫자 외의 입력 방지

    }
}
