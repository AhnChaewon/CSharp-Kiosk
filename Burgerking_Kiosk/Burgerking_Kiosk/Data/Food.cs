using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Burgerking_Kiosk.Data
{
    public class Food
    {
        public Category category { get; set; }
        public string path;
        public string name { get; set; }
        public int price { get; set; }
        public int count { get; set; }
        public string imagePath { get; set; }
        public int page { get; set; }
    }

    public class FoodData
    {
        public List<Food> lstFood { get; set; }
        public void Load()
        {
            if (lstFood == null)
            {
                lstFood = new List<Food>()
                {
                    new Data.Food()
                    {
                        category = Data.Category.BURGERS, name = "불고기버거", price = 3200, imagePath = "/Assets/Burgers/burger1.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.BURGERS, name = "불고기와퍼주니어", price = 4700, imagePath = "/Assets/Burgers/burger2.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.BURGERS, name = "와퍼주니어", price = 4700, imagePath = "/Assets/Burgers/burger3.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.BURGERS, name = "치즈버거", price = 3200, imagePath = "/Assets/Burgers/burger4.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.BURGERS, name = "치즈와퍼주니어", price = 4600, imagePath = @"/Assets/Burgers/burger5.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.BURGERS, name = "콰트로치즈와퍼주니어", price = 4900, imagePath = @"/Assets/Burgers/burger6.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.BURGERS, name = "통새우와퍼주니어", price = 4900, imagePath = @"/Assets/Burgers/burger7.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.BURGERS, name = "트러플머쉬룸와퍼주니어", price = 4900, imagePath = @"/Assets/Burgers/burger8.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.BURGERS, name = "할라피뇨불고기버거", price = 3100, imagePath = @"/Assets/Burgers/burger9.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.BURGERS, name = "할라피뇨와퍼주니어", price = 3100, imagePath = @"/Assets/Burgers/burger10.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.DRINKS, name = "미닛메이드오렌지", price = 2700, imagePath = @"/Assets/Drinks/drink1.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.DRINKS, name = "순수미네랄워터", price = 1500, imagePath = @"/Assets/Drinks/drink2.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.DRINKS, name = "스프라이트", price = 1700, imagePath = @"/Assets/Drinks/drink3.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.DRINKS, name = "씨그램", price = 1700, imagePath = @"/Assets/Drinks/drink4.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.DRINKS, name = "아메리카노", price = 1500, imagePath = @"/Assets/Drinks/drink5.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.DRINKS, name = "아이스아메리카노", price = 2100, imagePath = @"/Assets/Drinks/drink6.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.DRINKS, name = "아이스초코", price = 2000, imagePath = @"/Assets/Drinks/drink7.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.DRINKS, name = "제로톡톡복숭아", price = 2000, imagePath = @"/Assets/Drinks/drink8.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.DRINKS, name = "제로톡톡청포도", price = 2000, imagePath = @"/Assets/Drinks/drink9.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.DRINKS, name = "제로톡톡체리", price = 2000, imagePath = @"/Assets/Drinks/drink10.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.DRINKS, name = "코카콜라", price = 1700, imagePath = @"/Assets/Drinks/drink11.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.DRINKS, name = "코카콜라제로", price = 1700, imagePath = @"/Assets/Drinks/drink12.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.DRINKS, name = "핫초코", price = 2000, imagePath = @"/Assets/Drinks/drink13.png", page = 3
                    },
                    new Data.Food()
                    {
                        category = Data.Category.SIDES, name = "21치즈스틱", price = 1500, imagePath = @"/Assets/Sides/side1.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.SIDES, name = "너겟킹(10조각)", price = 5000, imagePath = @"/Assets/Sides/side2.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.SIDES, name = "너겟킹(6조각)", price = 3000, imagePath = @"/Assets/Sides/side3.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.SIDES, name = "너겟킹(4조각)", price = 2000, imagePath = @"/Assets/Sides/side4.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.SIDES, name = "바삭킹(4조각)", price = 5000, imagePath = @"/Assets/Sides/side5.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.SIDES, name = "바삭킹(2조각)", price = 2500, imagePath = @"/Assets/Sides/side6.png", page = 1
                    },
                    new Data.Food()
                    {
                        category = Data.Category.SIDES, name = "어니언링", price = 2000, imagePath = @"/Assets/Sides/side7.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.SIDES, name = "칠리크랩치즈프라이", price = 2500, imagePath = @"/Assets/Sides/side8.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.SIDES, name = "크리미모짜볼(10조각)", price = 4300, imagePath = @"/Assets/Sides/side9.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.SIDES, name = "크리미모짜볼(5조각)", price = 2200, imagePath = @"/Assets/Sides/side10.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.SIDES, name = "트러플프라이", price = 2800, imagePath = @"/Assets/Sides/side11.png", page = 2
                    },
                    new Data.Food()
                    {
                        category = Data.Category.SIDES, name = "프렌치프라이", price = 2100, imagePath = @"/Assets/Sides/side12.png", page = 2
                    },
                };
            }
        }
    }
}
