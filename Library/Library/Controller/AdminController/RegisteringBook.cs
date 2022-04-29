using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class RegisteringBook
    {
        private BookDAO bookDatabaseManager;
        private EnteringText text;
        private AdminView adminView;
        private Logo logo;

        public RegisteringBook(BookDAO bookDatabaseManager)
        {
            this.bookDatabaseManager = bookDatabaseManager;
            text = new EnteringText();
            adminView = new AdminView();
            logo = new Logo();
        }
        public bool IsDuplicateId(string bookId)   //입력한 도서번호가 중복된 아이디인지 검사
        {
            LibraryVO library = LibraryVO.GetLibraryVO();  //도서목록

            MySqlCommand command = new MySqlCommand("select id from book where id='" + bookId + "';", library.Connection);
            MySqlDataReader table = command.ExecuteReader();

            if (table.HasRows)
            {
                table.Close();
                return Constants.IS_DUPLICATE_ID;  //이미 존재하는 아이디
            }

            table.Close();
            return Constants.IS_NON_DUPLICATE_ID;  //중복없는 아이디 -> 입력가능
        }
        public string InputBookId(int left, int top)
        {
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(left, top, "");        //도서번호를 입력 받음

                if (bookId.Equals(Constants.ESC))//뒤로가기
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH) //양식에 맞지 않은 입력
                {
                    logo.PrintMessage(0, top + 1, Constants.MESSAGE_ABOUT_BOOK_ID_NOT_MATCH, ConsoleColor.Red);
                }
                else if (IsDuplicateId(bookId))      //입력 양식은 맞지만 도서아이디가 이미 존재하는 경우
                {
                    logo.PrintMessage(0, top + 1, Constants.MESSAGE_ABOUT_DUPLICATED_BOOK_ID, ConsoleColor.Red);
                }
                else
                {
                    logo.RemoveLine(0, top + 1);
                    break;
                }

                logo.RemoveLine(left, top);
            }
            return bookId;
        }
        public string InputBookName(int left, int top)
        {
            string bookName;

            while (Constants.INPUT_VALUE)
            {
                bookName = text.EnterText(left, top, "");        //(도서명/출판사)를 입력 받음

                if (bookName.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(bookName, Constants.BOOK_NAME_REGEX) == Constants.IS_NOT_MATCH)
                {
                    logo.PrintMessage(0, top + 1, Constants.MESSAGE_ABOUT_BOOK_NAME, ConsoleColor.Red);
                }
                else
                {
                    logo.RemoveLine(0, top + 1);
                    break;
                }

                logo.RemoveLine(left, top);
            }

            return bookName;
        }
        public string InputAuthor(int left, int top)
        {
            string author;

            while (Constants.INPUT_VALUE)
            {
                author = text.EnterText(left, top, "");            //저자를 입력받음

                if (author.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(author, Constants.AUTHOR_REGEX) == Constants.IS_NOT_MATCH)   //입력형식에 맞지 않은 입력
                {
                    logo.PrintMessage(0, top + 1, Constants.MESSAGE_ABOUT_AUTHOR, ConsoleColor.Red);
                }
                else
                {
                    logo.RemoveLine(0, top + 1);
                    break;
                }

                logo.RemoveLine(left, top);
            }
            return author;
        }
        public string InputPrice(int left, int top)
        {
            string price;

            while (Constants.INPUT_VALUE)
            {
                price = text.EnterText(left, top, "");             //가격을 입력받음

                if (price.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(price, Constants.PRICE_REGEX) == Constants.IS_NOT_MATCH)
                {
                    logo.PrintMessage(0, top + 1, Constants.MESSAGE_ABOUT_PRICE, ConsoleColor.Red);
                }
                else
                {
                    logo.RemoveLine(0, top + 1);
                    break;
                }

                logo.RemoveLine(left, top);
            }

            return price;
        }
        public string InputQuantity(int left, int top)
        {
            EnteringText text = new EnteringText();
            string quantity;

            while (Constants.INPUT_VALUE)
            {
                quantity = text.EnterText(left, top, "");                    //수량을 입력받음

                if (quantity.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(quantity, Constants.QUENTITY_REGEX) == Constants.IS_NOT_MATCH)
                {
                    logo.PrintMessage(0, top + 1, Constants.MESSAGE_ABOUT_QUENTITY, ConsoleColor.Red);
                }
                else
                {
                    logo.RemoveLine(0, top + 1);
                    break;
                }
                logo.RemoveLine(left, top);
            }

            return quantity;
        }
        public void StartRegistration()
        {
            RegisteringScreen screen = new RegisteringScreen();
            screen.PrintRegistering(); //도서등록 입력화면

            string id = InputBookId(10, (int)Constants.Registration.FIRST);              //도서번호
            if (id.Equals(Constants.ESC)) return; 
            string name = InputBookName(8, (int)Constants.Registration.SECOND);          //도서명
            if (name.Equals(Constants.ESC)) return;
            string publisher = InputBookName(8, (int)Constants.Registration.THIRD);      //출판사
            if (publisher.Equals(Constants.ESC)) return;
            string author = InputAuthor(6, (int)Constants.Registration.FOURTH);          //저자
            if (author.Equals(Constants.ESC)) return;
            string price = InputPrice(6, (int)Constants.Registration.FIFTH);             //가격
            if (price.Equals(Constants.ESC)) return;
            string quantity = InputQuantity(6, (int)Constants.Registration.SIXTH);       //수량
            if (quantity.Equals(Constants.ESC)) return;

            LibraryVO library = LibraryVO.GetLibraryVO();  //도서목록
            library.InsertBookList(id, name, publisher, author, price, quantity); //도서목록에 등록된 도서정보 추가

            screen = new RegisteringScreen();
            screen.PrintComplete();                   //등록 완료 화면 출력

            Keyboard keyboard = new Keyboard();
            keyboard.PressESC();                      //esc->종료(뒤로가기)
        }
    }
}
