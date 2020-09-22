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
            MainControl.mainNextBtn.Click += MainNextBtn_Click;
            OrderControl.orderNextBtn.Click += OrderNextBtn_Click;
            ChooseDiningPlaceControl.chooseNextBtn.Click += ChooseNextBtn_Click;
            DiningPlaceControl.diningNextBtn.Click += DiningNextBtn_Click;
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
