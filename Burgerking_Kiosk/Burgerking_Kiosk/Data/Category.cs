using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerking_Kiosk.Data
{
    public enum Category
    {
        BURGERS,
        DRINKS,
        SIDES,
    }

    public class CategoryMenu
    {
        public string name { get; set; }
        public Category category { get; set; }
    }

    public class CategoryData
    {
        public List<CategoryMenu> lstCategory { get; set; }
        public void Load()
        {
            if(lstCategory == null)
            {
                lstCategory = new List<CategoryMenu>()
                {
                    new CategoryMenu() {name = "햄버거", category = Category.BURGERS},
                    new CategoryMenu() {name = "음료수", category = Category.DRINKS},
                    new CategoryMenu() {name = "사이드", category = Category.SIDES},
                };
            }
        }
    }
}
