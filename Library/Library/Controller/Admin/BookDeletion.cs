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
        private BookDAO bookDAO = BookDAO.GetInstance();
        private LogDAO logDAO = LogDAO.GetInstance();
        private EnteringText text;
        private AdminView adminView;
        private Logo logo;
        public BookDeletion(EnteringText text, AdminView adminView, Logo logo)
        {
            this.text = text;
            this.adminView = adminView;
            this.logo = logo;
        }
        private bool IsBookInList(string bookId, List<BookDTO> bookList)
        {
            for(int i=0; i<bookList.Count; i++)
            {
                if(bookList[i].Id.Equals(bookId)) return Constants.IS_BOOK_IN_LIST;
            }

            return Constants.IS_BOOK_NOT_IN_LIST;   //현재 조회중인 도서목록에 입력받은 도서번호와 일치하는 도서가 없음
        }
        private string InputBookId(List<BookDTO> bookList)
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
                else if(Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH || Int32.Parse(bookId) < 1 || Int32.Parse(bookId) > bookList.Count)  //입력형식에 맞지 않은 입력
                {
                    logo.PrintMessage(exceptionLeft, exceptionTop, "(대여목록에 없는 도서번호입니다. 다시 입력해주세요.)", ConsoleColor.Red);
                }
                else if (bookDAO.IsBookOnLoan(bookList[Int32.Parse(bookId) - 1].Isbn))   //도서목록에 있지만 대여중인 회원이 있는 경우 -> 도서 삭제 불가
                {
                    logo.PrintMessage(exceptionLeft, exceptionTop, "(회원이 대여중인 도서는 삭제가 불가능합니다.)          ", ConsoleColor.Red);
                }
                else break;   //도서삭제 가능

                logo.RemoveLine(left, top);
            }

            return bookId;
        }
        private BookDTO FindBook(string bookId, List<BookDTO> bookList)   //삭제할 도서정보 찾기
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
            int bookIndex;
            int searchType;
            string searchWord;
            string bookId;
            List<BookDTO> bookList;

            while (Constants.INPUT_VALUE)
            {
                searchType = bookSearch.SelectSearchType(keyboard);        //검색유형 선택
                if (searchType == (int)Constants.Keyboard.ESCAPE) break;   //검색유형 선택 중 esc -> 도서검색 종료

                searchWord = bookSearch.InputSearchWord((int)Constants.InputField.SEARCH, searchType, (int)Constants.Exception.SEARCH);   //검색어 입력받기
                if (searchWord.Equals(Constants.ESC)) continue;            //검색어 입력 중 esc -> 검색유형 선택으로

                bookList = bookSearch.BookList;
                adminView.PrintBookDeletion(bookList);          //도서번호 입력칸 + 도서 검색 결과 출력

                bookId = InputBookId(bookList);                 //삭제할 도서번호 입력
                if (bookId.Equals(Constants.ESC)) continue;     //도서번호 입력 중 Esc -> 도서검색으로 돌아감

                bookIndex = Int32.Parse(bookId) - 1;
                adminView.PrintDeletedBook(bookList[bookIndex]);      //삭제 완료 메세지 + 삭제한 도서정보 출력

                bookDAO.DeleteFromBookList(bookList[bookIndex].Isbn); //DB에서 도서정보 삭제
                logDAO.DeleteFromBookList(bookList[bookIndex].Name);  //log에 도서삭제 기록

                if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break; //Esc->뒤로가기, Enter->재검색
            }
            Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
        }
    }
}
