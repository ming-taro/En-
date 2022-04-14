﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class ViewingScheduleScreen
    {
        public void PrintMenu()
        {
            Console.Clear();
            Logo logo = new Logo();
            logo.PrintTwoLine();
            logo.PrintMenu(50, 3, "[강의 시간표 조회]");
            logo.PrintMenu(5, (int)Constants.LectureSchedule.DEPARTMENT, "☞학과전공");
            logo.PrintMenu(5, (int)Constants.LectureSchedule.COMPLETION_TYPE, "☞이수구분");
            logo.PrintMenu(5, (int)Constants.LectureSchedule.GRADE, "☞학년");
            logo.PrintMenu(5, (int)Constants.LectureSchedule.COURSE_TITLE, "☞교과목명");
            logo.PrintMenu(5, (int)Constants.LectureSchedule.INSTRUCTOR, "☞교수명");
            logo.PrintMenu(5, (int)Constants.LectureSchedule.SEARCH, "☞조회");
        }
    }
}