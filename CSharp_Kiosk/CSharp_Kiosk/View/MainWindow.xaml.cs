using System.Windows;
using System.Windows.Controls;

namespace CSharp_Kiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        Visibility collpased = Visibility.Collapsed;
        Visibility visible = Visibility.Visible;

        public MainWindow()
        {
            InitializeComponent();
            //임시로 만들어놓은 다음 버튼
            MainControl.mainNextBtn.Click += MainNextBtn_Click;
            OrderControl.orderNextBtn.Click += OrderNextBtn_Click;
            ChooseDiningPlaceControl.chooseNextBtn.Click += ChooseNextBtn_Click;
            DiningPlaceControl.diningNextBtn.Click += DiningNextBtn_Click;

            //홈버튼. 너무 비효율적이긴 한데 일단 이렇게 쓰자
            ChooseDiningPlaceControl.chooseDiningHome.MouseDown += ChooseDiningHome_MouseDown;
            DiningPlaceControl.diningHome.MouseDown += DiningHome_MouseDown;
            PaymentControl.paymentHome.MouseDown += PaymentHome_MouseDown;

            PaymentControl.cardBtn.MouseDown += CardBtn_MouseDown;
            PaymentControl.moneyBtn.MouseDown += MoneyBtn_MouseDown;
        }

        private void MoneyBtn_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PaymentControl.Visibility = collpased;
            PaymentMoneyControl.Visibility = visible;
        }

        private void CardBtn_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PaymentControl.Visibility = collpased;
            PaymentCardControl.Visibility = visible;
        }

        private void DiningHome_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DiningPlaceControl.Visibility = collpased;
            MainControl.Visibility = visible;
        }

        private void ChooseDiningHome_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChooseDiningPlaceControl.Visibility = collpased;
            MainControl.Visibility = visible;
        }

        private void OrderHome_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OrderControl.Visibility = collpased;
            MainControl.Visibility = visible;
        }

        private void PaymentHome_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PaymentControl.Visibility = collpased;
            MainControl.Visibility = visible;
        }

        private void DiningNextBtn_Click(object sender, RoutedEventArgs e)
        {
            DiningPlaceControl.Visibility = collpased;
            PaymentControl.Visibility = visible;
        }

        private void ChooseNextBtn_Click(object sender, RoutedEventArgs e)
        {
            ChooseDiningPlaceControl.Visibility = collpased;
            DiningPlaceControl.Visibility = visible;
        }

        private void MainNextBtn_Click(object sender, RoutedEventArgs e)
        {
            MainControl.Visibility = collpased;
            OrderControl.Visibility = visible;
        }

        private void OrderNextBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderControl.Visibility = collpased;
            ChooseDiningPlaceControl.Visibility = visible;
        }
    }
}
