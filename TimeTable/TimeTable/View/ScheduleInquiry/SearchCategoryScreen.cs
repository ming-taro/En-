using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class SearchCategoryScreen
    {
        public void ShowSelection(int left, int top, string menu)
        {
            Logo logo = new Logo();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            logo.PrintMenu(left, top, menu);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void PrintDepartmentMenu()
        {
            Logo logo = new Logo();
            logo.PrintMenu((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.FIRST, "▷전체");
            logo.PrintMenu((int)Constants.RowMenu.SECOND, (int)Constants.ColumnMenu.FIRST, "▷데이터사이언스학과");
            logo.PrintMenu((int)Constants.RowMenu.THIRD, (int)Constants.ColumnMenu.FIRST, "▷컴퓨터공학과");
            logo.PrintMenu((int)Constants.RowMenu.FOURTH, (int)Constants.ColumnMenu.FIRST, "▷미디어커뮤니케이션학과");
        }
        public void PrintCompletionTypeMenu()
        {
            Logo logo = new Logo();
            logo.PrintMenu((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.SECOND, "▷전체");
            logo.PrintMenu((int)Constants.RowMenu.SECOND, (int)Constants.ColumnMenu.SECOND, "▷공통교양필수");
            logo.PrintMenu((int)Constants.RowMenu.THIRD, (int)Constants.ColumnMenu.SECOND, "▷전공필수");
            logo.PrintMenu((int)Constants.RowMenu.FOURTH, (int)Constants.ColumnMenu.SECOND, "▷전공선택");
        }
        public void PrintGradeMenu()
        {
            Logo logo = new Logo();
            logo.PrintMenu((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.THIRD, "▷전체");
            logo.PrintMenu((int)Constants.RowMenu.SECOND, (int)Constants.ColumnMenu.THIRD, "▷1학년");
            logo.PrintMenu((int)Constants.RowMenu.THIRD, (int)Constants.ColumnMenu.THIRD, "▷2학년");
            logo.PrintMenu((int)Constants.RowMenu.FOURTH, (int)Constants.ColumnMenu.THIRD, "▷3학년");
            logo.PrintMenu((int)Constants.RowMenu.FIFTH, (int)Constants.ColumnMenu.THIRD, "▷4학년");
        }
        public void PrintCourseTitleMenu()
        {
            Logo logo = new Logo();
            logo.PrintMenu((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.FOURTH, "▷2자 이상 입력:");
        }
    }
}
