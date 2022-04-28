using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Constants
    {
        public enum Keyboard
        {
            ESCAPE = -5,
            MOVING_CURSOR,
            ENTER
        }
        public const int CLOSE_PROGRAM = -3;    //----->삭제할 코드
        public const int INVALID_INPUT = -2;    //----->삭제할 코드
        public const int ESCAPE = -1;           //----->삭제할 코드
        public const int MOVING_CURSOR = 0;     //----->삭제할 코드
        public const int ENTERING_MENU = 1;     //----->삭제할 코드
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

        public const bool IS_BOOK_IN_LIST = true;
        public const bool IS_BOOK_NOT_IN_LIST = false;
        public const bool MEMBER_IN_LIST = true;
        public const bool IS_QUANTITY_ZERO = true;
        public const bool IS_QUANTITY_MORE_THAN_ONE = false;
        public const string RE_ENTER = "RE_ENTER";

        public const bool GOING_NEXT = true;
        public const bool GOING_BACK = true;
        public const bool INPUT_VALUE = true;
        public const string SIGN_IN_ERROR = "아이디 또는 비밀번호를 잘못 입력하셨습니다.\n다시 입력해주세요.\n";
        public const string SEARCH_TYPE = "☞도서명:\n☞출판사:\n☞저자:";
        public const string BOOK_ID_TO_BORROW= "☞대여할 도서 번호:";
        public const string BOOK_ID_TO_DELETE = "☞반납할 도서 번호:";
        public const string NO_SEARCH_RESULT = "[입력하신 검색어를 포함하는 도서가 없습니다.]";
        public const string ESC_AND_ENTER = "                         [ESC]:뒤로가기    [ENTER]:다시 검색";
        public const string ESC_MESSAGE = "                                              [ESC]:뒤로가기";
        //EnteringText
        public const bool MODIFIERS = true;
        public const bool NOT_MODIFIERS = false;
        public const bool KOREAN = true;
        public const bool NOT_KOREAN = false;
        public const string ESC = "ESC";
        public const string ENTER = "ENTER";

        //정규식
        public const string BOOK_NAME_REGEX = @"^[\w]{1,1}[^\e]{0,49}$";
        public const string BOOK_ID_REGEX = @"^[0-9]{1,3}$";

        //입력 오류 메세지
        public const string MESSAGE_ABOUT_BOOK_NAME = "(1~50자 이내의 문자를 입력해주세요.)                         ";
        public const string MESSAGE_ABOUT_BOOK_ID_NOT_MATCH = "(0~999사이의 숫자가 아닙니다.다시 입력해주세요.)               ";
        public const string MESSAGE_ABOUT_BOOK_NOT_IN_LIST = "(현재 조회 목록에 없는 도서번호입니다. 다시 입력해주세요.)           ";
        public const string MESSAGE_ABOUT_BOOK_I_BORROWED = "(이미 대여중인 도서입니다. 다른 도서를 선택해주세요.)                  ";
        public const string MESSAGE_ABOUT_QUANTITY_ZERO  = "(대여가능한 도서가 0권입니다. 다른 도서를 선택해주세요.)              ";
        public const string MESSAGE_ABOUT_BOOK_I_NEVER_BORROWED = "(대여목록에 없는 도서번호입니다. 다시 입력해주세요.)             ";
        //쿼리
        public const string BOOK_LIST = "select*from book;";
        public const string BOOK_NAME_SEARCH = "select*from book where name like '%";
        public const string PUBLISHER_SEARCH = "select*from book where publisher like '%";
        public const string AUTHOR_SEARCH = "select*from book where author like '%";
        public const string END_OF_SEARCH_QUERY = "%';";
        public const string RENTAL_LIST = "select * from book inner join borrowbook on book.id = borrowbook.bookId and borrowbook.memberId = '";
        public const string END_OF_STRING_QUERY = "';";
        public const string ADDITION_TO_RENTAL_LIST = "INSERT INTO borrowBook(memberId,bookId) VALUES ('";
        public const string DELETION_FROM_RENTAL_LIST = "DELETE FROM borrowBook WHERE memberId='";
        public const string END_OF_VALUE_QUERY = "');";
        public const string DECREASE_IN_BOOK_QUANTITY = "UPDATE book SET quantity = quantity - 1 WHERE id='";
        public const string INCREASE_IN_BOOK_QUANTITY = "UPDATE book SET quantity = quantity + 1 WHERE id='";
        public const string ADMIN_ACCOUNT = "select*from admin;";
        public const string MEMBER_ID = "select*from member where id='";
        public const string MEMBER_PASSWORD = "' and password='";

        //Connection
        public const string SERVER = "Server = localhost;";
        public const string PORT = "Port = 3306;";
        public const string DATABASE = "Database = booklist;";
        public const string ID = "Uid = root;";
        public const string PASSWORD = "Pwd = 0000;";

        public enum SignIn
        {
            INPUT_ID = 8,         //left
            INPUT_PASSWORD = 10,  //left
            ID = 7,               //top
            PASSWORD = 9          //top
        }
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
            ALL = 0,
            FIRST,
            SECOND,
            THIRD,
            FOURTH,
            NO_SEARCH_RESULT_LEFT = 9,
            LEFT_VALUE_OF_INPUT = 10,     
            STEP = 1
        }
    }
}
