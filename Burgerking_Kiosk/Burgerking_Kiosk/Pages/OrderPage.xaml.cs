using Burgerking_Kiosk.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Burgerking_Kiosk.DBManger;

namespace Burgerking_Kiosk.Pages
{
    /// <summary>
    /// OrderPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderPage : Page
    {
        CategoryData categoryData = new CategoryData();
        List<Data.Menu> foodData = new List<Data.Menu>(); // 메뉴 목록 보여줄 리스트

        int pageCount = 0;

        public OrderPage()
        {
            InitializeComponent();
            this.Loaded += OrderPage_Loaded;
            InitData();
        }

        private void OrderPage_Loaded(object sender, RoutedEventArgs e)
        {
            lbCategory.SelectedIndex = 0;
            lvSelected.ItemsSource = OrderData.menuList;
            totalPrice.Text = OrderData.sumMoney + "원";

            orderBtn_IsEnbled();
        }

        private void InitData()
        {
            categoryData.Load();
            lbCategory.ItemsSource = categoryData.lstCategory;

            lbMenus.ItemsSource = App.burgerList;
        }

        private void previewBtn_Click(object sender, RoutedEventArgs e) // 메뉴 목록 이전 버튼
        {
            if (lbCategory.SelectedIndex == 0 && pageCount > 0)
            {
                foodData.Clear();
                pageCount -= 1;

                for (int i = pageCount * 6; i <= pageCount * 6 + 5; i++)
                {
                    if (i >= App.burgerList.Count) break;
                    foodData.Add(App.burgerList[i]);
                }

                menuBtn_IsEnabled(1);
            }

            else if (lbCategory.SelectedIndex == 1 && pageCount > 0)
            {
                foodData.Clear();
                pageCount -= 1;

                for (int i = pageCount * 6; i <= pageCount * 6 + 5; i++)
                {
                    if (i >= App.drinkList.Count) break;
                    foodData.Add(App.drinkList[i]);
                }

                menuBtn_IsEnabled(2);
            }

            else if (lbCategory.SelectedIndex == 2 && pageCount > 0)
            {
                foodData.Clear();
                pageCount -= 1;

                for (int i = pageCount * 6; i <= pageCount * 6 + 5; i++)
                {
                    if (i >= App.sideList.Count) break;
                    foodData.Add(App.sideList[i]);
                }

                menuBtn_IsEnabled(1);
            }
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e) // 메뉴 목록 다음 버튼
        {
            if (lbCategory.SelectedIndex == 0 && (pageCount + 1) * 6 <= App.burgerList.Count) //
            {
                foodData.Clear();
                pageCount += 1;

                for (int i = pageCount * 6; i <= pageCount * 6 + 5; i++)
                {
                    if (i >= App.burgerList.Count) break;
                    foodData.Add(App.burgerList[i]);
                }

                menuBtn_IsEnabled(1);
            }

            else if (lbCategory.SelectedIndex == 1 && (pageCount + 1) * 6 <= App.drinkList.Count)
            {
                foodData.Clear();
                pageCount += 1;

                for (int i = pageCount * 6; i <= pageCount * 6 + 5; i++)
                {
                    if (i >= App.drinkList.Count) break;
                    foodData.Add(App.drinkList[i]);
                }

                menuBtn_IsEnabled(2);
            }

            else if (lbCategory.SelectedIndex == 2 && (pageCount + 1) * 6 <= App.sideList.Count)
            {
                foodData.Clear();
                pageCount += 1;

                for (int i = pageCount * 6; i <= pageCount * 6 + 5; i++)
                {
                    if (i >= App.sideList.Count) break;
                    foodData.Add(App.sideList[i]);
                }

                menuBtn_IsEnabled(1);
            }
        }

        private void menuBtn_IsEnabled(int pageNum) // 메뉴 목록 버튼 비활성화/활성화
        {
            if (pageCount != 0)
            {
                previewBtn.IsEnabled = true;
            }
            else
            {
                previewBtn.IsEnabled = false;
            }

            if (pageCount == pageNum)
            {
                nextBtn.IsEnabled = false;
            }
            else
            {
                nextBtn.IsEnabled = true;
            }

            lbMenus.ItemsSource = foodData;
            lbMenus.Items.Refresh();
        }

        private void orderBtn_Click(object sender, RoutedEventArgs e) // 주문 버튼 클릭 시
        {
            OrderData.sumMoney = setTotalPrice();
            NavigationService.Navigate(new Uri("/Pages/ChooseDiningPage.xaml", UriKind.Relative));
        }

        private void lbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e) // 카테고리가 바뀔 때
        {
            if (lbCategory.SelectedIndex == -1) return;
            pageCount = 0;
            Data.Category category = (Data.Category)lbCategory.SelectedIndex;

            if (lbCategory.SelectedIndex == 0)
            {
                lbMenus.ItemsSource = App.burgerList;
                previewBtn.IsEnabled = false;
                nextBtn.IsEnabled = true;
            }
            else if (lbCategory.SelectedIndex == 1)
            {
                lbMenus.ItemsSource = App.drinkList;
                previewBtn.IsEnabled = false;
                nextBtn.IsEnabled = true;
            }
            else if (lbCategory.SelectedIndex == 2)
            {
                lbMenus.ItemsSource = App.sideList;
                previewBtn.IsEnabled = false;
                nextBtn.IsEnabled = true;
            }
        }

        private void menuList_SelectionChanged(object sender, SelectionChangedEventArgs e) // 메뉴 클릭 시
        {
            int flag = 0;

            if (lbMenus.SelectedIndex == -1)
            {
                return;
            }

            Data.Menu food = (Data.Menu)lbMenus.SelectedItem;

            if (food.soldOut == 0)
            {
                for (int i = 0; i < OrderData.menuList.Count; i++)
                {
                    if (food.name == OrderData.menuList[i].name)
                    {
                        OrderData.menuList[i].count++;
                        flag = 1;
                        break;
                    }
                }

                if (flag == 0)
                {
                    food.count++;

                    OrderData.menuList.Add(food);
                }
            }

            if (food.soldOut == 1) // 품절된 상품일때
            {
                MessageBox.Show("품절된 메뉴입니다.", "안내");
            }

            setTotalPrice();
            totalPrice.Text = OrderData.sumMoney + "원";
            lvSelected.Items.Refresh();
            orderBtn_IsEnbled();

            lbMenus.SelectedIndex = -1;

            lvSelected.ItemsSource = OrderData.menuList;
        }

        private void countBtn_Click(object sender, RoutedEventArgs e) // 선택한 메뉴의 -, + 버튼 클릭 시
        {
            var name = ((Button)sender).Name;
            var food = ((ListViewItem)lvSelected.ContainerFromElement(sender as Button)).Content as Data.Menu;

            if (name == "addBtn")
            {
                food.count++;
                setTotalPrice();
            }
            else
            {
                if (food.count == 1)
                {
                    food.count = 0;

                    foodRemove(food);
                }
                else
                {
                    food.count--;
                }

                orderBtn_IsEnbled();
            }

            refreshFood();
        }

        private void foodRemove(Data.Menu food) // 선택한 메뉴 삭제
        {
            var itemSource = lvSelected.ItemsSource as List<Data.Menu>;
            itemSource.Remove(food);
            refreshFood();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e) // 선택한 메뉴 삭제 버튼 클릭 시
        {
            var food = ((ListViewItem)lvSelected.ContainerFromElement(sender as Button)).Content as Data.Menu;
            food.count = 0;

            foodRemove(food);
            orderBtn_IsEnbled();
            refreshFood();

            totalPrice.Text = OrderData.sumMoney + "원";
        }

        private void orderBtn_IsEnbled() // 버튼 비활성화/활성화
        {
            if (OrderData.menuList.Count != 0)
            {
                orderBtn.IsEnabled = true;
                deleteAllBtn.IsEnabled = true;
            }
            else
            {
                orderBtn.IsEnabled = false;
                deleteAllBtn.IsEnabled = false;
            }
        }

        private void deleteAllBtn_Click(object sender, RoutedEventArgs e) // 메뉴 전체 삭제 클릭 시
        {
            if (MessageBox.Show("선택한 메뉴를 모두 삭제하시겠습니까?", "안내", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                orderMenuClear();
                orderBtn_IsEnbled();
                MessageBox.Show("삭제되었습니다.", "안내");
            }
            else
            {
                return;
            }
        }


        private void prevPageBtn_Click(object sender, RoutedEventArgs e) // 주문 취소 버튼 클릭 (이전)
        {
            if (OrderData.menuList.Count != 0)
            {
                if (MessageBox.Show("주문을 취소하시겠습니까?", "안내", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    orderMenuClear();
                    NavigationService.Navigate(new Uri("/Pages/HomePage.xaml", UriKind.Relative));
                }
                else
                {
                    return;
                }
            }
            else
            {
                NavigationService.Navigate(new Uri("/Pages/HomePage.xaml", UriKind.Relative));
            }
        }

        private void orderMenuClear() // 주문한 메뉴 전체 삭제
        {
            for (int i = 0; i < OrderData.menuList.Count; i++)
            {
                OrderData.menuList[i].count = 0;
            }

            OrderData.menuList.Clear();
            lvSelected.Items.Refresh();
            totalPrice.Text = "" + "원";
        }

        private void refreshFood() // 계산한 총 금액 출력
        {
            totalPrice.Text = setTotalPrice() + "원";
            lvSelected.Items.Refresh();
        }

        private int setTotalPrice() // 총 금액 계산
        {
            int total = 0;

            for (int i = 0; i < OrderData.menuList.Count; i++)
            {
                total += OrderData.menuList[i].count * OrderData.menuList[i].sale;
            }

            OrderData.sumMoney = total;

            return OrderData.sumMoney;
        }
    }
}