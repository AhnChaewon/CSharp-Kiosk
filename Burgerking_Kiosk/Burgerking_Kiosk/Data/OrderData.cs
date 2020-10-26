using MySqlX.XDevAPI.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Burgerking_Kiosk.Data
{
    class OrderData
    {

        public static int table = -1;
        public static String payment;
        public static String member;
        public static List<Menu> menuList = new List<Menu>();

    }
}
