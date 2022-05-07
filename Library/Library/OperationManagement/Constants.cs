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

        public const bool IS_VISIBLE_CURSOR = true;
        public const bool IS_INVISIBLE_CURSOR = false;

        public const bool IS_EXISTING_FILE = true;

        public const bool IS_REGISTERABLE = true;
        public const bool IS_NOT_REGISTERABLE = false;

        public const bool HAS_ROW = true;
        public const bool NOT_HAS_ROW = false;

        //EnteringText
        public const bool IS_MODIFIERS = true;
        public const bool IS_NOT_MODIFIERS = false;
        public const bool IS_KOREAN = true;
        public const bool IS_NOT_KOREAN = false;
        public const string ESC = "ESC";
        public const string ENTER = "ENTER";

        //정규식
        public const string BOOK_ID_REGEX = @"^[0-9]{1,3}$";
        public const string BOOK_NAME_REGEX = @"[^\s]{1,1}.{0,49}$";
        public const string PUBLISHER_REGEX = @"[^\s]{1,1}.{0,49}$";
        public const string PUBLICATION_DATE_REGEX = @"^(1|2)[0-9]{3}\.(0[1-9]|1[0-2])\.(0[1-9]|(1|2)[0-9]|3[0-1])$";
        public const string AUTHOR_REGEX = @"^[a-zA-Z가-힣]{1,50}$";
        public const string PRICE_REGEX = @"^[1-9]{1}[0-9]{0,9}$";
        public const string BOOK_INTRODUCTION_REGEX = @"^.{0,100}$";
        public const string ISBN_REGEX = @"^[0-9]{1,1}[0-9\s]{0,49}$";
        public const string QUANTITY_REGEX = @"^[1-9]{1}[0-9]{0,1}$";
        public const string DISPLAY_REGEX = @"(^[1-9]{1}[0-9]{0,1})$|(100)$";
        
        public const string MEMBER_ID_REGEX = @"^[a-zA-Z0-9]{5,10}$";
        public const string NAME_REGEX = @"^[a-zA-Z가-힣]{1,30}$";
        public const string AGE_REGEX = @"^[1-9]{1}[0-9]{0,1}$";
        public const string PHONE_NUMBER_REGEX = @"010-[0-9]{4}-[0-9]{4}$";
        public const string ADDRESS_REGEX = @"([가-힣]+(시|도)\s[가-힣]+(시|군|구)\s[가-힣]+(읍|면)\s[가-힣0-9]+(로|길)\s[0-9])|([가-힣]+(시|도)\s[가-힣]+(시|군|구)\s[가-힣0-9]+(로|길)\s[0-9])";
        
        //입력 오류 메세지
        public const string MESSAGE_ABOUT_BOOK_ID_NOT_MATCH = "(0~999사이의 숫자가 아닙니다.다시 입력해주세요.)               ";
        public const string MESSAGE_ABOUT_BOOK_NOT_IN_LIST = "(현재 조회 목록에 없는 도서입니다. 다시 입력해주세요.)           ";
        public const string MESSAGE_ABOUT_BOOK_I_BORROWED = "(이미 대여중인 도서입니다. 다른 도서를 선택해주세요.)                  ";
        public const string MESSAGE_ABOUT_QUANTITY_ZERO = "(대여가능한 도서가 0권입니다. 다른 도서를 선택해주세요.)              ";
        public const string MESSAGE_ABOUT_BOOK_I_NEVER_BORROWED = "(대여목록에 없는 도서번호입니다. 다시 입력해주세요.)             ";
        public const string MESSAGE_ABOUT_DUPLICATE_BOOK_ID = "(이미 사용중인 도서번호입니다. 다시 입력해주세요.)    ";

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
        public const string MESSAGE_ABOUT_BOOK_ON_LOAN = "(회원이 대여중인 도서는 삭제가 불가능합니다.)                 ";

        //쿼리
        public const string BOOK_LIST = "select*from book;";
        public const string BOOK_NAME_SEARCH = "select*from book where name like '%{0}%';";
        public const string PUBLISHER_SEARCH = "select*from book where publisher like '%{0}%';";
        public const string AUTHOR_SEARCH = "select*from book where author like '%{0}%';";

        public const string RENTAL_LIST = "select * from book join bookRentalList on book.isbn = bookRentalList.isbn and bookRentalList.memberId = @memberId;";
        public const string RENTAL_LIST_INQUIRY = "select * from book join bookRentalList on book.isbn = bookRentalList.isbn;";
        public const string ADDITION_TO_RENTAL_LIST = "INSERT INTO bookRentalList(memberId, isbn, rentalPeriod) VALUES (@memberId, @isbn, @rentalPeriod);";
        public const string DELETION_FROM_RENTAL_LIST = "DELETE FROM bookRentalList WHERE memberId = @memberId and rentalPeriod = @rentalPeriod;";
        public const string DECREASE_IN_BOOK_QUANTITY = "UPDATE book SET quantity = quantity - 1 WHERE isbn=@isbn;";
        public const string INCREASE_IN_BOOK_QUANTITY = "UPDATE book SET quantity = quantity + 1 WHERE isbn=@isbn;";

        public const string ADMIN_ACCOUNT = "select*from admin;";
        public const string MEMBER_ACCOUNT = "select*from member where id = @memberId;";
        public const string MEMBER_CONFIRMATION = "select id from member where id=@memberId and password=@password;";

        public const string ADDITION_TO_MEMBER_LIST = "INSERT INTO member VALUES(@id, @password, @name, @age, @phoneNumber, @address)";
        public const string DUPLICATE_MEMBER_ID = "select id from member where id=@memberId;";

        public const string UPDATE_TO_MEMBER_LIST = "UPDATE member SET password=@password, name=@name, age=@age, phoneNumber=@phoneNumber, address=@address WHERE id=@id";
        public const string UPDATE_ON_MEMBER_ID = "UPDATE member SET id=@id WHERE id=@memberId;";
        public const string MEMBER_LIST = "select*from member;";
        public const string MEMBER_WHO_BORROWED_BOOK = "select memberId from bookRentalList where memberId = @memberId;";
        public const string DELETION_FROM_MEMBER_LIST = "DELETE FROM member WHERE id = @memberId;";

        public const string BOOK_ON_LOAN = "select isbn from bookRentalList where isbn = @isbn;";
        public const string DUPLICATE_BOOK_ID = "select id from book where id = @bookId;";
        public const string ADDITION_TO_BOOK_LIST = "INSERT INTO book VALUES (@name, @author, @publisher, @publicationDate, @isbn, @price, @quantity, @bookIntroduction);";
        public const string DELETION_FROM_BOOK_LIST = "DELETE FROM book WHERE isbn = @isbn;";

        public const string UPDATE_TO_BOOK_LIST = "update book set name=@name,author=@author, publisher=@publisher, publicationDate=@publicationDate, isbn=@isbn, price=@price ,quantity=@quantity, bookIntroduction=@bookIntroduction where isbn='{0}';";
        
        public const string UPDATE_TO_LOG = "INSERT INTO log(user,menu,content, date) VALUES (@user, @menu, @content, @date);";
        public const string LOG_LIST = "select*from log;";
        public const string LOG_INITIALIZATION = "TRUNCATE TABLE log;";
        public const string DELETION_FROM_LOG_LIST = "DELETE FROM log WHERE date = @date;";

        //Connection
        public const string SERVER = "Server = localhost;";
        public const string PORT = "Port = 3306;";
        public const string DATABASE = "Database = ParkMinJi_Library;";
        public const string ID = "Uid = root;";
        public const string PASSWORD = "Pwd = 0000;";

        //Naver
        public const string API_URL = "https://openapi.naver.com/v1/search/book.json?query={0}&display={1}";
        public const string NAVER_CLIENT_ID = "X-Naver-Client-Id";
        public const string NAVER_CLIENT_SECRET = "X-Naver-Client-Secret";
        public const string CLIENT_ID = "pKd8QC0tp8T66Lu1Irnj";
        public const string CLIENT_SECRET = "ey4QHrrt1y";

        //로그
        public const string FILE_NAME = "/로그 기록.txt";

        public enum Keyboard
        {
            ESCAPE = -3,
            MOVING_CURSOR,
            ENTER
        }
        public enum SignIn
        {
            LEFT = 40,
            INPUT = 50,           //left
            ID = 17,              //top
            PASSWORD = 20,        //top
            MESSAGE = 24,
            MESSAGE_LEFT = 28
        }
        public enum Menu
        {
            FIRST = 20,          //top
            SECOND = 22,
            THIRD = 24,
            FOURTH = 26,
            FIFTH = 28,
            SIXTH = 30,
            SEVENTH = 32,
            EIGHT = 34,
            LEFT = 45,
            CONSOLE_HEIGHT = 40,
            CONSOLE_WIDTH = 100,
            LOG_MESSAGE_LEFT = 29,
            STEP = 2
        }
        public enum Registration
        {
            LEFT = 17,
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
            LEFT = 0,
            MESSAGE = 8,
            ZERO = 10,
            FIRST = 12,        //top
            SECOND = 15,
            THIRD = 18,
            FOURTH = 21,
            FIFTH = 24,
            SIXTH = 27,
            SEVENTH = 30,
            EIGHT = 33,
            NINTH = 36,
            STEP = 3
        }
        public enum SearchMenu
        {
            ESC_TOP = 7,
            ESC_LEFT = 65,
            LEFT = 20,
            ALL = 0,            //top
            ZERO = 11,
            FIRST = 13,
            SECOND = 15,
            THIRD = 17,
            FOURTH = 19,
            FIFTH = 21,
            SIXTH = 23,
            NO_SEARCH_RESULT_LEFT = 29,
            STEP = 2
        }
        public enum InputField  //left값
        {
            MEMBER_DELETION = 42,//회원삭제에서의 입력칸 left값
            ID_TO_MODIFY = 46,   //도서정보수정에서의 수정할 도서번호 입력칸 left값
            BOOK_EDITION = 12,   //도서정보수정에서의 입력칸 left값
            SEARCH = 30,         //도서검색에서의 입력칸 left값
            NAVER_SEARCH = 34,   //네이버검색에서의 입력칸 left값
            LEFT = 40,           //도서반납,대출,삭제에서의 입력칸 left값
            REGISTRATION = 12,   //도서등록에서의 입력칸 left값
        }
        public enum Exception//top값
        {
            SEARCH = 20,     //도서검색에서의 예외메세지 top값
            TOP = 16         //도서반납,대출,삭제에서의 예외메세지 top값
        }
    }
}
