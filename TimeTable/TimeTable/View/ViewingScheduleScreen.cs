using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class ViewingScheduleScreen
    {
        public void PrintMenu()
        {
            Console.Clear();
            Logo logo = new Logo();
            logo.PrintTwoLine();
            logo.PrintMenu(56, 3, "[강의 시간표 조회]");
            logo.PrintMenu(15, (int)Constants.ColumnMenu.FIRST, "☞학과전공");
            logo.PrintMenu(15, (int)Constants.ColumnMenu.SECOND, "☞이수구분");
            logo.PrintMenu(15, (int)Constants.ColumnMenu.THIRD, "☞학년");
            logo.PrintMenu(15, (int)Constants.ColumnMenu.FOURTH, "☞교과목명");
            logo.PrintMenu(15, (int)Constants.ColumnMenu.FIFTH, "☞교수명");
            logo.PrintMenu(15, (int)Constants.ColumnMenu.SIXTH, "☞조회");
        }
    }
}
