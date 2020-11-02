﻿using Burgerking_Kiosk.Data;
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

namespace Burgerking_Kiosk.Pages
{
    /// <summary>
    /// PaymentMoneyPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PaymentMoneyPage : Page
    {
        public PaymentMoneyPage()
        {
            InitializeComponent();
            OrderData.payment = "money";
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void barcodeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            OrderData.member = barcodeText.Text;
            NavigationService.Navigate(new Uri("/Pages/FinishPaymentPage.xaml", UriKind.Relative));
        }
    }
}
