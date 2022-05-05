using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class BookRegistration
    {
        private BookDAO bookDAO = BookDAO.GetInstance();
        private LogDAO logDAO = LogDAO.GetInstance();
        private EnteringText text;
        private AdminView adminView;
        private Exception exception;

        public BookRegistration(EnteringText text, AdminView adminView, Exception exception)
        {
            this.text = text;
            this.adminView = adminView;
            this.exception = exception;
        }
        public string InputBookId(int left, int top)
        {
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                exception.RemoveLine(left, top);
                bookId = text.EnterText(left, top, "");        //도서번호를 입력 받음

                if (bookId.Equals(Constants.ESC))//뒤로가기
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH) //양식에 맞지 않은 입력
                {
                    exception.PrintBookIdRegex(0, top + 1);
                }
                else if (bookDAO.IsDuplicateBookId(bookId)) //입력 양식은 맞지만 도서아이디가 이미 존재하는 경우
                {
                    exception.PrintDuplicateBookId(0, top + 1);
                }
                else
                {
                    exception.RemoveLine(0, top + 1);
                    break;
                }
            }

            return bookId;
        }
        public void PrintErrorMessage(int left, int top)  //예외메세지
        {
            int exceptionTop = top + 1;

            switch (top)
            {
                case (int)Constants.Registration.SECOND:  //도서명
                    exception.PrintBookNameRegex(left, exceptionTop);
                    break;
                case (int)Constants.Registration.THIRD:   //출판사
                    exception.PrintPublisherRegex(left, exceptionTop);
                    break;
                case (int)Constants.Registration.FOURTH:  //저자
                    exception.PrintAuthorRegex(left, exceptionTop);
                    break;
                case (int)Constants.Registration.FIFTH:   //가격
                    exception.PrintPriceRegex(left, exceptionTop);
                    break;
                case (int)Constants.Registration.SIXTH:   //수량
                    exception.PrintQuantityRegex(left, exceptionTop);
                    break;
            }
        }
        public string InputBookName(int left, int top, string regexText)
        {
            string bookName;

            while (Constants.INPUT_VALUE)
            {
                exception.RemoveLine(left, top);
                bookName = text.EnterText(left, top, "");        //도서명을 입력 받음

                if (bookName.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(bookName, regexText) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않은 입력
                {
                    PrintErrorMessage(left, top);
                }
                else
                {
                    exception.RemoveLine(0, top + 1);
                    break;
                }
            }

            return bookName;
        }
        public void RegisterBook(Keyboard keyboard)
        {
            BookDTO book;
            //string id;
            string name;              //도서명
            string author;            //저자
            string publisher;         //출판사
            string publicationDate="";   //출판일
            string isbn="";              //ISBN
            string price;             //가격
            string quantity;          //수량
            string bookIntroduction="";  //책소개

            adminView.PrintBookRegistration();   //도서등록화면 출력
           
            //id = InputBookId(12, (int)Constants.Registration.FIRST);  
            //if (id.Equals(Constants.ESC)) return;             //도서번호 입력

            name = InputBookName(10, (int)Constants.Registration.SECOND, Constants.BOOK_NAME_REGEX);        
            if (name.Equals(Constants.ESC)) return;           //도서명 입력

            publisher = InputBookName(10, (int)Constants.Registration.THIRD, Constants.PUBLISHER_REGEX);      
            if (publisher.Equals(Constants.ESC)) return;      //출판사 입력

            author = InputBookName(8, (int)Constants.Registration.FOURTH, Constants.AUTHOR_REGEX);          
            if (author.Equals(Constants.ESC)) return;         //저자 입력

            price = InputBookName(8, (int)Constants.Registration.FIFTH, Constants.PRICE_REGEX);             
            if (price.Equals(Constants.ESC)) return;          //가격 입력

            quantity = InputBookName(8, (int)Constants.Registration.SIXTH, Constants.QUENTITY_REGEX);      
            if (quantity.Equals(Constants.ESC)) return;       //수량 입력

            book = new BookDTO("", name, author, publisher, publicationDate, isbn, price, bookIntroduction, quantity);
            bookDAO.AddToBookList(Constants.ADDITION_TO_BOOK_LIST, book);  //DB에 도서정보 저장
            logDAO.RegisterBook(name);                                     //로그에 도서등록 기록

            adminView.PrintRegisteredBook(book);              //등록 완료 화면 출력
            keyboard.PressESC();                              //Esc -> 종료(뒤로가기)
            Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
        }
    }
}
