using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class BorrowingBook
    {
        private BookDAO bookDatabaseManager;
        private EnteringText text;
        private AdminMenu adminMenu;


        private LibraryVO library;//---->삭제할코드
        public BorrowingBook()
        {
            bookDatabaseManager = new BookDAO();
            text = new EnteringText();
            adminMenu = new AdminMenu();


            library = LibraryVO.GetLibraryVO();//---->삭제할코드
        }
        public void PrintInputBox(string message)
        {
            Console.SetCursorPosition(0, 2);
            Console.Write(message);
        }
        public bool IsBookIBorrowed(string memberId, string bookId)
        {
            string query = "select*from borrowBook where memberId='" + memberId + "' and bookId=" + bookId;

            MySqlCommand command = new MySqlCommand(query + ";", library.Connection);
            MySqlDataReader table = command.ExecuteReader();

            if (table.HasRows)
            {
                table.Close();
                return Constants.BOOK_I_BORROWED;
            }

            table.Close();
            return Constants.BOOK_I_NEVER_BORROWED;   //해당 검색어를 포함하는 도서를 찾지 못함
        }
        public bool IsBookOnList(string searchWord)   //입력값이 중복된 아이디인지 검사
        {
            string query = searchWord;

            MySqlCommand command = new MySqlCommand(query + ";", library.Connection);
            MySqlDataReader table = command.ExecuteReader();

            if (table.HasRows)
            {
                table.Close();
                return Constants.BOOK_IN_LIST;
            }

            table.Close();
            return Constants.BOOK_NOT_IN_LIST;   //해당 검색어를 포함하는 도서를 찾지 못함
        }
        public string InputBookId(string memberId, string searchWord)    //도서명 입력 후 도서번호 입력
        {
            Regex regex = new Regex(Constants.BOOK_ID_REGEX);  //도서번호:숫자,0~999까지
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(20, 1, "");           //도서번호를 입력 받음

                if (bookId.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (string.IsNullOrEmpty(bookId) || regex.IsMatch(bookId) == Constants.IS_NOT_MATCH) //형식에 맞지 않는 입력일 경우
                {
                    PrintInputBox("(0~999사이의 숫자가 아닙니다.다시 입력해주세요.)               ");
                }
                else if (IsBookOnList(searchWord + " and id=" + bookId) == Constants.BOOK_NOT_IN_LIST)  //입력형식은 맞지만, 목록에 없는 도서를 빌리려고 했을 때
                {
                    PrintInputBox("(현재 조회 목록에 없는 도서번호입니다. 다시 입력해주세요.)           ");
                }
                else if (IsBookIBorrowed(memberId, bookId))  //도서목록에 있지만, 이미 대여중인 도서일 때
                {
                    PrintInputBox("(이미 대여중인 도서입니다. 다른 도서를 선택해주세요.)                  ");
                }
                else if (IsBookOnList(searchWord + "and id='" + bookId + "' and quantity!=0") == Constants.BOOK_NOT_IN_LIST)   //도서목록에 있고, 대여하지 않은 도서이지만 수량이 0일 때
                {
                    PrintInputBox("(대여가능한 도서가 0권입니다. 다른 도서를 선택해주세요.)              ");
                }
                else break;  //도서대여가능 -> 해당 도서 아이디 리턴

                Console.SetCursorPosition(20, 1);
                Console.Write(Constants.REMOVE_LINE);
            }
            return bookId;
        }
        public void SearchBorrowBook(string memberId, SearchingBook searchingBook, Keyboard keyboard)
        {
            int searchType;        //검색유형
            string searchWord;     //검색어
            string bookId;         //도서번호
            List<BookVO> bookList; //검색결과

            while (Constants.INPUT_VALUE)
            {
                searchType = searchingBook.SelectSearchType(keyboard);   //검색유형 선택
                if (searchType == (int)Constants.Keyboard.ESCAPE) break; //검색유형 선택 중 esc -> 도서검색 종료

                searchWord = searchingBook.InputSearchWord((int)Constants.SearchMenu.LEFT_VALUE_OF_INPUT, searchType, (int)Constants.SearchMenu.ERROR_MESSAGE);   //검색어 입력받기
                if (searchWord.Equals(Constants.ESC)) continue;          //검색어 입력 중 esc -> 검색유형 선택으로

                adminMenu.PrintBookIdInputScreen(searchingBook.BookList);//도서검색창 출력

                bookId = InputBookId(memberId, searchWord);     //도서번호를 입력받음
                if (bookId.Equals(Constants.ESC)) continue;     //도서번호 입력 중 esc -> 다시 도서검색으로

                //if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break; //Esc->뒤로가기, Enter->재검색
            }

            //searchingBook.ShowSearchResult(keyboard);
            //string searchWord = searchingBook.SearchBook(keyboard); //도서명,출판사, 저자명 검색(리턴값 -> 쿼리문)
            //if (searchWord.Equals(Constants.ESC)) return;   //입력 중 esc -> 뒤로가기

            //adminMenu.PrintSearchBox("☞대여할 도서 번호:");

            //searchingScreen.PrintSearchingBook(searchWord, library); //검색결과로 나온 책목록 출력
            ///////////////////////////////////////////------->끝
            
            //string bookId = InputBookId(memberId, searchWord);     //도서번호를 입력받음
            //if (bookId.Equals(Constants.ESC)) return;  //도서번호 입력 중 esc -> 대여종료

            //borrowingScreen.PrintSuccessMessage();     //도서대여 완료 메세지 출력
            //library.InsertBorrowBook(memberId, bookId);//대여목록에 대여정보 저장

            //keyboard.PressESC();   //esc -> 뒤로가기
        }
    }
}
