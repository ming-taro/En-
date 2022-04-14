using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class ViewingLectureSchedule
    {
        private void SelectMenu()
        {
            LectureScheduleScreen lectureScheduleScreen = new LectureScheduleScreen();
            lectureScheduleScreen.PrintMenu();

            Keyboard keyboard = new Keyboard(50, 10);
            int menu = keyboard.SelectMenu(10, 16, 2);   //강좌조회 및 수강신청 : 메뉴선택
            if (menu == (int)Constants.Keyboard.ESCAPE) return;  //esc클릭 -> 로그인 화면으로 돌아감
            
        }

        public void ViewLectueSchedule()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Logo logo = new Logo();
            logo.PrintMain();           //메인화면 출력

            LogIn logIn = new LogIn();

            while (Constants.LOGIN_IN)
            {
                if (logIn.LogInToWebsite() == Constants.LOGIN_IN) SelectMenu();
                else
                {
                    logo.FailureMessage(60, 14, "(학번이나 비밀번호가 일치하지 않습니다.)");  //로그인 실패 메세지 출력
                    logo.RemoveLine(80, 9);
                    logo.RemoveLine(92, 12);
                }
            }
        }
    }
}
