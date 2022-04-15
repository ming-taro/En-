using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Searching
    {
        public void ShowLectureSchedule(string department, string completionType, string grade, string courseTitle, string instructor)
        {
            LectureScheduleScreen lectureScheduleScreen = new LectureScheduleScreen();
            lectureScheduleScreen.PrintLectureSchedule((int)Constants.CONSOLE_HEIGHT + 4, department, completionType, grade, courseTitle, instructor);

            Logo logo = new Logo();
            logo.PrintMenu((int)Constants.GO_BACK_BUTTON_LEFT, Console.CursorTop, "☜뒤로가기");
            Keyboard keyboard = new Keyboard((int)Constants.GO_BACK_BUTTON_LEFT, Console.CursorTop);
            keyboard.PressESC();   //뒤로가기(esc) -> 조회 종료
        }

    }
}
