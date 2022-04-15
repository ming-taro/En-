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
        public void PrintInputBox(int appliedCredit, List<CourseVO> lectureSchedule, CourseVO courseVO)
        {
            Logo logo = new Logo();

            logo.PrintMenu((int)Constants.Credit.FIRST, (int)Constants.Credit.TOP, "◇등록가능학점: " + (24 - appliedCredit));
            logo.PrintMenu((int)Constants.Credit.SECOND, (int)Constants.Credit.TOP, "◇담은 학점: " + appliedCredit);
            logo.PrintMenu((int)Constants.Credit.THIRD, (int)Constants.Credit.TOP, "◇담을 과목 순번:");

            LectureScheduleScreen lectureScheduleScreen = new LectureScheduleScreen();
            lectureScheduleScreen.PrintLectureSchedule((int)Constants.Credit.TOP + 2, lectureSchedule, courseVO);//목록을 보여줌
        }
        public void PrintSuccessMessage()
        {
            Logo logo = new Logo();

            logo.PrintMenu((int)Constants.RowMenu.SECOND, (int)Constants.Credit.TOP - 2, ">>>관심과목 담기에 성공했습니다!<<<");
            logo.PrintMenu((int)Constants.RowMenu.FOURTH, (int)Constants.Credit.TOP - 2, "[ESC]:뒤로가기       [ENTER]:재조회");
        }
    }
}
