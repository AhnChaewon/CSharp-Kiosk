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
using System.Windows.Threading;

namespace Burgerking_Kiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        public String content = "주문하던 중이였습니다.취소하시겠습니까?";
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(0.01);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            date.Text = System.DateTime.Now.ToString("yyyy년 MM월 dd일");
            time.Text = System.DateTime.Now.ToString("HH:mm:ss");
        }

        private void homeBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(OrderData.menuList != null)
            {
                if(MessageBox.Show(content, "경고", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    OrderData.table = -1;
                    OrderData.member = null;
                    OrderData.menuList = null;
                    OrderData.member = null;
                    frame.Source = new Uri("/Pages/HomePage.xaml", UriKind.Relative);
                }
            } 


            
        }
    }
}
