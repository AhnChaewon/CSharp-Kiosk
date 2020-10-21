using Burgerking_Kiosk.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        CategoryData categoryData = new CategoryData();
        FoodData foodData = new FoodData();
        List<Food> foods = new List<Food> {};

        private int pageCount = 1;

        public OrderPage()
        {
            InitializeComponent();
            this.Loaded += OrderPage_Loaded;
            InitData();

        }

        private void OrderPage_Loaded(object sender, RoutedEventArgs e)
        {
            lbCategory.SelectedIndex = 0;
        }

        private void InitData()
        {
            categoryData.Load();
            lbCategory.ItemsSource = categoryData.lstCategory;

            foodData.Load();
            lbMenus.ItemsSource = foodData.lstFood;

            lvSelected.ItemsSource = foods;
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/ChooseDiningPage.xaml", UriKind.Relative));
        }

        private void lbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCategory.SelectedIndex == -1) return; 
            Data.Category category = (Data.Category)lbCategory.SelectedIndex;

            lbMenus.ItemsSource = foodData.lstFood.Where(x => x.category == category).ToList();
        }

        private void menuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Food food = (Food)lbMenus.SelectedItem;
            foods.Add(food);
            lvSelected.Items.Refresh();
            RefreshFood(food);
        }


        private void previewBtn_Click(object sender, EventArgs e) // 메뉴 목록 이전 버튼
        {
 
        }

        private void nextBtn_Click(object sender, EventArgs e) // 메뉴 목록 다음 버튼
        {

        }

        private void countBtn_Click(object sender, RoutedEventArgs e) // 선택한 메뉴의 -, + 버튼 클릭 시
        {
            var name = ((Button)sender).Name;
            var food = ((ListViewItem)lvSelected.ContainerFromElement(sender as Button)).Content as Food;
            if (name == "addBtn")
            {
                food.count++;
            }
            else
            {
                if(food.count == 1)
                {
                    foodRemove(food);
                }
                else
                {
                    food.count--;
                }
            }
            RefreshFood(food);
        }

        private void foodRemove(Food food) // 선택한 메뉴 삭제
        {
            var itemSource = lvSelected.ItemsSource as List<Food>;

            itemSource.Remove(food);
            RefreshFood(food);
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e) // 선택한 메뉴 삭제 버튼 클릭 시
        {
            var food = ((ListBoxItem)lvSelected.ContainerFromElement(sender as Button)).Content as Food;
            foodRemove(food);
            RefreshFood(food);
        }

        private void RefreshFood(Food food) // 계산한 총 금액 출력
        {
            totalPrice.Text = setTotalPrice() + "원";
            lvSelected.Items.Refresh();
        }

        private int setTotalPrice() // 총 금액 계산
        {
            int total = 0;

            foreach (Food food in foodData.lstFood)
            {
                total += (food.price * food.count);
            }

            return total;
        }


    }
}
