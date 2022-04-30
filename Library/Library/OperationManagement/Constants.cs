using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Constants
    {
        public const bool INPUT_VALUE = true;
        public const bool KEYBOARD_OPERATION = true;
        public const bool OUT_OF_MENU = true;
        public const bool MOVEMENT_WITHIN_MENU = false;
        public const bool ADMIN_MODE = true;
        public const bool MEMBER_MODE = true;

        public const bool IS_DUPLICATE_ID = true;
        public const bool IS_NON_DUPLICATE_ID = false;
        
        public const bool IS_EXISTING_MEMBER = true;
        public const bool IS_NON_EXISTING_MEMBER = false;

        public const bool IS_MATCH = true;
        public const bool IS_NOT_MATCH = false;

        public const bool IS_BOOK_I_BORROWED = true;
        public const bool IS_BOOK_I_NEVER_BORROWED = false;

        public const bool IS_BOOK_IN_LIST = true;
        public const bool IS_BOOK_NOT_IN_LIST = false;

        public const bool IS_BOOK_ON_LOAN = true;
        public const bool IS_BOOK_NOT_ON_LOAN = false;

        public const bool IS_QUANTITY_ZERO = true;
        public const bool IS_QUANTITY_MORE_THAN_ONE = false;

        public const bool IS_MEMBER_IN_LIST = true;
        public const bool IS_MEMBER_NOT_IN_LIST = false;

        public const bool IS_MEMBER_BORROWING_BOOK = true;
        public const bool IS_MEMBER_NOT_BORROWING_BOOK = false;

        public const string ESC_AND_ENTER = "                          [ESC]:뒤로가기    [ENTER]:다시 검색";
        public const string ESC_MESSAGE = "                                              [ESC]:뒤로가기";
        public const string REMOVE_LINE = "                                                             ";

        //EnteringText
        public const bool IS_MODIFIERS = true;
        public const bool IS_NOT_MODIFIERS = false;
        public const bool IS_KOREAN = true;
        public const bool IS_NOT_KOREAN = false;
        public const string ESC = "ESC";
        public const string ENTER = "ENTER";

        //정규식
        public const string BOOK_ID_REGEX = @"^[0-9]{1,3}$";
        public const string BOOK_NAME_REGEX = @"^[\w]{1,1}[^\e]{0,49}$";
        public const string PUBLISHER_REGEX = @"^[\w]{1,1}[^\e]{0,49}$";
        public const string AUTHOR_REGEX = @"^[a-zA-Z가-힣]{1,50}$";
        public const string PRICE_REGEX = @"^[1-9]{1}[0-9]{0,9}$";
        public const string QUENTITY_REGEX = @"^[1-9]{1}[0-9]{0,1}$";

        public const string MEMBER_ID_REGEX = @"^[a-zA-Z0-9]{5,10}$";
        public const string NAME_REGEX = @"^[a-zA-Z가-힣]{1,30}$";
        public const string AGE_REGEX = @"^[1-9]{1}[0-9]{0,1}$";
        public const string PHONE_NUMBER_REGEX = @"010-[0-9]{4}-[0-9]{4}$";
        public const string ADDRESS_REGEX = @"[가-힣]+(시|도)\s[가-힣]+(시|군|구)\s[가-힣]+(읍|면|동)";

        //입력 오류 메세지
        public const string MESSAGE_ABOUT_BOOK_ID_NOT_MATCH = "(0~999사이의 숫자가 아닙니다.다시 입력해주세요.)               ";
        public const string MESSAGE_ABOUT_BOOK_NOT_IN_LIST = "(현재 조회 목록에 없는 도서입니다. 다시 입력해주세요.)           ";
        public const string MESSAGE_ABOUT_BOOK_I_BORROWED = "(이미 대여중인 도서입니다. 다른 도서를 선택해주세요.)                  ";
        public const string MESSAGE_ABOUT_QUANTITY_ZERO  = "(대여가능한 도서가 0권입니다. 다른 도서를 선택해주세요.)              ";
        public const string MESSAGE_ABOUT_BOOK_I_NEVER_BORROWED = "(대여목록에 없는 도서번호입니다. 다시 입력해주세요.)             ";
        public const string MESSAGE_ABOUT_DUPLICATE_BOOK_ID = "(이미 사용중인 도서번호입니다. 다시 입력해주세요.)    ";

        public const string MESSAGE_ABOUT_BOOK_NAME = "(1~50자 이내의 문자를 입력해주세요.)                         ";
        public const string MESSAGE_ABOUT_PUBLISHER = "(1~50자 이내의 문자를 입력해주세요.)                         ";
        public const string MESSAGE_ABOUT_AUTHOR = "(50자 이내의 영어, 한글만 입력해주세요.)                            ";
        public const string MESSAGE_ABOUT_PRICE = "(0이상의 숫자를 10자 이내로 다시 입력해주세요.)                                 ";
        public const string MESSAGE_ABOUT_QUENTITY = "(1~99사이의 숫자만 가능합니다. 다시 입력해주세요.)                            ";

        public const string MESSAGE_ABOUT_MEMBER_ID = "(5~10자의 영어, 숫자만 다시 입력해주세요.)         ";
        public const string MESSAGE_ABOUT_DUPLICATE_ID = "(이미 사용중인 아이디입니다. 다시 입력해주세요.)    ";
        public const string MESSAGE_ABOUT_AVAILABLE_ID = "(사용 가능한 아이디입니다.)                          ";
        public const string MESSAGE_ABOUT_PASSWORD = "(비밀번호가 맞지 않습니다. 다시 입력해주세요.)         ";
        public const string MESSAGE_ABOUT_MEMBER_NAME = "(30자 이내의 영어, 한글만 다시 입력해주세요.)             ";
        public const string MESSAGE_ABOUT_AGE = "(1~99세까지 입력 가능합니다.)                ";
        public const string MESSAGE_ABOUT_PHONE_NUMBER = "(양식에 맞춰 다시 입력해주세요.(ex: 010-0000-0000))              ";
        public const string MESSAGE_ABOUT_ADDRESS = "(양식에 맞춰 입력해주세요.(ex: 서울특별시 광진구 군자동))       ";

        public const string MESSAGE_ABOUT_MEMBER_NOT_IN_LIST = "(존재하지 않는 회원입니다.)                     ";
        public const string MESSAGE_ABOUT_MEMBER_BORROWING_BOOK = "(도서를 대여중인 회원은 삭제가 불가능합니다.)   ";
        public const string MESSAGE_ABOUT_BOOK_ON_LOAN  = "(회원이 대여중인 도서는 삭제가 불가능합니다.)                 ";
        
        //쿼리
        public const string BOOK_LIST = "select*from book;";
        public const string BOOK_NAME_SEARCH = "select*from book where name like '%{0}%';";
        public const string PUBLISHER_SEARCH = "select*from book where publisher like '%{0}%';";
        public const string AUTHOR_SEARCH = "select*from book where author like '%{0}%';";

        public const string RENTAL_LIST = "select * from book join borrowbook on book.id = borrowbook.bookId and borrowbook.memberId = @memberId;";
        public const string ADDITION_TO_RENTAL_LIST = "INSERT INTO borrowBook(memberId,bookId,rentalPeriod) VALUES (@memberId, @bookId, @rentalPeriod);";
        public const string DELETION_FROM_RENTAL_LIST = "DELETE FROM borrowBook WHERE memberId = @memberId and bookId = @bookId;";
        public const string DECREASE_IN_BOOK_QUANTITY = "UPDATE book SET quantity = quantity - 1 WHERE id=@bookId;";
        public const string INCREASE_IN_BOOK_QUANTITY = "UPDATE book SET quantity = quantity + 1 WHERE id=@bookId;";

        public const string ADMIN_ACCOUNT = "select*from admin;";
        public const string MEMBER_ACCOUNT = "select*from member where id = @memberId;";
        public const string MEMBER_CONFIRMATION = "select id from member where id=@memberId and password=@password;";

        public const string ADDITION_TO_MEMBER_LIST = "INSERT INTO member VALUES(@id, @password, @name, @age, @phoneNumber, @address)";
        public const string DUPLICATE_MEMBER_ID = "select id from member where id=@memberId;";

        public const string UPDATE_TO_MEMBER_LIST = "UPDATE member SET password=@password, name=@name, age=@age, phoneNumber=@phoneNumber, address=@address WHERE id=@id";
        public const string UPDATE_ON_MEMBER_ID = "UPDATE member SET id=@id WHERE id=@memberId;";
        public const string MEMBER_LIST = "select*from member;";
        public const string MEMBER_BORROWING_BOOK = "select memberId from borrowbook where memberId = @memberId;";
        public const string DELETION_FROM_MEMBER_LIST = "DELETE FROM member WHERE id = @memberId;";

        public const string BOOK_ON_LOAN = "select bookId from borrowBook where bookId = @bookId;";
        public const string DUPLICATE_BOOK_ID = "select id from book where id = @bookId;";
        public const string ADDITION_TO_BOOK_LIST = "INSERT INTO book VALUES (@id, @name, @publisher, @author, @price, @quantity);";
        public const string DELETION_FROM_BOOK_LIST = "DELETE FROM book WHERE id = @bookId;";

        public const string UPDATE_TO_BOOK_LIST = "update book set name=@name,publisher=@publisher,author=@author,price=@price,quantity=@quantity where id=@id;";
        public const string UPDATE_ON_BOOK_ID = "update book set id=@id where id=@bookId;";

        //Connection
        public const string SERVER = "Server = localhost;";
        public const string PORT = "Port = 3306;";
        public const string DATABASE = "Database = ParkMinJi_Library;";
        public const string ID = "Uid = root;";
        public const string PASSWORD = "Pwd = 0000;";

        public enum Keyboard
        {
            ESCAPE = -3,
            MOVING_CURSOR,
            ENTER
        }
        public enum SignIn
        {
            INPUT = 10,           //left
            ID = 7,               //top
            PASSWORD = 9          //top
        }
        public enum Menu
        {
            FIRST = 15,          //top
            SECOND = 17,
            THIRD = 19,
            FOURTH = 21,
            FIFTH = 23,
            LEFT = 25,
            CONSOLE_HEIGHT = 40,
            CONSOLE_WIDTH = 61,
            STEP = 2
        }
        public enum Registration
        {
            FIRST = 7,          //top
            SECOND = 10,
            THIRD = 13,
            FOURTH = 16,
            FIFTH = 19,
            SIXTH = 22,
            SEVENTH = 25,
            STEP = 3
        }
        public enum EditMenu
        {
            FIRST = 12,        //top
            SECOND = 15, 
            THIRD = 18,
            FOURTH = 21,
            FIFTH = 24,
            SIXTH = 27,
            SEVENTH = 30,
            STEP = 3
        }
        public enum SearchMenu
        {
            ALL = 0,            //top
            FIRST = 6,
            SECOND,
            THIRD,
            FOURTH,
            NO_SEARCH_RESULT_LEFT = 9,
            LEFT_VALUE_OF_INPUT = 10,     
            STEP = 1
        }
    }
}
