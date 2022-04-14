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

        public enum Keyboard{
            ENTERING_MENU,
            ESCAPE,
            MOVING_CURSOR,
            OUT_OF_MENU,
            WITHIN_THE_MENU
        }
        public enum LectureSchedule
        {
            DEPARTMENT = 5,
            COMPLETION_TYPE = 8,
            GRADE = 11,
            COURSE_TITLE = 14,
            INSTRUCTOR = 17,
            SEARCH = 20
        }

        public enum Grade
        {
            ALL = 23,
            FRESHMAN = 41,   
            SOPHOMORE = 59,
            JUNIOR = 77,
            SENIOR = 95,
            STEP = 18
        }
        public enum Department
        {
            ALL = 23,
            COMPUTER_ENGINEERING = 41,
            DATA_SCIENCE = 64,
            HISTORY = 87,
            STEP = 18
        }
        public enum CompletionType
        {
            ALL = 23,
            COMON_EDUCATION_REQUIRED = 41,
            MAJOR_REQUIRED = 64,
            MAJOR_ELECTIVE = 87,
            STEP = 18
        }
    }
}
