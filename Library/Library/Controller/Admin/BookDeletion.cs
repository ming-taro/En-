using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class BookDeletion
    {
        private EnteringText text;
        private AdminView adminView;
        private Exception exception;
        public BookDeletion(EnteringText text, AdminView adminView, Exception exception)
        {
            this.text = text;
            this.adminView = adminView;
            this.exception = exception;
        }
        private bool IsBookInList(string bookId, List<BookVO> bookList)
        {
            for(int i=0; i<bookList.Count; i++)
            {
                if(bookList[i].Id.Equals(bookId)) return Constants.IS_BOOK_IN_LIST;
            }

            return Constants.IS_BOOK_NOT_IN_LIST;   //현재 조회중인 도서목록에 입력받은 도서번호와 일치하는 도서가 없음
        }
        private string InputBookId(List<BookVO> bookList)
        {
            int top = (int)Constants.SearchMenu.FIRST;
            int left = (int)Constants.InputField.LEFT;
            int exceptionLeft = (int)Constants.SearchMenu.LEFT;
            int exceptionTop = (int)Constants.Exception.TOP;
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(left, top, "");       //삭제할 도서번호를 입력받음

                if (bookId.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if(Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않은 입력
                {
                    exception.PrintBookIdRegex(exceptionLeft, exceptionTop);
                }
                else if (IsBookInList(bookId, bookList) == Constants.IS_BOOK_NOT_IN_LIST)      //현재 조회중인 도서목록에 없는 도서번호를 입력할 경우
                {
                    exception.PrintBookNotInList(exceptionLeft, exceptionTop);
                }
                else if (BookDAO.bookDAO.IsBookOnLoan(bookId))   //도서목록에 있지만 대여중인 회원이 있는 경우 -> 도서 삭제 불가
                {
                    exception.PrintBookOnLoan(exceptionLeft, exceptionTop);
                }
                else break;   //도서삭제 가능

                exception.RemoveLine(left, top);
            }

            return bookId;
        }
        private BookVO FindBook(string bookId, List<BookVO> bookList)   //삭제할 도서정보 찾기
        {
            int i;

            for(i = 0; i<bookList.Count; i++)
            {
                if (bookList[i].Id.Equals(bookId)) break;
            }

            return bookList[i];
        }
        public void DeleteBook(BookSearch bookSearch, Keyboard keyboard)
        {
            int searchType;
            string searchWord;
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                searchType = bookSearch.SelectSearchType(keyboard);        //검색유형 선택
                if (searchType == (int)Constants.Keyboard.ESCAPE) break;   //검색유형 선택 중 esc -> 도서검색 종료

                searchWord = bookSearch.InputSearchWord((int)Constants.InputField.SEARCH, searchType, (int)Constants.Exception.SEARCH);   //검색어 입력받기
                if (searchWord.Equals(Constants.ESC)) continue;            //검색어 입력 중 esc -> 검색유형 선택으로

                adminView.PrintBookDeletion(bookSearch.BookList);          //도서번호 입력칸 + 도서 검색 결과 출력

                bookId = InputBookId(bookSearch.BookList);                 //삭제할 도서번호 입력
                if (bookId.Equals(Constants.ESC)) continue;                //도서번호 입력 중 Esc -> 도서검색으로 돌아감
                
                adminView.PrintDeletedBook(FindBook(bookId, bookSearch.BookList));    //삭제 완료 메세지 + 삭제한 도서정보 출력
                BookDAO.bookDAO.DeleteFromBookList(bookId);            //DB에서 도서정보 삭제

                if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break; //Esc->뒤로가기, Enter->재검색
            }
            Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
        }
    }
}
