using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Grade
    {
        public string ShowMenu(int menu)
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();
            searchCategoryScreen.PrintGradeMenu();

            switch (menu)
            {
                case (int)Constants.Grade.ALL:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.LectureSchedule.GRADE, "▷전체");
                    break;
                case (int)Constants.Grade.FRESHMAN:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.LectureSchedule.GRADE, "▷1학년");
                    return "1";
                case (int)Constants.Grade.SOPHOMORE:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.LectureSchedule.GRADE, "▷2학년");
                    return "2";
                case (int)Constants.Grade.JUNIOR:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.LectureSchedule.GRADE, "▷3학년");
                    return "3";
                case (int)Constants.Grade.SENIOR:
                    searchCategoryScreen.ShowSelection(menu, (int)Constants.LectureSchedule.GRADE, "▷4학년");
                    return "4";
            }

            return "";
        }
        public void SelectMenu(ref string grade)  //학년 선택(커서 좌우로 이동)
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();
            if (grade == null) searchCategoryScreen.PrintGradeMenu();      //학년구분 화면 출력

            Keyboard keyboard = new Keyboard((int)Constants.Grade.ALL, (int)Constants.LectureSchedule.GRADE);
            int menu = keyboard.SelectLeft((int)Constants.Grade.ALL, (int)Constants.Grade.SENIOR, (int)Constants.Grade.STEP);   //학년 선택

            if (menu == (int)Constants.Keyboard.ESCAPE) return;  //학년구분선택 중 뒤로가기
            menu = keyboard.Left;

            grade = ShowMenu(menu);   //학년 숫자 저장
        }
    }
}
