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
using System.Xml;

namespace Burgerking_Kiosk.Pages
{
    /// <summary>
    /// FinishPaymentPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FinishPaymentPage : Page
    {
        public FinishPaymentPage()
        {
            InitializeComponent();
            //DB 연결되면 DB Member 테이블 접근해서 이름 받아와야 함
            card.Text = "인식된 카드 번호 : " +OrderData.member;

            //SELECT OrderId FROM csdb.sell ORDER BY OrderId desc LIMIT 1;
            //이 코드를 사용하면 주문번호 중 제일 큰 거 받아옴 여기에 +1해주면 됨
        }

        private void finishBtn_Click(object sender, RoutedEventArgs e)
        {
            while (NavigationService?.CanGoBack == true) { 
                NavigationService?.RemoveBackEntry(); 
            }

            

            NavigationService.Navigate(new HomePage());
        }
    }
}
