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
            BookListVO bookListVO = BookListVO.GetBookListVO();  //도서목록
            Console.WriteLine(bookListVO.bookList[0].Id);
            for (int i = 0; i < bookListVO.bookList.Count; i++)
            {
                if (bookListVO.bookList[i].Id.Equals(bookId)) return Constants.DUPLICATE_ID;  //이미 존재하는 아이디
            }
            return !Constants.DUPLICATE_ID;  //중복없는 아이디 -> 입력가능
        }
        public string InputBookId(int left, int top)
        {
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(left, top);
                bookId = Console.ReadLine();        //도서번호를 입력 받음
                if (!string.IsNullOrEmpty(bookId) && Regex.IsMatch(bookId, @"^[0-9]{1,3}$") && !IsDuplicateId(bookId)) //중복 없이 알맞게 입력한 경우
                {
                    PrintInputBox(0, top + 1, Constants.REMOVE_LINE);
                    break;
                }
                else if (!string.IsNullOrEmpty(bookId) && Regex.IsMatch(bookId, @"^[0-9]{1,3}$"))      //입력 양식은 맞지만 도서아이디가 이미 존재하는 경우
                {
                    PrintInputBox(0, top + 1, "(이미 존재하는 아이디입니다. 다시 입력해주세요.)          ");
                }
                else
                {
                    PrintInputBox(0, top + 1, "(0~999사이의 숫자가 아닙니다. 다시 입력해주세요.)          ");
                }
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }
            return bookId;
        }
        public string InputBookName(int left, int top)
        {
            string bookName;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(left, top);
                bookName = Console.ReadLine();        //(도서명/출판사)를 입력 받음
                if (!string.IsNullOrEmpty(bookName) && Regex.IsMatch(bookName, @"^[\w]{1,1}[^\e]{0,49}$"))
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
        public int ControlRegistering()
        {
            RegisteringScreen screen = new RegisteringScreen();
            screen.PrintRegistering(); //도서등록 입력화면

            string[] book = new string[6];

            book[0] = InputBookId(10, 7);              //도서번호
            book[1] = InputBookName(8, 10);            //도서명
            book[2] = InputBookName(8, 13);            //출판사
            book[3] = InputAuthor(6, 16);                   //저자
            book[4] = InputPrice(6, 19);                    //가격
            book[5] = InputQuantity(6, 22);                 //수량

            BookListVO bookListVO = BookListVO.GetBookListVO();  //도서목록
            bookListVO.bookList.Add(new BookVO(book[0], book[1], book[2], book[3], book[4], book[5])); //도서목록에 등록된 도서정보 추가

            screen = new RegisteringScreen();
            screen.PrintComplete();                   //등록 완료 화면 출력

            return Constants.COMPLETE_FUNCTION;       //도서등록까지 모두 완료
        }
    }
}
