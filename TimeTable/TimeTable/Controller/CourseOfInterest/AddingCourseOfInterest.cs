using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class AddingCourseOfInterest
    {
        private void SelectMenu()
        {
            CourseOfInterestScreen courseOfInterestScreen = new CourseOfInterestScreen();
            Keyboard keyboard = new Keyboard((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FIRST);
            int menu;

            while (Constants.KEYBOARD_OPERATION)
            {
                courseOfInterestScreen.PrintSearchMenu();
                menu = keyboard.SelectTop((int)Constants.MainMenu.FIRST, (int)Constants.MainMenu.FIFTH, (int)Constants.MainMenu.STEP);
                if (menu == (int)Constants.Keyboard.ESCAPE) return;  //esc클릭 -> 관심과목 담기 메뉴 화면으로 돌아감
                menu = keyboard.Top;

                switch (menu)
                {
                    case (int)Constants.MainMenu.FIRST:   //학과전공
                        DepartmentMajor departmentMajor = new DepartmentMajor();
                        departmentMajor.SearchMajor();
                        break;
                    case (int)Constants.MainMenu.SECOND:  //학수번호/분반

                        break;
                    case (int)Constants.MainMenu.THIRD:   //학년

                        break;
                    case (int)Constants.MainMenu.FOURTH:  //교수명

                        break;

                }
            }
        }
        public void ShowCourseOfInterestMenu()
        {
            CourseOfInterestScreen courseOfInterestScreen = new CourseOfInterestScreen();
            Keyboard keyboard = new Keyboard((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FIRST);
            int menu;

            while (Constants.KEYBOARD_OPERATION)
            {
                courseOfInterestScreen.PrintMenu(); 
                menu = keyboard.SelectTop((int)Constants.MainMenu.FIRST, (int)Constants.MainMenu.FOURTH, (int)Constants.MainMenu.STEP);
                if (menu == (int)Constants.Keyboard.ESCAPE) return;  //esc클릭 -> 로그인 화면으로 돌아감
                menu = keyboard.Top;

                switch (menu)
                {
                    case (int)Constants.MainMenu.FIRST:   //분야별 과목 검색
                        SelectMenu();
                        break;
                    case (int)Constants.MainMenu.SECOND:  //담은 강의 내역

                        break;
                    case (int)Constants.MainMenu.THIRD:   //관심과목 시간표

                        break;
                    case (int)Constants.MainMenu.FOURTH:  //관심과목 삭제

                        break;

                }
            }
            

        }
    }
}
