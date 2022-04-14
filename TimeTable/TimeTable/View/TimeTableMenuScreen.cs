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
            logo.PrintMenu(58, 3, "[강좌조회 및 수강신청]");
            logo.PrintMenu(60, 9, "☞강의 시간표 조회");
            logo.PrintMenu(60, 11, "☞관심과목 담기");
            logo.PrintMenu(60, 13, "☞수강신청");
            logo.PrintMenu(60, 15, "☞수강 내역 조회");
            logo.PrintLine(11, 22);
        }
    }
}
