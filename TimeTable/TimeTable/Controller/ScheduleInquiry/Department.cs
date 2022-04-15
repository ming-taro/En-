using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Department
    {
        private string ShowMenu(int menu)
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();
            searchCategoryScreen.PrintDepartmentMenu();

            switch (menu)
            {
                case (int)Constants.RowMenu.FIRST:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.ColumnMenu.FIRST, "▷전체");
                    break;
                case (int)Constants.RowMenu.SECOND:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.ColumnMenu.FIRST, "▷데이터사이언스학과");
                    return "데이터사이언스학과";
                case (int)Constants.RowMenu.THIRD:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.ColumnMenu.FIRST, "▷컴퓨터공학과");
                    return "컴퓨터공학과";
                case (int)Constants.RowMenu.FOURTH:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.ColumnMenu.FIRST, "▷미디어커뮤니케이션학과");
                    return "미디어커뮤니케이션학과";
            }

            return "";
        }
        public void SelectMenu(ref string department)  //학과전공 선택(커서 좌우로 이동)
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();
            if (department == null || department.Equals("")) searchCategoryScreen.PrintDepartmentMenu();      //학과 선택 화면 출력

            Keyboard keyboard = new Keyboard((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.FIRST);
            int menu = keyboard.SelectLeft((int)Constants.RowMenu.FIRST, (int)Constants.RowMenu.FOURTH, (int)Constants.RowMenu.STEP);   //학과선택

            if (menu == (int)Constants.Keyboard.ESCAPE) return;  //학과선택 중 뒤로가기
            menu = keyboard.Left;

            department = ShowMenu(menu);   //학과이름 저장
        }
    }
}
