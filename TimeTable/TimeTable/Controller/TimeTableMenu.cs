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

            Keyboard keyboard = new Keyboard(60, 9);
            int menu = keyboard.SelectTop(9, 15, 2);            //강좌조회 및 수강신청 : 메뉴선택
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
            logo.PrintMain();           //메인화면 출력

            LogIn logIn = new LogIn();

            while (Constants.LOGIN_IN)
            {
                if (logIn.LogInToWebsite() == Constants.LOGIN_IN)
                {
                    SelectMenu();
                    logo.PrintMain();           //메인화면 출력
                }
                else
                {
                    logo.FailureMessage(80, 14, "(학번이나 비밀번호가 일치하지 않습니다.)");  //로그인 실패 메세지 출력
                    logo.RemoveLine(100, 9);
                    logo.RemoveLine(112, 12);
                }
            }
        }
    }
}
