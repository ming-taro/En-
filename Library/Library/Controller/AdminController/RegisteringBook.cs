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
        public void PrintInputBox(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }
        public bool IsDuplicateId(string bookId)   //입력한 도서번호가 중복된 아이디인지 검사
        {
            LibraryVO library = LibraryVO.GetLibraryVO();  //도서목록

            MySqlCommand command = new MySqlCommand("select id from book where id='" + bookId + "';", library.Connection);
            MySqlDataReader table = command.ExecuteReader();

            if (table.HasRows)
            {
                table.Close();
                return Constants.DUPLICATE_ID;  //이미 존재하는 아이디
            }

            table.Close();
            return Constants.NON_DUPLICATE_ID;  //중복없는 아이디 -> 입력가능
        }
        public string InputBookId(int left, int top)
        {
            EnteringText text = new EnteringText();
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(left, top, "");        //도서번호를 입력 받음

                if (bookId.Equals(Constants.ESC))//뒤로가기
                {
                    return Constants.ESC;
                }
                else if (string.IsNullOrEmpty(bookId) || Regex.IsMatch(bookId, @"^[0-9]{1,3}$") == Constants.IS_NOT_MATCH) //양식에 맞지 않은 입력
                {
                    PrintInputBox(0, top + 1, "(0~999사이의 숫자가 아닙니다. 다시 입력해주세요.)          ");
                }
                else if (IsDuplicateId(bookId))      //입력 양식은 맞지만 도서아이디가 이미 존재하는 경우
                {
                    PrintInputBox(0, top + 1, "(이미 존재하는 아이디입니다. 다시 입력해주세요.)          ");
                }
                else
                {
                    PrintInputBox(0, top + 1, Constants.REMOVE_LINE);
                    break;
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }
            return bookId;
        }
        public string InputBookName(int left, int top)
        {
            EnteringText text = new EnteringText();
            string bookName;

            while (Constants.INPUT_VALUE)
            {
                bookName = text.EnterText(left, top, "");        //(도서명/출판사)를 입력 받음
                if()
                else if (!string.IsNullOrEmpty(bookName) && Regex.IsMatch(bookName, @"^[\w]{1,1}[^\e]{0,49}$"))
                {
                    PrintInputBox(0, top + 1, Constants.REMOVE_LINE);
                    break;
                }
                else
                {
                    PrintInputBox(0, top + 1, "(공백으로 시작하지 않는 50자 이내의 글자를 입력해주세요.)                    ");
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }
            return bookName;
        }
        public string InputAuthor(int left, int top)
        {
            string author;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(left, top);
                author = Console.ReadLine();             //저자를 입력받음
                if (!string.IsNullOrEmpty(author) && Regex.IsMatch(author, @"^[a-zA-Z가-힣]{1,50}$"))
                {
                    PrintInputBox(0, top + 1, Constants.REMOVE_LINE);
                    break;
                }
                else
                {
                    PrintInputBox(0, top + 1, "(50자 이내의 영어, 한글만 입력해주세요.)                            ");
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }
            return author;
        }
        public string InputPrice(int left, int top)
        {
            string price;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(left, top);
                price = Console.ReadLine();             //가격을 입력받음
                if (!string.IsNullOrEmpty(price) && Regex.IsMatch(price, @"^[1-9]{1}[0-9]{0,9}"))
                {
                    PrintInputBox(0, top + 1, Constants.REMOVE_LINE);
                    break;
                }
                else
                {
                    PrintInputBox(0, top + 1, "(0이상의 숫자를 10자 이내로 다시 입력해주세요.)                                 ");
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }
            return price;
        }
        public string InputQuantity(int left, int top)
        {
            string quantity;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(left, top);
                quantity = Console.ReadLine();                     //수량을 입력받음
                if (!string.IsNullOrEmpty(quantity) && Regex.IsMatch(quantity, @"^[1-9]{1}[0-9]{0,1}$"))
                {
                    PrintInputBox(0, top + 1, Constants.REMOVE_LINE);
                    break;
                }
                else
                {
                    PrintInputBox(0, top + 1, "(1~99사이의 숫자만 가능합니다. 다시 입력해주세요.)                            ");
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }
            return quantity;
        }
        public void ControlRegistering()
        {
            RegisteringScreen screen = new RegisteringScreen();
            screen.PrintRegistering(); //도서등록 입력화면

            string id = InputBookId(10, (int)Constants.Registration.FIRST);              //도서번호
            if (id.Equals(Constants.ESC)) return; 

            string name = InputBookName(8, (int)Constants.Registration.SECOND);            //도서명
            string publisher = InputBookName(8, (int)Constants.Registration.THIRD);            //출판사
            string author = InputAuthor(6, (int)Constants.Registration.FOURTH);                   //저자
            string price = InputPrice(6, (int)Constants.Registration.FIFTH);                    //가격
            string quantity = InputQuantity(6, (int)Constants.Registration.SIXTH);                 //수량

            LibraryVO library = LibraryVO.GetLibraryVO();  //도서목록
            library.bookList.Add(new BookVO(book[0], book[1], book[2], book[3], book[4], book[5])); //도서목록에 등록된 도서정보 추가

            screen = new RegisteringScreen();
            screen.PrintComplete();                   //등록 완료 화면 출력

            return Constants.COMPLETE_FUNCTION;       //도서등록까지 모두 완료
        }
    }
}
