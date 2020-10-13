using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;

namespace Burgerking_Kiosk.Data
{
    class MemberData
    {
        public String name;
        public String cardNum;
        public int money;

        public MemberData(String name, String cardNum, int money)
        {
            this.name = name;
            this.cardNum = cardNum;
            this.money = money;
        }

    }
}
