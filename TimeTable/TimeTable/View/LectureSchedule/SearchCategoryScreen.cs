using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class SearchCategoryScreen
    {
        public void PrintMenu(string menu)
        {
            Console.Clear();
            Logo logo = new Logo();
            logo.PrintTwoLine();
            logo.PrintMenu((int)Constants.ColumnMenu.LOGO_LEFT, (int)Constants.ColumnMenu.LOGO_TOP, "[강의 시간표 조회]");
            logo.PrintMenu((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.FIRST, "☞학과전공");
            logo.PrintMenu((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.SECOND, menu);
            logo.PrintMenu((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.THIRD, "☞학년");
            logo.PrintMenu((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.FOURTH, "☞교과목명");
            logo.PrintMenu((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.FIFTH, "☞교수명");
            logo.PrintMenu((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.SIXTH, "☞조회");
        }

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
            logo.PrintMenu((int)Constants.RowMenu.SECOND, (int)Constants.ColumnMenu.FIRST, "▷컴퓨터공학과");
            logo.PrintMenu((int)Constants.RowMenu.THIRD, (int)Constants.ColumnMenu.FIRST, "▷소프트웨어학과");
            logo.PrintMenu((int)Constants.RowMenu.FOURTH, (int)Constants.ColumnMenu.FIRST, "▷지능기전공학부");
            logo.PrintMenu((int)Constants.RowMenu.FIFTH, (int)Constants.ColumnMenu.FIRST, "▷기계항공우주공학부");
        }
        public void PrintCompletionTypeMenu()
        {
            Logo logo = new Logo();
            logo.PrintMenu((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.SECOND, "▷전체");
            logo.PrintMenu((int)Constants.RowMenu.SECOND, (int)Constants.ColumnMenu.SECOND, "▷공통교양필수");
            logo.PrintMenu((int)Constants.RowMenu.THIRD, (int)Constants.ColumnMenu.SECOND, "▷전공필수");
            logo.PrintMenu((int)Constants.RowMenu.FOURTH, (int)Constants.ColumnMenu.SECOND, "▷전공선택");
        }
        public void PrintGradeMenu(int top)
        {
            Logo logo = new Logo();
            logo.PrintMenu((int)Constants.RowMenu.FIRST, top, "▷전체");
            logo.PrintMenu((int)Constants.RowMenu.SECOND, top, "▷1학년");
            logo.PrintMenu((int)Constants.RowMenu.THIRD, top, "▷2학년");
            logo.PrintMenu((int)Constants.RowMenu.FOURTH, top, "▷3학년");
            logo.PrintMenu((int)Constants.RowMenu.FIFTH, top, "▷4학년");
        }
        public void PrintCourseTitleMenu()
        {
            Logo logo = new Logo();
            logo.PrintMenu((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.FOURTH, "▷2자 이상 입력:");
        }
    }
}
