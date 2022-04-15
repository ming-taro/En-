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
        public void PrintInputBox(int appliedCredit)
        {
            Logo logo = new Logo();

            logo.PrintMenu((int)Constants.Credit.FIRST, (int)Constants.Credit.TOP, "◇등록가능학점: " + (24 - appliedCredit));
            logo.PrintMenu((int)Constants.Credit.SECOND, (int)Constants.Credit.TOP, "◇담은 학점: " + appliedCredit);
            logo.PrintMenu((int)Constants.Credit.THIRD, (int)Constants.Credit.TOP, "◇담을 과목 순번:");
        }
    }
}
