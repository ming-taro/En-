using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class CourseOfInterest
    {
        
        public void ShowCourseHistory(List<CourseVO> courseOfInterest)
        {
            Logo logo = new Logo();
            Console.Clear();
            logo.PrintMenu((int)Constants.ColumnMenu.LOGO_LEFT, (int)Constants.ColumnMenu.LOGO_TOP, "[담은 강의 내역]");

            CourseHistoryScreen courseHistoryScreen = new CourseHistoryScreen();
            courseHistoryScreen.PrintCourseHistory((int)Constants.ColumnMenu.LOGO_TOP + 4, courseOfInterest);           //현재 담겨진 관심과목 내역

            logo.PrintMenu((int)Constants.RowMenu.FIFTH, (int)Constants.ColumnMenu.LOGO_TOP + 2, "[ESC]: 뒤로가기");

            Keyboard keyboard = new Keyboard();
            keyboard.PressESC();               //과목리스트 출력 후 esc -> 뒤로가기
        }
    }
}
