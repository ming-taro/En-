﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class SelectingGrade
    {
        public string ShowMenu(int menu, int top)
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();
            searchCategoryScreen.PrintGradeMenu(top);

            switch (menu)
            {
                case (int)Constants.RowMenu.FIRST:
                    searchCategoryScreen.ShowSelection(menu, top, "▷전체");
                    break;
                case (int)Constants.RowMenu.SECOND:
                    searchCategoryScreen.ShowSelection(menu, top, "▷1학년");
                    return "1";
                case (int)Constants.RowMenu.THIRD:
                    searchCategoryScreen.ShowSelection(menu, top, "▷2학년");
                    return "2";
                case (int)Constants.RowMenu.FOURTH:
                    searchCategoryScreen.ShowSelection(menu, top, "▷3학년");
                    return "3";
                case (int)Constants.RowMenu.FIFTH:
                    searchCategoryScreen.ShowSelection(menu, top, "▷4학년");
                    return "4";
            }

            return "";
        }
        public void SelectMenu(CourseVO courseVO, int top)  //학년 선택(커서 좌우로 이동)
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();
            if (courseVO.Grade == null || courseVO.Grade.Equals("")) searchCategoryScreen.PrintGradeMenu(top);      //학년구분 화면 출력

            Keyboard keyboard = new Keyboard((int)Constants.RowMenu.FIRST, top);
            int menu = keyboard.SelectLeft((int)Constants.RowMenu.FIRST, (int)Constants.RowMenu.FIFTH, (int)Constants.RowMenu.STEP);   //학년 선택

            if (menu == (int)Constants.Keyboard.ESCAPE) return;  //학년구분선택 중 뒤로가기
            menu = keyboard.Left;

            courseVO.Grade = ShowMenu(menu, top);   //학년 숫자 저장
        }
    }
}
