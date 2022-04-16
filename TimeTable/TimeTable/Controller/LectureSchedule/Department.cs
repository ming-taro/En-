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
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.ColumnMenu.FIRST, "▷컴퓨터공학과");
                    return "컴퓨터공학과";
                case (int)Constants.RowMenu.THIRD:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.ColumnMenu.FIRST, "▷소프트웨어학과");
                    return "소프트웨어학과";
                case (int)Constants.RowMenu.FOURTH:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.ColumnMenu.FIRST, "▷지능기전공학부");
                    return "지능기전공학부";
                case (int)Constants.RowMenu.FIFTH:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.ColumnMenu.FIRST, "▷기계항공우주공학부");
                    return "기계항공우주공학부";
            }

            return "";
        }
        public void SelectMenu(CourseVO courseVO)  //학과전공 선택(커서 좌우로 이동)
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();
            if (courseVO.Department == null || courseVO.Department.Equals("")) searchCategoryScreen.PrintDepartmentMenu();      //학과 선택 화면 출력

            Keyboard keyboard = new Keyboard((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.FIRST);
            int menu = keyboard.SelectLeft((int)Constants.RowMenu.FIRST, (int)Constants.RowMenu.FIFTH, (int)Constants.RowMenu.STEP);   //학과선택

            if (menu == (int)Constants.Keyboard.ESCAPE) return;  //학과선택 중 뒤로가기
            menu = keyboard.Left;

            courseVO.Department = ShowMenu(menu);   //학과이름 저장
        }
    }
}
