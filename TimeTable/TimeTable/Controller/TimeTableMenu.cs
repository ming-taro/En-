using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class TimeTableMenu
    {
        private void SelectMenu()
        {
            TimeTableMenuScreen timeTableMenuScreen = new TimeTableMenuScreen();
            timeTableMenuScreen.PrintMenu();

            Keyboard keyboard = new Keyboard(50, 9);
            int menu = keyboard.SelectMenu(9, 15, 2);            //강좌조회 및 수강신청 : 메뉴선택
            if (menu == (int)Constants.Keyboard.ESCAPE) return;  //esc클릭 -> 로그인 화면으로 돌아감
            menu = keyboard.Top;

            switch (menu)
            {
                case 9:   //강의 시간표 조회
                    ViewingLectureSchedule viewingLectureSchedule = new ViewingLectureSchedule();
                    viewingLectureSchedule.ViewLectureSchedule();
                    break;
                case 11:  //관심과목 담기

                    break;
                case 13:  //수강신청

                    break;
                case 15:  //수강내역 조회

                    break;

            }
        }

        public void ShowTimeTableMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Logo logo = new Logo();
            

            LogIn logIn = new LogIn();

            while (Constants.LOGIN_IN)
            {
                logo.PrintMain();           //메인화면 출력
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
