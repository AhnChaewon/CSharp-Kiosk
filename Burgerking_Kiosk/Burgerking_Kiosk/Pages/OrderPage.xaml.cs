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
        List<Data.Menu> foodData = new List<Data.Menu>();
        List<Data.Menu> showFood = new List<Data.Menu>(); // 보여지는 리스트

        int categorySelect = 1;

        public OrderPage()
        {
            InitializeComponent();
            this.Loaded += OrderPage_Loaded;
            InitData();
        }

        //주석 부분은 나중에 다시 정리할 예정
        //private void insertNowMenu(int pageNum)
        //{
        //    for(int i=MenuCount; i<MenuCount+6; i++)
        //    {
        //        if(foodData[i].page!= pageNum)
        //        {
        //            return;
        //        }
        //            showFood.Add(foodData[i]);
        //    }

        //}

        private void navBtn_Click(object sender, RoutedEventArgs e) // 메뉴 목록 이전 버튼
        {
            //var name = ((Button)sender).Name;
            //MenuCount += 6;
            //lbMenus.ItemsSource = null;
            //insertNowMenu(categorySelect);

            //lbMenus.ItemsSource = foodData;

            //if (name == "nextBtn")
            //{
            //    if (pageCount == 1)
            //    {
            //        for (int i = 5; i < 11; i++)
            //        {
            //            showFood.Add(foodData[i]);
            //        }
            //        pageCount++;
            //    }
            //    else if (pageCount == 2)
            //    {
            //        for (int i = 11; i < 17; i++)
            //        {
            //            showFood.Add(foodData[i]);
            //        }
            //        pageCount = 1;
            //    }
            //}
        }

        private void OrderPage_Loaded(object sender, RoutedEventArgs e)
        {
            lbCategory.SelectedIndex = 0;
            lvSelected.ItemsSource = OrderData.menuList;
            totalPrice.Text = OrderData.sumMoney + "원";
            btnIsEnbled();
        }

        private void InitData()
        {
            categoryData.Load();
            lbCategory.ItemsSource = categoryData.lstCategory;

            DBMenu db = new DBMenu();
            foodData = db.GetMenu();

            lbMenus.ItemsSource = foodData;

            //if (OrderData.menuList.Count != 0)
            //{
            //    foods = OrderData.menuList;
            //}

            //lvSelected.ItemsSource = OrderData.menuList;


        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderData.sumMoney = setTotalPrice();
            NavigationService.Navigate(new Uri("/Pages/ChooseDiningPage.xaml", UriKind.Relative));
        }

        private void lbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCategory.SelectedIndex == -1) return;
            Data.Category category = (Data.Category)lbCategory.SelectedIndex;
            //Debug.WriteLine(category.ToString());
            //if (category.ToString().Equals("BURGERS"))
            //{
            //    categorySelect = 1;
            //}
            //else if (category.ToString().Equals("DRINKS"))
            //{
            //    categorySelect = 2;
            //    MenuCount = 11;


            //}
            //else
            //{
            //    categorySelect = 3;
            //    MenuCount = 24;

            //}

            lbMenus.ItemsSource = foodData.Where(x => x.category == category).ToList();

        }

        private void menuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int flag = 0;

            if (lbMenus.SelectedIndex == -1)
            {
                return;
            }

            Data.Menu food = (Data.Menu)lbMenus.SelectedItem;

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

            setTotalPrice();
            totalPrice.Text = OrderData.sumMoney + "원";
            lvSelected.Items.Refresh();
            btnIsEnbled();

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
                btnIsEnbled();
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
            btnIsEnbled();
            refreshFood();
            totalPrice.Text = OrderData.sumMoney + "원";
        }

        private void btnIsEnbled() // 버튼 활성화
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
            if (OrderData.menuList.Count != 0)
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
            for (int i = 0; i < OrderData.menuList.Count; i++)
            {
                OrderData.menuList[i].count = 0;
            }
            OrderData.menuList.Clear();
            lvSelected.Items.Refresh();
            totalPrice.Text = "";
        }


        private void refreshFood() // 계산한 총 금액 출력
        {
            totalPrice.Text = setTotalPrice() + "원";
            lvSelected.Items.Refresh();

        }

        private int setTotalPrice() // 총 금액 계산
        {
            int total = 0;

            for(int i = 0; i<OrderData.menuList.Count; i++)
            {
                total += OrderData.menuList[i].count * OrderData.menuList[i].sale;
            }
            
            OrderData.sumMoney = total;

            return OrderData.sumMoney;
        }
    }
}
