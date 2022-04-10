using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class Borrowing
    {
        public Borrowing()
        {
            BorrowingScreen borrowingScreen = new BorrowingScreen();
            borrowingScreen.PrintBorrowing();
        }
        public void PrintInputBox(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }
        public bool IsDuplicateId(string bookId)   //입력값이 중복된 아이디인지 검사
        {
            BookListVO bookListVO = BookListVO.GetBookListVO();  //도서목록

            for (int i = 0; i < bookListVO.bookList.Count; i++)
            {
                if (bookListVO.bookList[i].Id.Equals(bookId)) return Constants.DUPLICATE_ID;  //도서목록에 존재함
            }

            return !Constants.DUPLICATE_ID;  //도서목록에 없는 책을 대여하려 함
        }
        public bool IsQuantityZero(string bookId)
        {
            BookListVO bookListVO = BookListVO.GetBookListVO();  //도서목록

            for(int i = 0; i<bookListVO.bookList.Count; i++)
            {
                if (bookListVO.bookList[i].Id.Equals(bookId) && bookListVO.bookList[i].Quantity.Equals("0"))
                {
                    return Constants.QUANTITY_ZERO;
                }
            }

            return !Constants.QUANTITY_ZERO;
        }
        public string InputBookId()          //도서명 입력 후 도서번호 입력
        {
            Regex regex = new Regex(@"^[0-9]{1,3}$");   //도서번호:숫자,0~999까지
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(10, 1);
                bookId = Console.ReadLine();            //도서번호를 입력 받음
                if (string.IsNullOrEmpty(bookId) || !regex.IsMatch(bookId)) //형식에 맞지 않는 입력일 경우
                {
                    PrintInputBox(0, 2, "(0~999사이의 숫자가 아닙니다.다시 입력해주세요.)               ");
                }
                else if (!IsDuplicateId(bookId))         //입력형식은 맞지만, 목록에 없는 도서를 빌리려고 했을 때
                {
                    PrintInputBox(0, 2, "(도서목록에 없는 도서번호입니다. 다시 입력해주세요.)           ");
                }
                else if (IsQuantityZero(bookId))  //목록에 있는 도서이지만 수량이 0일 때
                {
                    PrintInputBox(0, 2, "(대여가능한 도서가 0권입니다. 다시 입력해주세요.)              ");
                    return Constants.RE_ENTER;
                }
                else break;  //도서목록에 있는 책을 빌리려고 했을 때 -> 해당 도서 아이디 리턴
            }
            return bookId;
        }
        public int ControlBorrowing(string memberId)
        {
            Registering registeringBook = new Registering();
            while (Constants.INPUT_VALUE)
            {
                string bookName = registeringBook.InputBookName(19, 1, "도서명으로 검색");   //먼저 도서명 입력받기
                string bookId = InputBookId();  //도서번호를 입력받음
                if (!bookId.Equals(Constants.RE_ENTER)) break;   //도서번호를 잘못 입력받으면 도서명부터 다시 입력
            }
            
            return Constants.COMPLETE_FUNCTION;
        }
    }
}
