﻿using System;
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
            logo.PrintMenu((int)Constants.Department.ALL, (int)Constants.LectureSchedule.DEPARTMENT, "▷전체");
            logo.PrintMenu((int)Constants.Department.COMPUTER_ENGINEERING, (int)Constants.LectureSchedule.DEPARTMENT, "▷컴퓨터공학과");
            logo.PrintMenu((int)Constants.Department.DATA_SCIENCE, (int)Constants.LectureSchedule.DEPARTMENT, "▷데이터사이언스과");
            logo.PrintMenu((int)Constants.Department.HISTORY, (int)Constants.LectureSchedule.DEPARTMENT, "▷역사학과");
        }
    }
}