using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class DeletingBook
    {
        private BookDAO bookDatabaseManager;
        private EnteringText text;
        private AdminView adminView;
        private Logo logo;

        public DeletingBook(BookDAO bookDatabaseManager)
        {
            this.bookDatabaseManager = bookDatabaseManager;
            text = new EnteringText();
            adminView = new AdminView();
            logo = new Logo();
        }

        private LibraryVO library = LibraryVO.GetLibraryVO();//-------->삭제할 코드
 
        public bool IsBorrowedBook(string bookId)
        {
            string query = "select*from borrowBook where bookId='" + bookId + "'";

            MySqlCommand command = new MySqlCommand(query + ";", library.Connection);
            MySqlDataReader table = command.ExecuteReader();

            if (table.HasRows)
            {
                table.Close();
                return Constants.BOOK_I_BORROWED;     //대여중인 회원이 있는 도서 -> 삭제 불가
            }

            table.Close();
            return Constants.BOOK_I_NEVER_BORROWED;   //대여한 회원이 없는 도서 -> 삭제 가능
        }
        public bool IsBookInList(string bookId, List<BookVO> bookList)
        {
            for(int i=0; i<bookList.Count; i++)
            {
                if(bookList[i].Id.Equals(bookId)) return Constants.IS_BOOK_IN_LIST;
            }

            return Constants.IS_BOOK_NOT_IN_LIST;   //현재 조회중인 도서목록에 입력받은 도서번호와 일치하는 도서가 없음
        }
        public string InputBookId(string searchWord, List<BookVO> bookList)
        {
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(24, 1, "");       //삭제할 도서번호를 입력받음

                if (bookId.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if(Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않은 입력
                {
                    logo.PrintMessage(0, 2, Constants.MESSAGE_ABOUT_BOOK_ID_NOT_MATCH, ConsoleColor.Red);
                }
                else if (IsBookInList(searchWord, bookList) == Constants.IS_BOOK_NOT_IN_LIST)      //현재 조회중인 도서목록에 없는 도서번호를 입력할 경우
                {
                    logo.PrintMessage(0, 2, Constants.MESSAGE_ABOUT_BOOK_NOT_IN_LIST, ConsoleColor.Red);
                }
                else if (IsBorrowedBook(bookId))   //도서목록에 있지만 대여중인 회원이 있는 경우 -> 도서 대여 불가
                {
                    logo.PrintMessage(0, 2, "(회원이 대여중인 도서는 삭제가 불가능합니다.)                 ", ConsoleColor.Red);
                }
                else break;   //도서삭제 가능

                logo.RemoveLine(24, 1);
            }

            return bookId;
        }
        public void DeleteBook(SearchingBook searchingBook, Keyboard keyboard)
        {
            //DeletingBookScreen deletingBookScreen = new DeletingBookScreen();//---->삭제할코드
            int searchType;
            string searchWord;
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                searchType = searchingBook.SelectSearchType(keyboard);     //검색유형 선택
                if (searchType == (int)Constants.Keyboard.ESCAPE) break;   //검색유형 선택 중 esc -> 도서검색 종료

                searchWord = searchingBook.InputSearchWord((int)Constants.SearchMenu.LEFT_VALUE_OF_INPUT, searchType, (int)Constants.SearchMenu.FOURTH);   //검색어 입력받기
                if (searchWord.Equals(Constants.ESC)) continue;            //검색어 입력 중 esc -> 검색유형 선택으로

                adminView.PrintBookIdInputScreen(searchingBook.BookList);  //도서번호 입력칸 + 도서 검색 결과 출력

                bookId = InputBookId(searchWord, searchingBook.BookList);

                if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break; //Esc->뒤로가기, Enter->재검색
            }

            //
            //if (bookId.Equals(Constants.ESC)) return;

            //deletingBookScreen.PrintSuccessMessage(bookId, library);   //도서삭제 완료 메세지 출력
            //library.DeleteBookList(bookId);                //리스트에서 도서삭제

            //keyboard.PressESC();
        }
    }
}
