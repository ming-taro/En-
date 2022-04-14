﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class ViewingLectureSchedule
    {

        private void SelectMenu(int menu)
        {
            int selection;

            switch (menu)
            {
                case (int)Constants.LectureSchedule.DEPARTMENT:       //학과전공
                    Department department = new Department();
                    selection = department.SelectMenu();
                    Console.Read();  //test용 지울것
                    break;
                case (int)Constants.LectureSchedule.COMPLETION_TYPE:  //이수구분

                    break;
                case (int)Constants.LectureSchedule.GRADE:            //학년

                    break;
                case (int)Constants.LectureSchedule.COURSE_TITLE:     //교과목명

                    break;
                case (int)Constants.LectureSchedule.INSTRUCTOR:       //교수명

                    break;
                case (int)Constants.LectureSchedule.SEARCH:           //조회

                    break;
            }


        }
        public void ViewLectureSchedule()
        {
            ViewingScheduleScreen viewingScheduleScreen = new ViewingScheduleScreen();
            viewingScheduleScreen.PrintMenu();

            Keyboard keyboard = new Keyboard(5, (int)Constants.LectureSchedule.DEPARTMENT);
            int menu = keyboard.SelectTop((int)Constants.LectureSchedule.DEPARTMENT, (int)Constants.LectureSchedule.SEARCH, 3);
            if (menu == (int)Constants.Keyboard.ESCAPE) return;  //메뉴선택 중 esc -> 뒤로가기(강의조회 및 수강신청 화면으로)
            menu = keyboard.Top;

            SelectMenu(menu);  //1.학과전공  2.이수구분  3.학년  4.교과목명  5.교수명  6.조회
        }

    }
}
