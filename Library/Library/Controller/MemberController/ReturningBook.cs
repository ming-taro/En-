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

        private LibraryVO library;//---->삭제할 코드
        public ReturningBook()
        {
            bookDatabaseManager = new BookDAO();
            text = new EnteringText();
            memberView = new MemberView();
            logo = new Logo();

            library = LibraryVO.GetLibraryVO();   //----->삭제할 코드
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
        private void AddInBookListVO(string bookId)
        {
            int bookIndex;

            for(bookIndex = 0; bookIndex < library.bookList.Count; bookIndex++)
            {
                if (library.bookList[bookIndex].Id.Equals(bookId)) break;//반납한 도서 아이디를 도서목록에서 찾음
            }

            int quantity = int.Parse(library.bookList[bookIndex].Quantity) + 1; //해당 도서 수량을 +1만큼
            library.bookList[bookIndex].Quantity = quantity.ToString();
        }
        public void ShowMyBookList(string memberId, Keyboard keyboard)
        {
            List<BookVO> myBookList = bookDatabaseManager.MakeBorrowList(memberId); //현재 로그인한 회원의 도서대여목록

            logo.PrintSearchBox(Constants.BOOK_ID_TO_DELETE);    //도서번호 입력창
            memberView.PrintMyBookList(myBookList);              //나의 대여 목록 출력

            string bookId = InputBookId(memberId, myBookList);   //반납하려는 도서번호 입력
            if (bookId.Equals(Constants.ESC)) return;            //도서번호 입력 중 esc -> 뒤로가기

            bookDatabaseManager.DeleteFromRentalList(memberId, bookId); //도서대여리스트에서 대여정보 삭제 
            myBookList = bookDatabaseManager.MakeBorrowList(memberId);  //도서반납 후 대여목록



            keyboard.PressESC(); //esc -> 뒤로가기
        }
    }
}
