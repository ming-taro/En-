using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class ViewingScheduleScreen
    {
        public void PrintMenu(string menu)
        {
            Console.Clear();
            Logo logo = new Logo();
            logo.PrintTwoLine();
            logo.PrintMenu((int)Constants.ColumnMenu.LOGO_LEFT, (int)Constants.ColumnMenu.LOGO_TOP, "[강의 시간표 조회]");
            logo.PrintMenu((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.FIRST, "☞학과전공");
            logo.PrintMenu((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.SECOND, menu);
            logo.PrintMenu((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.THIRD, "☞학년");
            logo.PrintMenu((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.FOURTH, "☞교과목명");
            logo.PrintMenu((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.FIFTH, "☞교수명");
            logo.PrintMenu((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.SIXTH, "☞조회");
        }

    }
}
