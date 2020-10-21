﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// StatisticsPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StatisticsPage : Page
    {
        public StatisticsPage()
        {
            InitializeComponent();
        }

        private void sale_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); 
            e.Handled = regex.IsMatch(e.Text);

        }

        private void saleMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            string connStr = "Server=10.80.161.167;Database=csdb;Uid=root;Pwd=rbtjr0614!";
            clearDB(connStr);
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "UPDATE csdb.menu SET Sale = CASE Name WHEN \'" + menu1.Text + "\' THEN "+ sale1.Text +" ELSE Sale END," +
                    "Sale = CASE Name WHEN \'"+ menu2.Text +"\' THEN "+ sale2.Text +" ELSE Sale END WHERE Name IN (\'"+menu1.Text+"\',\'"+menu2.Text+"\')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void clearDB(string db)
        {
            MySqlConnection conn = new MySqlConnection(db);
            try
            {
                conn.Open();
                string sql = "UPDATE csdb.menu SET Sale = 0";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
