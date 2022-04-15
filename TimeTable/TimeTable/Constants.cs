using System;
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

        public const int LEFT_VALUE_OF_COURSE_INPUT = 42;

        public enum Keyboard{
            ENTERING_MENU,
            ESCAPE,
            MOVING_CURSOR,
            OUT_OF_MENU,
            WITHIN_THE_MENU
        }

        public enum Grade
        {
            ALL = 43,              //left
            FRESHMAN = 63,   
            SOPHOMORE = 83,
            JUNIOR = 103,
            SENIOR = 123,
            STEP = 20
        }
        public enum Department
        {
            ALL = 43,
            COMPUTER_ENGINEERING = 63,
            DATA_SCIENCE = 83,
            HISTORY = 103,
            STEP = 20
        }
        public enum CompletionType
        {
            ALL = 43,
            COMON_EDUCATION_REQUIRED = 63,
            MAJOR_REQUIRED = 83,
            MAJOR_ELECTIVE = 103,
            STEP = 20
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
            FIRST = 5,         //top
            SECOND = 8,
            THIRD = 11,
            FOURTH = 14,
            FIFTH = 17,
            SIXTH = 20,
            LOGO_LEFT = 58,
            LOGO_TOP = 3,
            LEFT = 15
        }
        public enum MenuTop
        {
            MAIN_MENU = 9
        }
    }
}
