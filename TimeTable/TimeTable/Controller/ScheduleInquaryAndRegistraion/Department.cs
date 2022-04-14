using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Department
    {
        public void ShowMenu(int menu)
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();

            switch (menu)
            {
                case (int)Constants.Department.ALL:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.LectureSchedule.DEPARTMENT, "▷전체");
                    break;
                case (int)Constants.Department.COMPUTER_ENGINEERING:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.LectureSchedule.DEPARTMENT, "▷컴퓨터공학과");
                    break;
                case (int)Constants.Department.DATA_SCIENCE:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.LectureSchedule.DEPARTMENT, "▷데이터사이언스과");
                    break;
                case (int)Constants.Department.HISTORY:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.LectureSchedule.DEPARTMENT, "▷역사학과");
                    break;
            }
        }
        public int SelectMenu()  //학과전공 선택(커서 좌우로 이동)
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();
            searchCategoryScreen.PrintDepartmentMenu();        //학과 선택 화면 출력

            Keyboard keyboard = new Keyboard((int)Constants.Department.ALL, (int)Constants.LectureSchedule.DEPARTMENT);
            int menu = keyboard.SelectLeft((int)Constants.Department.ALL, (int)Constants.Department.HISTORY, (int)Constants.Department.STEP);   //학과선택

            if (menu == (int)Constants.Keyboard.ESCAPE) return (int)Constants.Keyboard.ESCAPE;  //학과선택 중 뒤로가기
            menu = keyboard.Left;

            ShowMenu(menu);

            return (int)Constants.Keyboard.ENTERING_MENU;     //학과를 선택함
        }
    }
}
