using System.Windows;
using System.Windows.Controls;

namespace CSharp_Kiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            MainControl.btnMove.Click += BtnMove_Click;
        }

        private void BtnMove_Click(object sender, RoutedEventArgs e)
        {
            MainControl.Visibility = Visibility.Collapsed;
            OrderControl.Visibility = Visibility.Visible;
        }
    }
}
