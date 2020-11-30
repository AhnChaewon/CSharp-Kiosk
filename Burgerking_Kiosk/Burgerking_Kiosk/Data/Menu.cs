using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerking_Kiosk.Data
{ 
    public class Menu
    {
        public int id { get; set; }
        public Category category { get; set; }
        public String name { get; set; }
        public string imagePath { get; set; }
        public int count { get; set; }

        public int sale { get; set; }
        public int price { get; set; }
        public int page { get; set; }
        public int soldOut { get; set; }
    }
}
