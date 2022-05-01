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
        private BookDAO bookDatabaseManager;
        private EnteringText text;
        private Logo logo;
        private AdminView adminView;

        public BookRegistration(BookDAO bookDatabaseManager, EnteringText text, Logo logo, AdminView adminView)
        {
            this.bookDatabaseManager = bookDatabaseManager;
            this.text = text;
            this.logo = logo;
            this.adminView = adminView;
        }
        public string InputBookId(int left, int top)
        {
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                logo.RemoveLine(left, top);
                bookId = text.EnterText(left, top, "");        //도서번호를 입력 받음

                if (bookId.Equals(Constants.ESC))//뒤로가기
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH) //양식에 맞지 않은 입력
                {
                    logo.PrintMessage(0, top + 1, Constants.MESSAGE_ABOUT_BOOK_ID_NOT_MATCH, ConsoleColor.Red);
                }
                else if (bookDatabaseManager.IsDuplicateBookId(bookId)) //입력 양식은 맞지만 도서아이디가 이미 존재하는 경우
                {
                    logo.PrintMessage(0, top + 1, Constants.MESSAGE_ABOUT_DUPLICATE_BOOK_ID, ConsoleColor.Red);
                }
                else
                {
                    logo.RemoveLine(0, top + 1);
                    break;
                }
            }

            return bookId;
        }
        public string InputBookName(int left, int top, string regexText, string errorMessage)
        {
            string bookName;

            while (Constants.INPUT_VALUE)
            {
                logo.RemoveLine(left, top);
                bookName = text.EnterText(left, top, "");        //도서명을 입력 받음

                if (bookName.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(bookName, regexText) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않은 입력
                {
                    logo.PrintMessage(0, top + 1, errorMessage, ConsoleColor.Red);
                }
                else
                {
                    logo.RemoveLine(0, top + 1);
                    break;
                }
            }

            return bookName;
        }
        public void StartRegistration(Keyboard keyboard)
        {
            BookVO book;
            string id;
            string name;
            string publisher;
            string author;
            string price;
            string quantity;

            adminView.PrintBookRegistration();   //도서등록화면 출력
           
            id = InputBookId(12, (int)Constants.Registration.FIRST);  
            if (id.Equals(Constants.ESC)) return;             //도서번호 입력

            name = InputBookName(10, (int)Constants.Registration.SECOND, Constants.BOOK_NAME_REGEX, Constants.MESSAGE_ABOUT_BOOK_NAME);        
            if (name.Equals(Constants.ESC)) return;           //도서명 입력

            publisher = InputBookName(10, (int)Constants.Registration.THIRD, Constants.PUBLISHER_REGEX, Constants.MESSAGE_ABOUT_PUBLISHER);      
            if (publisher.Equals(Constants.ESC)) return;      //출판사 입력

            author = InputBookName(8, (int)Constants.Registration.FOURTH, Constants.AUTHOR_REGEX, Constants.MESSAGE_ABOUT_AUTHOR);          
            if (author.Equals(Constants.ESC)) return;         //저자 입력

            price = InputBookName(8, (int)Constants.Registration.FIFTH, Constants.PRICE_REGEX, Constants.MESSAGE_ABOUT_PRICE);             
            if (price.Equals(Constants.ESC)) return;          //가격 입력

            quantity = InputBookName(8, (int)Constants.Registration.SIXTH, Constants.QUENTITY_REGEX, Constants.MESSAGE_ABOUT_QUENTITY);      
            if (quantity.Equals(Constants.ESC)) return;       //수량 입력

            book = new BookVO(id, name, publisher, author, price, quantity);
            bookDatabaseManager.AddToBookList(Constants.ADDITION_TO_BOOK_LIST, book);  //DB에 도서정보 저장

            adminView.PrintRegisteredBook(book);              //등록 완료 화면 출력
            keyboard.PressESC();                              //Esc -> 종료(뒤로가기)
        }
    }
}
