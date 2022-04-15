using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class TimeTableMenuScreen
    {
        public void PrintMenu()
        {
            Logo logo = new Logo();

            Console.Clear();
            logo.PrintLine(11,1);
            logo.PrintMenu((int)Constants.MainMenu.LOGO_LEFT, (int)Constants.MainMenu.LOGO_TOP, "[강좌조회 및 수강신청]");
            logo.PrintMenu((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FIRST, "☞강의 시간표 조회");
            logo.PrintMenu((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.SECOND, "☞관심과목 담기");
            logo.PrintMenu((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.THIRD, "☞수강신청");
            logo.PrintMenu((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FOURTH, "☞수강 내역 조회");
            logo.PrintLine(11, 22);
        }
    }
}
