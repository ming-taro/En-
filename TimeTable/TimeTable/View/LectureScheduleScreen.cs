using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class LectureScheduleScreen
    {

        public void PrintMenu()
        {
            Logo logo = new Logo();

            Console.Clear();
            logo.PrintLine(1,1);
            logo.PrintMenu(49, 3, "[강좌조회 및 수강신청]");
            logo.PrintMenu(50, 10, "☞강의 시간표 조회");
            logo.PrintMenu(50, 12, "☞관심과목 담기");
            logo.PrintMenu(50, 14, "☞수강신청");
            logo.PrintMenu(50, 16, "☞수강 내역 조회");
            logo.PrintLine(1, 22);
        }
    }
}
