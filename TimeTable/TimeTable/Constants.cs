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
        public enum LectureSchedule
        {
            DEPARTMENT = 5,         //top
            COMPLETION_TYPE = 8,
            GRADE = 11,
            COURSE_TITLE = 14,
            INSTRUCTOR = 17,
            SEARCH = 20
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
    }
}
