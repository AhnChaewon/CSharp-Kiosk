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
    /// OrderPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            ChooseDiningPlace chooseDP = new ChooseDiningPlace();
            NavigationService.Navigate(chooseDP);
        }
    }
}