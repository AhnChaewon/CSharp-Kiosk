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
    /// DiningPlace.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DiningPlace : Page
    {
        public DiningPlace()
        {
            InitializeComponent();
        }

        private void PaymentBtn_Click(object sender, RoutedEventArgs e)
        {
            Payment payment = new Payment();
            NavigationService.Navigate(payment);
        }
    }
}
