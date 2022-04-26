using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Constants
    {
        public const int CLOSE_PROGRAM = -3;
        public const int INVALID_INPUT = -2;
        public const int ESCAPE = -1;
        public const int MOVING_CURSOR = 0;
        public const int ENTERING_MENU = 1;
        public const bool KEYBOARD_OPERATION = true;

        public const int COMPLETE_FUNCTION = 7; //----->삭제할 코드

        public const bool OUT_OF_MENU = true;
        public const bool ADMIN_MODE = true;
        public const bool MEMBER_MODE = true;
        public const bool SIGN_IN = true;

        public const bool CORRECT_MEMBERSHIP = true;
        public const bool WRONG_MEMBERSHIP = false;

        public const bool DUPLICATE_ID = true;
        public const bool NON_DUPLICATE_ID = false;
        public const string REMOVE_LINE = "                                                             ";

        public const bool EXISTING_MEMBER = true;
        public const bool NON_EXISTING_MEMBER = false;

        public const bool IS_MATCH = true;
        public const bool IS_NOT_MATCH = false;

        public const bool BOOK_I_BORROWED = true;
        public const bool BOOK_I_NEVER_BORROWED = false;

        public const bool BOOK_IN_LIST = true;
        public const bool BOOK_NOT_IN_LIST = false;
        public const bool MEMBER_IN_LIST = true;
        public const bool QUANTITY_ZERO = true;
        public const string RE_ENTER = "RE_ENTER";

        public const bool GOING_NEXT = true;
        public const bool GOING_BACK = true;
        public const bool INPUT_VALUE = true;
        public const string SIGN_IN_ERROR = "아이디 또는 비밀번호를 잘못 입력하셨습니다.\n다시 입력해주세요.\n";

        //정규식
        public const string BOOK_NAME_REGEX = @"^[\w]{1,1}[^\e]{0,49}$";
        public const string BOOK_NAME_ERROR_MESSAGE = "(해당 검색어와 일치하는 도서가 없습니다. 다시 입력해주세요.)";


        //EnteringText
        public const bool MODIFIERS = true;
        public const bool NOT_MODIFIERS = false;
        public const bool KOREAN = true;
        public const bool NOT_KOREAN = false;
        public const string ESC = "ESC";
        public const string ENTER = "ENTER";

        //Connection
        public const string SERVER = "Server = localhost;";
        public const string PORT = "Port = 3306;";
        public const string DATABASE = "Database = booklist;";
        public const string ID = "Uid = root;";
        public const string PASSWORD = "Pwd = 0000;";

        public enum Menu
        {
            FIRST = 13,
            SECOND = 14,
            THIRD = 15,
            FOURTH = 16,
            FIFTH = 17,
            STEP = 1
        }
        public enum Registration
        {
            FIRST = 7,
            SECOND = 10,
            THIRD = 13,
            FOURTH = 16,
            FIFTH = 19,
            SIXTH = 22,
            STEP = 3
        }
        public enum ProfileMenu
        {
            FIRST = 16,
            SECOND = 19,
            THIRD = 22,
            FOURTH = 25,
            FIFTH = 28,
            SIXTH = 31,
            SEVENTH = 34,
            STEP = 3
        }
        public enum SearchMenu
        {
            BOOK_NAME = 1,
            PUBLISHER,
            AUTHOR
        }
    }
}
