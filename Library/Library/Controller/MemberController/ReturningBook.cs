using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class ReturningBook
    {
        private BookDAO bookDatabaseManager;
        private EnteringText text;
        private MemberView memberView;
        private Logo logo;

        public ReturningBook(BookDAO bookDatabaseManager)
        {
            this.bookDatabaseManager = bookDatabaseManager;
            text = new EnteringText();
            memberView = new MemberView();
            logo = new Logo();
        }
        public bool IsBookIBorrowed(string bookId, List<BookVO> myBookList)              //회원이 대여중인 도서인지 검사
        {
            for (int i = 0; i < myBookList.Count; i++)
            {
                if (myBookList[i].Id.Equals(bookId)) return Constants.BOOK_I_BORROWED;   //대여한 도서를 반납하려고 할 때 -> 반납가능
            }

            return Constants.BOOK_I_NEVER_BORROWED;  //대여하지 않은 도서 -> 반납불가
        }
        private string InputBookId(string memberId, List<BookVO> myBookList)
        {
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(20, (int)Constants.SearchMenu.FIRST, "");//도서번호 입력

                if (bookId.Equals(Constants.ESC))  //도서번호 입력 중 esc -> 뒤로가기
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH)  //양식에 맞지 않는 입력
                {
                    logo.PrintMessage(0, (int)Constants.SearchMenu.SECOND, Constants.MESSAGE_ABOUT_BOOK_ID_NOT_MATCH, ConsoleColor.Red);
                }
                else if (IsBookIBorrowed(bookId, myBookList) == Constants.BOOK_I_NEVER_BORROWED)   //양식은 지켰지만, 내가 대여한 도서가 아닌 도서를 반납하려 할 때
                {
                    logo.PrintMessage(0, (int)Constants.SearchMenu.SECOND, Constants.MESSAGE_ABOUT_BOOK_I_NEVER_BORROWED, ConsoleColor.Red);
                }
                else
                {
                    break;   //반납 성공
                }

                logo.RemoveLine(20, (int)Constants.SearchMenu.FIRST);
            }

            return bookId;
        }
        public BookVO FindBook(string bookId, List<BookVO> myBookList)  //도서번호로 해당 도서정보 찾기
        {
            int i = 0;

            for(i = 0; i<myBookList.Count; i++)
            {
                if (myBookList[i].Id.Equals(bookId)) break;
            }

            return myBookList[i];
        }
        public void ShowMyBookList(string memberId, Keyboard keyboard) 
        {
            List<BookVO> myBookList = bookDatabaseManager.MakeMyBookList(memberId); //현재 로그인한 회원의 도서대여목록

            logo.PrintSearchBox("☞반납할 도서 번호:");    //도서번호 입력창
            Console.WriteLine();
            memberView.PrintMyBookList(myBookList);              //나의 대여 목록 출력

            string bookId = InputBookId(memberId, myBookList);   //반납하려는 도서번호 입력
            if (bookId.Equals(Constants.ESC)) return;            //도서번호 입력 중 Esc -> 회원모드로 돌아감

            memberView.PrintBookReturnSuccess(FindBook(bookId, myBookList));  //반납한 도서정보 출력
            bookDatabaseManager.DeleteFromRentalList(memberId, bookId); //도서대여리스트에서 대여정보 삭제 
            myBookList = bookDatabaseManager.MakeMyBookList(memberId);  //도서반납 후 대여목록

            keyboard.PressESC(); //도서 반납 후 Esc -> 회원모드로 돌아감
        }
    }
}
