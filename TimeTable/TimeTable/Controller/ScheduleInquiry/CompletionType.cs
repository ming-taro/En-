using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class CompletionType
    {
        public string ShowMenu(int menu)
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();
            searchCategoryScreen.PrintCompletionTypeMenu();

            switch (menu)
            {
                case (int)Constants.RowMenu.FIRST:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.ColumnMenu.SECOND, "▷전체");
                    break;
                case (int)Constants.RowMenu.SECOND:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.ColumnMenu.SECOND, "▷공통교양필수");
                    return "공필";
                case (int)Constants.RowMenu.THIRD:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.ColumnMenu.SECOND, "▷전공필수");
                    return "전필";
                case (int)Constants.RowMenu.FOURTH:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.ColumnMenu.SECOND, "▷전공선택");
                    return "전선";
            }

            return "";
        }
        public void SelectMenu(CourseVO courseVO)  //이수구분 선택(커서 좌우로 이동)
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();
            if (courseVO.CompletionType == null || courseVO.CompletionType.Equals("")) searchCategoryScreen.PrintCompletionTypeMenu();      //이수구분 화면 출력

            Keyboard keyboard = new Keyboard((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.SECOND);
            int menu = keyboard.SelectLeft((int)Constants.RowMenu.FIRST, (int)Constants.RowMenu.FOURTH, (int)Constants.RowMenu.STEP);   //학과선택

            if (menu == (int)Constants.Keyboard.ESCAPE) return;  //이수구분선택 중 뒤로가기
            menu = keyboard.Left;

            courseVO.CompletionType = ShowMenu(menu);   //이수구분 저장
        }
    }
}
