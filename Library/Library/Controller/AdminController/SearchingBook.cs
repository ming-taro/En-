using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class SearchingBook
    {
        private BookDAO bookDatabaseManager;
        private AdminMenu adminMenu;
        private EnteringText text;
        public SearchingBook()
        {
            bookDatabaseManager = new BookDAO();
            adminMenu = new AdminMenu();
            text = new EnteringText();
        }
        public string InputSearchWord(int left, int top, int messageTop)
        {
            string searchWord;

            while (Constants.INPUT_VALUE)
            {
                searchWord = text.EnterText(left, top, "");        //(도서명/출판사/저자)를 입력 받음
                
                if (searchWord.Equals(Constants.ESC))              //입력도중 ESC -> 뒤로가기
                {
                    return Constants.ESC;
                }
                else if(Regex.IsMatch(searchWord, Constants.BOOK_NAME_REGEX) == Constants.IS_NOT_MATCH)
                {
                    adminMenu.PrintInputBox(0, messageTop, Constants.BOOK_NAME_ERROR_MESSAGE, ConsoleColor.Red);
                }
                else break; //목록에 있는 책에 대한 검색어를 입력한 경우

                adminMenu.PrintInputBox(left, top, Constants.REMOVE_LINE, ConsoleColor.Gray);
            }

            return searchWord;  //검색어 반환
        }
        public int SelectSearchType(Keyboard keyboard)
        {
            int searchType;

            adminMenu.PrintBookList(bookDatabaseManager.MakeBookList((int)Constants.SearchMenu.ALL, ""));   //전체도서 출력

            keyboard.SetPosition(0, (int)Constants.SearchMenu.BOOK_NAME);      //커서위치
            searchType = keyboard.SelectMenu((int)Constants.SearchMenu.BOOK_NAME, (int)Constants.SearchMenu.AUTHOR, (int)Constants.SearchMenu.STEP);               //메뉴선택
            if (searchType == (int)Constants.Keyboard.ESCAPE) return (int)Constants.Keyboard.ESCAPE;  //검색유형 선택 도중 뒤로가기 -> 관리자 메뉴로 돌아감

            return keyboard.Top;    //Enter를 누른 시점의 커서의 top값으로 선택한 검색유형을 알 수 있음
        }

        public void ShowSearchResult(Keyboard keyboard)
        {
            int searchType;
            string searchWord;
            List<BookVO> bookList;

            while (Constants.INPUT_VALUE)
            {
                searchType = SelectSearchType(keyboard);                 //검색유형 선택
                if (searchType == (int)Constants.Keyboard.ESCAPE) break; //검색유형 선택 중 esc -> 도서검색 종료

                searchWord = InputSearchWord((int)Constants.SearchMenu.LEFT_VALUE_OF_INPUT, searchType, (int)Constants.SearchMenu.ERROR_MESSAGE);   //검색어 입력받기
                if (searchWord.Equals(Constants.ESC)) continue;          //검색어 입력 중 esc -> 검색유형 선택으로

                bookList = bookDatabaseManager.MakeBookList(searchType, searchWord);  //검색결과

                if (bookList.Count == 0) adminMenu.PrintNoSearchResult();//검색결과가 없음
                else adminMenu.PrintBookList(bookList);                  //검색결과로 나온 책목록 출력

                if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break; //Esc->뒤로가기, Enter->재검색
            }
        }
    }
}
