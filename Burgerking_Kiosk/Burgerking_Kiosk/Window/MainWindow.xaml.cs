using Burgerking_Kiosk.Data;
using Burgerking_Kiosk.DBManger;
using Burgerking_Kiosk.Network;
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
using System.Windows.Threading;

namespace Burgerking_Kiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        public String content = "주문하던 중이였습니다. 취소하시겠습니까?";
        public MainWindow()
        {
            InitializeComponent();

            DBMenu db = new DBMenu();
            db.GetMenu();

            this.Loaded += MainWindow_Loaded;
            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(0.01);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            String day = System.DateTime.Now.ToString("yyyy년 MM월 dd일 (ddd)");
            String t = System.DateTime.Now.ToString("tt h:mm:ss");
            
            date.Text = day;
            time.Text = t;
        }


        private void homeBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(OrderData.menuList.Count != 0)
            {
                if(MessageBox.Show(content, "경고", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    OrderData.clearData();
                }
                else
                {
                    return;
                }
                
            }
            frame.Source = new Uri("/Pages/HomePage.xaml", UriKind.Relative);
        }

        private void msgSendBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ClientManager.client.Connected)
            {
                if (!msgEdit.Text.Equals(""))
                {
                    ClientManager.sendMessage(msgEdit.Text, 1, "", "", false, null);
                    msgEdit.Text = "";
                }
                else
                {
                    MessageBox.Show("메세지를 적어주세요!");
                }
            }
            else
            {
                MessageBox.Show("서버가 연결되어 있지 않습니다.");
            }
            

        }
    }
}
