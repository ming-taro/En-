﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Constants
    {
        //EnteringText
        public const bool INPUT_VALUE = true;
        public const bool MODIFIERS = true;
        public const bool NOT_MODIFIERS = false;
        public const string ESC = "ESC";
        public const string ENTER = "ENTER";
        public const string INVALID_VALUE = "INVALID_VALUE";
        public const bool KOREAN = true;
        public const bool NOT_KOREAN = false;

        //ViewLectureSchedule
        public const bool LOGIN_IN = true;
        public const bool LOGIN_FAILURE = false;
        public const bool KEYBOARD_OPERATION = true;
        public const bool TOP = true;
        public const bool LEFT = false;
        public const bool IS_NOT_NULL_OR_EXCEPTION = false;
        public const bool IS_MEETING_CONDITION = true;
        public const bool IS_NOT_MEETING_CONDITION = false;

        public const int LEFT_VALUE_OF_COURSE_INPUT = 54;
        public const int GO_BACK_BUTTON_LEFT = 120;
        public const int CONSOLE_WIDTH = 145;
        public const int CONSOLE_HEIGHT = 25;
        public enum Keyboard{
            ENTERING_MENU,
            ESCAPE,
            MOVING_CURSOR,
            OUT_OF_MENU,
            WITHIN_THE_MENU
        }
        public enum MainMenu
        {
            STEP = 2,
            FIRST = 9,
            SECOND = 11,
            THIRD = 13,
            FOURTH = 15,
            LOGO_LEFT = 58,
            LOGO_TOP = 3,
            LEFT = 60
        }
        public enum ColumnMenu
        {
            STEP = 3,
            FIRST = 5,      
            SECOND = 8,
            THIRD = 11,
            FOURTH = 14,
            FIFTH = 17,
            SIXTH = 20,
            LOGO_LEFT = 61,
            LOGO_TOP = 3,
            LEFT = 15
        }
        public enum RowMenu
        {
            FIRST = 35,
            SECOND = 55,
            THIRD = 75,
            FOURTH = 95,
            FIFTH = 115,
            STEP = 20
        }
        public enum MenuTop
        {
            MAIN_MENU = 9
        }
    }
}
