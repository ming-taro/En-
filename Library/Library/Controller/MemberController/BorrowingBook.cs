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
        private Logo logo;
        
        public BorrowingBook()
        {
            bookDatabaseManager = new BookDAO();
            text = new EnteringText();
            adminMenu = new AdminMenu();
            logo = new Logo();
        }

        public bool IsBookInList(string bookId, List<BookVO> bookList)   //현재 조회중인 도서목록에 있는 도서인지 검사
        {
            for(int i=0; i<bookList.Count; i++)
            {
                if (bookList[i].Id.Equals(bookId)) return Constants.IS_BOOK_IN_LIST;  //목록에 존재하는 도서
            }

            return Constants.IS_BOOK_NOT_IN_LIST;    //현재 조회중인 도서목록에 없는 도서를 대여하려고 함
        }
        public bool IsBookIBorrowed(string bookId, List<BookVO> borrowList)              //회원이 대여중인 도서인지 검사
        {
            for (int i = 0; i < borrowList.Count; i++)
            {
                if (borrowList[i].Id.Equals(bookId)) return Constants.BOOK_I_BORROWED;   //입력한 도서를 이미 대여중 -> 대여할 수 없음
            }

            return Constants.BOOK_I_NEVER_BORROWED;  //대여하지 않은 도서 -> 대여가능
        }
        public bool IsQuantityZero(string bookId, List<BookVO> bookList)   //대여가능 수량이 있는 지 검사
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].Id.Equals(bookId) && bookList[i].Quantity.Equals("0")) return Constants.IS_QUANTITY_ZERO;  //대여 가능 수량이 0권 -> 대여 불가
            }

            return Constants.IS_QUANTITY_MORE_THAN_ONE;   //대여 가능 수량이 남아있음 -> 대여가능
        }
        public string InputBookId(List<BookVO> bookList, List<BookVO> borrowList)    //도서명 입력 후 도서번호 입력
        {
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(20, (int)Constants.SearchMenu.FIRST, "");           //도서번호를 입력 받음

                if (bookId.Equals(Constants.ESC))  //도서번호 입력 중 esc -> 다시 도서검색으로
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH)  //형식에 맞지 않는 입력일 경우
                {
                    logo.PrintMessage(0, (int)Constants.SearchMenu.SECOND, Constants.MESSAGE_ABOUT_BOOK_ID_NOT_MATCH, ConsoleColor.Red);
                }
                else if (IsBookInList(bookId, bookList) == Constants.IS_BOOK_NOT_IN_LIST)  //입력형식은 맞지만, 목록에 없는 도서를 빌리려고 했을 때
                {
                    logo.PrintMessage(0, (int)Constants.SearchMenu.SECOND, Constants.MESSAGE_ABOUT_BOOK_NOT_IN_LIST, ConsoleColor.Red);
                }
                else if (IsBookIBorrowed(bookId, borrowList)) //도서목록에 있지만, 이미 대여중인 도서일 때
                {
                    logo.PrintMessage(0, (int)Constants.SearchMenu.SECOND, Constants.MESSAGE_ABOUT_BOOK_I_BORROWED, ConsoleColor.Red);
                }
                else if (IsQuantityZero(bookId, bookList))   //도서목록에 있고, 대여하지 않은 도서이지만 수량이 0일 때
                {
                    logo.PrintMessage(0, (int)Constants.SearchMenu.SECOND, Constants.MESSAGE_ABOUT_QUANTITY_ZERO, ConsoleColor.Red);
                }
                else break;  //도서대여가능 -> 해당 도서 아이디 리턴

                Console.SetCursorPosition(20, (int)Constants.SearchMenu.FIRST);
                Console.Write(Constants.REMOVE_LINE);
            }
            return bookId;
        }
        public void SearchBorrowBook(string memberId, SearchingBook searchingBook, Keyboard keyboard)
        {
            int searchType;        //검색유형
            string searchWord;     //검색어
            string bookId;         //도서번호
            List<BookVO> borrowList = bookDatabaseManager.MakeBorrowList(memberId); //현재 로그인한 회원의 도서대여목록

            while (Constants.INPUT_VALUE)
            {
                searchType = searchingBook.SelectSearchType(keyboard);   //검색유형 선택
                if (searchType == (int)Constants.Keyboard.ESCAPE) break; //검색유형 선택 중 esc -> 도서검색 종료

                searchWord = searchingBook.InputSearchWord((int)Constants.SearchMenu.LEFT_VALUE_OF_INPUT, searchType, (int)Constants.SearchMenu.FOURTH);   //검색어 입력받기
                if (searchWord.Equals(Constants.ESC)) continue;          //검색어 입력 중 esc -> 검색유형 선택으로

                adminMenu.PrintBookIdInputScreen(searchingBook.BookList, logo);//도서검색창 출력

                bookId = InputBookId(searchingBook.BookList, borrowList);//도서번호를 입력받음
                if (bookId.Equals(Constants.ESC)) continue;              //도서번호 입력 중 esc -> 다시 도서검색으로


                if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break; //Esc->뒤로가기, Enter->재검색
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
