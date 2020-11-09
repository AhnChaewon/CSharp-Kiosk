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

namespace Burgerking_Kiosk.Pages
{
    /// <summary>
    /// StatisticsPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StatisticsPage : Page
    {
        List<MemberData> member = new List<MemberData>();

        public StatisticsPage()
        {
            InitializeComponent();
            drivingTime();
            callMember();
        }

        private void drivingTime()
        {
            int time = 0;
            int day = 0;
            int hour = 0;
            int min = 0;
            int sec = 0;

            try
            {
                DBConnection db = new DBConnection();
                db.connectDB();
                string sql = "SELECT * FROM csdb.time";
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
            Console.WriteLine("처음 시간 : " + time);

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
                Console.WriteLine("분 계산 시간 : " + time);
            }
            sec = time;
            Console.WriteLine("초 시간 : " + sec);

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
        }

        private void saleMenuBtn_Click(object sender, RoutedEventArgs e)
        {
          
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

            clearDB();
            try
            {
                DBConnection db = new DBConnection();
                db.connectDB();
                string sql = "UPDATE csdb.menu SET Sale = CASE Name WHEN \'" + menu1.Text + "\' THEN " + sale1.Text + " ELSE Sale END," +
                    "Sale = CASE Name WHEN \'" + menu2.Text + "\' THEN " + sale2.Text + " ELSE Sale END WHERE Name IN (\'" + menu1.Text + "\',\'" + menu2.Text + "\')";
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
                string sql = "UPDATE csdb.menu SET Sale = 0";
                db.setCommand(sql);
                db.execute();
                db.closeConnection();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        } // 할인율 비우기

        private void sale_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); 
            e.Handled = regex.IsMatch(e.Text);

        } //숫자 외의 입력 방지

    }
}
