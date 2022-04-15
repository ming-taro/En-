using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class SearchByFieldScreen
    {
        public void PrintLine()
        {
            Logo logo = new Logo();

            Console.Clear();
            logo.PrintLine((int)Constants.Console.LEFT, (int)Constants.Console.MIN_TOP);
            logo.PrintLine((int)Constants.Console.LEFT, (int)Constants.Console.MiDLIE_TOP);
            logo.PrintMenu((int)Constants.RowMenu.FOURTH, (int)Constants.Console.MiDLIE_TOP - 2, "[ESC]:뒤로가기       [ENTER]:조회");
        }
    }
}
