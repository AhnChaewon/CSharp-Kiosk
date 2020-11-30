using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerking_Kiosk.Network
{
    class JsonMsg
    {
        public int MSGType { get; set; }
        public int Id { get; set; }
        public String Content { get; set; }
        public String ShopName { get; set; }
        public String OrderNumber { get; set; }
        public Boolean Group { get; set; }
        public List<ServerMenu> Menus { get; set; }

        public JsonMsg(int MSGType, int Id, String Content, String ShopName, String OrderNumber, Boolean Group, List<ServerMenu> Menus) 
        {
            this.MSGType = MSGType;
            this.Id = Id;
            this.Content = Content;
            this.ShopName = ShopName;
            this.OrderNumber = OrderNumber;
            this.Group = Group;
            this.Menus = Menus;
        }

    }
    class ServerMenu
    {
        public String Name { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }

}
