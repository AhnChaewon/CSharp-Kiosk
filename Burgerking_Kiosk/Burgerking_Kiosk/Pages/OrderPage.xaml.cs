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
        List<Food> foods = new List<Food>() 
        { 
            // 담기는 리스트
        };

        List<Food> showFood = new List<Food>()
        {
            // 보이는 리스트
        };

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
            int flag = 0;

            if(lbMenus.SelectedIndex == -1)
            {
                return;
            }

            Food food = (Food)lbMenus.SelectedItem;

            for(int i = 0; i < foods.Count; i++)
            {
                if (food.name == foods[i].name)
                {
                    foods[i].count++;
                    flag = 1;
                    break;
                }
            }

            if(flag == 0)
            {
                food.count++;
                foods.Add(food);
            }

            totalPrice.Text = setTotalPrice() + "원";
            lvSelected.Items.Refresh();
            btnIsEnbled();

            lbMenus.SelectedIndex = -1;
        }


        private void previewBtn_Click(object sender, RoutedEventArgs e) // 메뉴 목록 이전 버튼
        {

        }

        private void nextBtn_Click(object sender, RoutedEventArgs e) // 메뉴 목록 다음 버튼
        {
            previewBtn.IsEnabled = true;
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
                    food.count = 0;
                    
                    foodRemove(food);
                    }
                else
                {
                    food.count--;
                }
                btnIsEnbled();
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
            var food = ((ListViewItem)lvSelected.ContainerFromElement(sender as Button)).Content as Food;
            food.count = 0;
            foodRemove(food);
            btnIsEnbled();
        }

        private void btnIsEnbled() // 버튼 활성화
        {
            if(foods.Count != 0) {
                orderBtn.IsEnabled = true;
                deleteAllBtn.IsEnabled = true;
            }
            else
            {
                orderBtn.IsEnabled = false;
                deleteAllBtn.IsEnabled = false;
            }
        }


        private void deleteAllBtn_Click(object sender, RoutedEventArgs e) // 메뉴 전체 삭제
        {

            if (MessageBox.Show("선택한 메뉴를 모두 삭제하시겠습니까?", "안내", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foodClear();
                btnIsEnbled();
                MessageBox.Show("삭제되었습니다.", "안내");
            }
            else
            {
                MessageBox.Show("취소되었습니다", "안내");
            }
        }


        private void prePageBtn_Click(object sender, RoutedEventArgs e) // 주문 취소
        {
            if(foods.Count != 0)
            {
                if (MessageBox.Show("주문을 취소하시겠습니까?", "안내", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    foodClear();
                    NavigationService.Navigate(new Uri("/Pages/HomePage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("취소되었습니다", "안내");
                }
            }
            else
            {
                NavigationService.Navigate(new Uri("/Pages/HomePage.xaml", UriKind.Relative));
            }
            
            
        }

        private void foodClear() // 주문한 메뉴 전체 삭제
        {
            for(int i=0; i < foods.Count; i++)
            {
                foods[i].count = 0;
            }
            foods.Clear();
            lvSelected.Items.Refresh(); 
            totalPrice.Text = "0";
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
                total += food.price * food.count;
            }

            return total;
        }
    }
}
