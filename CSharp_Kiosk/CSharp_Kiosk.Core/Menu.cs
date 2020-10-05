using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Kiosk.Core
{
    public class Menu
    {
        public String Name { get; set; }
        public String Image { get; set; }
        public String Category { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }

        public Menu Clone()
        {
            var returnValue = new Menu
            {
                Name = this.Name,
                Image = this.Image,
                Category = this.Category,
                Price = this.Price,
                Count = this.Count
            };

            return returnValue;
        }
    }
}
