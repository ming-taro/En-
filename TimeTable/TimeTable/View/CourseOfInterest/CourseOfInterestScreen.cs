using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class CourseOfInterestScreen
    {
        public void PrintMenu()
        {
            Logo logo = new Logo();

            Console.Clear();
            logo.PrintLine((int)Constants.Console.LEFT, (int)Constants.Console.MIN_TOP);
            logo.PrintMenu((int)Constants.MainMenu.LOGO_LEFT + 5, (int)Constants.MainMenu.LOGO_TOP, "[관심과목 담기]");
            logo.PrintMenu((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FIRST, "☞분야별 과목 검색");
            logo.PrintMenu((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.SECOND, "☞담은 강의 내역");
            logo.PrintMenu((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.THIRD, "☞관심과목 시간표");
            logo.PrintMenu((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FOURTH, "☞관심과목 삭제");
            logo.PrintLine((int)Constants.Console.LEFT, (int)Constants.Console.MAX_TOP);
        }
        public void PrintSearchMenu()
        {
            Logo logo = new Logo();

            Console.Clear();
            logo.PrintLine((int)Constants.Console.LEFT, (int)Constants.Console.MIN_TOP);
            logo.PrintMenu((int)Constants.MainMenu.LOGO_LEFT, (int)Constants.MainMenu.LOGO_TOP, "[분야별 과목 검색]");
            logo.PrintMenu((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FIRST, "☞학과전공");
            logo.PrintMenu((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.SECOND, "☞학수번호/분반");
            logo.PrintMenu((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.THIRD, "☞학년");
            logo.PrintMenu((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FOURTH, "☞교과목명");
            logo.PrintMenu((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FIFTH, "☞교수명");
            logo.PrintLine((int)Constants.Console.LEFT, (int)Constants.Console.MAX_TOP);

        }
    }
}
