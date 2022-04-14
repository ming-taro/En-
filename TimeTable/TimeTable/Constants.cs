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
    }
}
