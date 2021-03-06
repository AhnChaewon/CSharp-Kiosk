﻿using System;
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
    /// PaymentCard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PaymentCard : Page
    {
        public PaymentCard()
        {
            InitializeComponent();
        }
        private void FinishPaymentBtn_Click(object sender, RoutedEventArgs e)
        {
            FinishPayment finishPayment = new FinishPayment();
            NavigationService.Navigate(finishPayment);
        }
    }
}
