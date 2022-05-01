using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class BookReturn
    {
        private BookDAO bookDatabaseManager;
        private EnteringText text;
        private MemberView memberView;
        private Exception exception;

        public BookReturn(BookDAO bookDatabaseManager, EnteringText text, MemberView memberView, Exception exception)
        {
            this.bookDatabaseManager = bookDatabaseManager;
            this.text = text;
            this.memberView = memberView;
            this.exception = exception;
        }
        private bool IsBookIBorrowed(string bookId, List<BorrowBookVO> myBookList)          //회원이 대여중인 도서인지 검사
        {
            for (int i = 0; i < myBookList.Count; i++)
            {
                if (myBookList[i].BookId.Equals(bookId)) return Constants.IS_BOOK_I_BORROWED;   //대여한 도서를 반납하려고 할 때 -> 반납가능
            }

            return Constants.IS_BOOK_I_NEVER_BORROWED;  //대여하지 않은 도서 -> 반납불가
        }
        private string InputBookId(string memberId, List<BorrowBookVO> myBookList)
        {
            int top = (int)Constants.SearchMenu.FIRST;
            int left = (int)Constants.InputField.RETURN;
            int exceptionLeft = (int)Constants.SearchMenu.LEFT;
            int exceptionTop = (int)Constants.Exception.RETURN;
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(left, top, "");//도서번호 입력

                if (bookId.Equals(Constants.ESC))  //도서번호 입력 중 esc -> 뒤로가기
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH)  //양식에 맞지 않는 입력
                {
                    exception.PrintBookIdRegex(exceptionLeft, exceptionTop);
                }
                else if (IsBookIBorrowed(bookId, myBookList) == Constants.IS_BOOK_I_NEVER_BORROWED)   //양식은 지켰지만, 내가 대여한 도서가 아닌 도서를 반납하려 할 때
                {
                    exception.PrintBookINeverBorrowed(exceptionLeft, exceptionTop);
                }
                else
                {
                    break;   //반납 성공
                }

                exception.RemoveLine(left, (int)Constants.SearchMenu.FIRST);
            }

            return bookId;
        }
        private BorrowBookVO FindBook(string bookId, List<BorrowBookVO> myBookList)  //도서번호로 해당 도서정보 찾기
        {
            int i = 0;

            for(i = 0; i<myBookList.Count; i++)
            {
                if (myBookList[i].BookId.Equals(bookId)) break;
            }

            return myBookList[i];
        }
        public void ReturnBook(string memberId, Keyboard keyboard) 
        {
            List<BorrowBookVO> myBookList = bookDatabaseManager.MakeMyBookList(Constants.RENTAL_LIST, memberId); //현재 로그인한 회원의 도서대여목록

            memberView.PrintBookReturn(myBookList);              //나의 대출 목록 출력

            string bookId = InputBookId(memberId, myBookList);   //반납하려는 도서번호 입력
            if (bookId.Equals(Constants.ESC)) return;            //도서번호 입력 중 Esc -> 회원모드로 돌아감

            memberView.PrintBookReturnSuccess(FindBook(bookId, myBookList));  //반납한 도서정보 출력
            bookDatabaseManager.DeleteFromRentalList(memberId, bookId); //도서대여리스트에서 대여정보 삭제 
            myBookList = bookDatabaseManager.MakeMyBookList(Constants.RENTAL_LIST, memberId);  //도서반납 후 대여목록

            keyboard.PressESC(); //도서 반납 후 Esc -> 회원모드로 돌아감
            Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
        }
    }
}
