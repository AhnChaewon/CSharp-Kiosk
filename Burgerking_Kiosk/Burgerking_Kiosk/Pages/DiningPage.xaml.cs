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
        }


        private void BackChooseDiningBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("테이블 선택을 취소 하시겠습니까?", "안내", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
            else
            {
                return;
            }
            

        }


        private String content = "";
        int timeCounter = 19;

       
        //private void ChangeContent()
        //{
        //    content = lbBtn.Content.ToString();
        //}

        DispatcherTimer dt = new DispatcherTimer();
        private void lbBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("클릭클릭");
            Button a = (Button)sender;
            OrderData.table = Convert.ToInt32(a.Content);
            Console.WriteLine(Convert.ToInt32(a.Content));
            if (MessageBox.Show("테이블을 선택하시겠습니까?", "안내", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new Uri("/Pages/PaymentPage.xaml", UriKind.Relative));
            }
            else
            {
                return;
            }
            

            //dt.Interval = TimeSpan.FromSeconds(1);
            //dt.Tick += new EventHandler(timer_Tick);

            //dt.Start();
            //ChangeContent();


        }


        private void timer_Tick(object sender, EventArgs e)
        {

            if (timeCounter > 0)
            {
                timeCounter--;
                lbBtn.Content = timeCounter.ToString("0:00");
                lbBtn.Background = Brushes.Red;
            }

            else
            {
                dt.Stop();
                dt.IsEnabled = false;
                lbBtn.Content = content;
                lbBtn.Background = Brushes.Green;
            }

        }


    }
}