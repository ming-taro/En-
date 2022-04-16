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

            logo.RemoveLine((int)Constants.Credit.FIRST, (int)Constants.Credit.TOP);
            logo.PrintMenu((int)Constants.Credit.FIRST, (int)Constants.Credit.TOP, "◇등록가능학점: " + (24 - appliedCredit).ToString());
            logo.PrintMenu((int)Constants.Credit.SECOND, (int)Constants.Credit.TOP, "◇담은 학점: " + appliedCredit);
            logo.PrintMenu((int)Constants.Credit.THIRD, (int)Constants.Credit.TOP, "◇담을 과목 순번:");
            logo.RemoveLine((int)Constants.Credit.THIRD + 18, (int)Constants.Credit.TOP);  //순번 입력값 지우기
            logo.RemoveLine((int)Constants.RowMenu.SECOND, (int)Constants.Credit.TOP - 2); //메세지 지우기
        }
        public void PrintDeletionInputBox(int appliedCredit)
        {
            Logo logo = new Logo();

            logo.RemoveLine((int)Constants.Credit.FIRST, (int)Constants.ColumnMenu.LOGO_TOP + 6);
            logo.PrintMenu((int)Constants.Credit.FIRST, (int)Constants.ColumnMenu.LOGO_TOP + 6, "◇등록가능학점: " + (24 - appliedCredit).ToString());
            logo.PrintMenu((int)Constants.Credit.SECOND, (int)Constants.ColumnMenu.LOGO_TOP + 6, "◇담은 학점: " + appliedCredit);
            logo.PrintMenu((int)Constants.Credit.THIRD, (int)Constants.ColumnMenu.LOGO_TOP + 6, "◇삭제할 과목 순번:");
            logo.RemoveLine((int)Constants.Credit.THIRD + 20, (int)Constants.ColumnMenu.LOGO_TOP + 6);  //순번 입력값 지우기
            logo.RemoveLine((int)Constants.RowMenu.SECOND, (int)Constants.ColumnMenu.LOGO_TOP + 4); //메세지 지우기
        }
        public void PrintResult(int appliedCredit, List<CourseVO> lectureSchedule, CourseVO courseVO)
        {
            PrintInputBox(appliedCredit);
            LectureScheduleScreen lectureScheduleScreen = new LectureScheduleScreen();
            lectureScheduleScreen.PrintLectureSchedule((int)Constants.Credit.TOP + 2, lectureSchedule, courseVO);//목록을 보여줌
        }
        public void PrintSuccessMessage()
        {
            Logo logo = new Logo();

            Console.ForegroundColor = ConsoleColor.Yellow;
            logo.PrintMenu((int)Constants.RowMenu.SECOND, (int)Constants.Credit.TOP - 2, ">>>관심과목 담기에 성공했습니다!<<<");
            logo.PrintMenu((int)Constants.RowMenu.FOURTH, (int)Constants.Credit.TOP - 2, "[ESC]:뒤로가기       [ENTER]:재조회");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void PrinFaliureMessage(string message)
        {
            Logo logo = new Logo();

            Console.ForegroundColor = ConsoleColor.Red;
            logo.PrintMenu((int)Constants.RowMenu.SECOND, (int)Constants.Credit.TOP - 2, message);
            logo.PrintMenu((int)Constants.RowMenu.FOURTH, (int)Constants.Credit.TOP - 2, "[ESC]:뒤로가기       [ENTER]:재조회");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
