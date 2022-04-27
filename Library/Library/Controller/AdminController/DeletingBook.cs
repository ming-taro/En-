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
        private LibraryVO library;
        public DeletingBook()
        {
            library = LibraryVO.GetLibraryVO();
        }

        public void PrintInputBox(int left, int top, string message)///---->뷰로빼기(겹침)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }
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
        public bool IsBookOnList(string searchWord, string bookId)
        {
            string query = searchWord + " and id='" + bookId + "'";

            MySqlCommand command = new MySqlCommand(query + ";", library.Connection);
            MySqlDataReader table = command.ExecuteReader();

            if (table.HasRows)
            {
                table.Close();
                return Constants.IS_BOOK_IN_LIST;
            }

            table.Close();
            return Constants.IS_BOOK_NOT_IN_LIST;   //해당 검색어를 포함하는 도서를 찾지 못함
        }
        public string InputBookId(string searchWord)
        {
            EnteringText text = new EnteringText();
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(24, 1, "");       //도서번호를 입력받음
                if (bookId.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if(string.IsNullOrEmpty(bookId) || Regex.IsMatch(bookId, @"^[0-9]{1,3}$") == Constants.IS_NOT_MATCH || IsBookOnList(searchWord, bookId) == Constants.IS_BOOK_NOT_IN_LIST)
                {
                    PrintInputBox(0, 2, "(현재 조회 목록에 없는 도서번호입니다.다시 입력해주세요.)          ");
                }
                else if (IsBorrowedBook(bookId))   //도서목록에 있지만 대여중인 회원이 있는 경우 -> 도서 대여 불가
                {
                    PrintInputBox(0, 2, "(회원이 대여중인 도서는 삭제가 불가능합니다.)                 ");
                }
                else break;   //도서삭제 가능
                PrintInputBox(24, 1, Constants.REMOVE_LINE);
            }

            return bookId;
        }
        public void DeleteBook()
        {
            AdminMenu screen = new AdminMenu();
            DeletingBookScreen deletingBookScreen = new DeletingBookScreen();
            Logo logoScreen = new Logo();
            Keyboard keyboard = new Keyboard();
            SearchingBook searchingBook = new SearchingBook();

            string searchWord;
            string bookId;

            //searchWord = searchingBook.SearchBook(keyboard); //도서명,출판사, 저자명 검색(리턴값 -> 쿼리문)
            //if (searchWord.Equals(Constants.ESC)) return;   //입력 중 esc -> 뒤로가기

            logoScreen.PrintSearchBox("☞삭제할 도서번호 입력: ");
            screen.PrintSearchingBook("select*from book", library);   //전체도서 출력

            //bookId = InputBookId(searchWord);
            //if (bookId.Equals(Constants.ESC)) return;

            //deletingBookScreen.PrintSuccessMessage(bookId, library);   //도서삭제 완료 메세지 출력
            //library.DeleteBookList(bookId);                //리스트에서 도서삭제

            keyboard.PressESC();
        }
    }
}
