using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class ViewingLectureSchedule
    {


        public void ViewLectureSchedule()
        {
            ViewingScheduleScreen viewingScheduleScreen = new ViewingScheduleScreen();
            viewingScheduleScreen.PrintMenu();

            Keyboard keyboard = new Keyboard(5, (int)Constants.LectureSchedule.DEPARTMENT);
            int menu = keyboard.SelectMenu((int)Constants.LectureSchedule.DEPARTMENT, (int)Constants.LectureSchedule.SEARCH, 3);
            if (menu == (int)Constants.Keyboard.ESCAPE) return;  //메뉴선택 중 esc -> 뒤로가기(강의조회 및 수강신청 화면으로)

        }

    }
}
