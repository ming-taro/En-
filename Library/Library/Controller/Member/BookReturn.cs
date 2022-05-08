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
        private BookDAO bookDAO = BookDAO.GetInstance();
        private LogDAO logDAO = LogDAO.GetInstance();
        private EnteringText text;
        private MemberView memberView;
        private Logo logo;

        public BookReturn(EnteringText text, MemberView memberView, Logo logo)
        {
            this.text = text;
            this.memberView = memberView;
            this.logo = logo;
        }
        private string InputBookId(string memberId, List<BookDTO> myBookList)
        {
            int top = (int)Constants.SearchMenu.FIRST;
            int left = (int)Constants.InputField.LEFT;
            int exceptionLeft = (int)Constants.SearchMenu.LEFT;
            int exceptionTop = (int)Constants.Exception.TOP;
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(left, top, "");//도서번호 입력
                
                if (bookId.Equals(Constants.ESC) == Constants.IS_NOT_MATCH && (Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH || Int32.Parse(bookId) < 1 || Int32.Parse(bookId) > myBookList.Count))    //양식에 맞지 않는 입력
                {
                    logo.PrintMessage(exceptionLeft, exceptionTop, "(대여목록에 없는 도서번호입니다. 다시 입력해주세요.)   ", ConsoleColor.Red);
                }
                else
                {
                    break;   //반납 성공
                }

                logo.RemoveLine(left, (int)Constants.SearchMenu.FIRST);
            }

            return bookId;
        }
        private void SaveChanges(string memberId, string bookId, List<BookDTO> myBookList)
        {
            int bookIndex = Int32.Parse(bookId) - 1;

            bookDAO.DeleteFromRentalList(memberId, myBookList[bookIndex].RentalPeriod, myBookList[bookIndex].Isbn);     //도서대여리스트에서 대여정보 삭제 
            logDAO.DeleteFromRentalList(memberId, myBookList[bookIndex].Name);              //로그에 반납기록 추가

            memberView.PrintBookReturnSuccess(myBookList[bookIndex]);  //반납한 도서정보 출력
            myBookList.RemoveAt(bookIndex);                            //도서반납 후 대여목록

            for(int index = bookIndex; index < myBookList.Count; index++)
            {
                myBookList[index].Id = (index + 1).ToString();         //도서번호 수정
            }
        }
        public void ManageBookReturn(string memberId, Keyboard keyboard) 
        {
            string bookId;
            List<BookDTO> myBookList = bookDAO.GetMyBookList(Constants.RENTAL_LIST, memberId); //현재 로그인한 회원의 도서대여목록

            while (Constants.INPUT_VALUE)
            {
                memberView.PrintBookReturn(myBookList);             //나의 대출 목록 출력

                bookId = InputBookId(memberId, myBookList);         //반납하려는 도서번호 입력
                if (bookId.Equals(Constants.ESC)) break;            //도서번호 입력 중 Esc -> 회원모드로 돌아감

                SaveChanges(memberId, bookId, myBookList);

                if(keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break;  //도서 반납 후 Esc -> 회원모드로 돌아감
                Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
            }
            Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
        }
    }
}
