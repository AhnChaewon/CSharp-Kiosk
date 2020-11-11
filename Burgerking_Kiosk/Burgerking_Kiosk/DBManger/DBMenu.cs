using Burgerking_Kiosk.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerking_Kiosk.DBManger
{
    class DBMenu
    {
        private DBConnection conDB = new DBConnection();

        public List<Menu> GetMenu()
        {
            conDB.connectDB();
            try
            {
                conDB.setCommand("select * from menu");
                MySqlDataReader reader = conDB.executeReadQuery();

                Menu menu;
                List<Menu> menus = new List<Menu>();
                int buger = 1;
                int drink = 1;
                int side = 1;

                while (reader.Read())
                {
                    menu = new Menu();
                    menu.id = (int)reader["MenuId"]; 
                    menu.name = (string)reader["Name"];
                    menu.imagePath = (string)reader["ImagePath"];
                    menu.price = (int)reader["Price"];
                    menu.sale = (int)reader["Sale"];

                    if (menu.id / 100 == 1)
                    {
                        menu.page = buger / 6;
                        menu.category = Category.BURGERS;
                        buger++;
                    }
                    else if (menu.id / 100 == 2)
                    {
                        menu.page = drink / 6;
                        menu.category = Category.DRINKS;
                        drink++;
                    }
                    else
                    {
                        menu.page = side / 6;
                        menu.category = Category.SIDES;
                        side++;
                    }

                    menus.Add(menu);
                }

                return menus;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("서버에 연결할 수 없습니다.");
                        break;

                    case 1045:
                        Console.WriteLine("ID 또는 Password를 확인해주세요.");
                        break;
                }
            }

            return null;
        }
    }
}
