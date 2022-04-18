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
        private LibraryVO library;
        public SearchingBook()
        {
            library = LibraryVO.GetLibraryVO();
        }
        public void PrintInputBox(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }
        public bool IsBookInList(int menu, string searchWord)
        {
            string sql = "";

            switch (menu)
            {
                case 1:
                    sql = "select*from book where name like \"%" + searchWord + "%\";";
                    break;
                case 2:
                    sql = "select*from book where publisher like \"%" + searchWord + "%\";";
                    break;
                case 3:
                    sql = "select*from book where author like \"%" + searchWord + "%\";";
                    break;
            }

            MySqlCommand command = new MySqlCommand(sql, library.Connection);
            MySqlDataReader table = command.ExecuteReader();

            if (table.HasRows)
            {
                return Constants.BOOK_IN_LIST;
            }

            return !Constants.BOOK_NOT_IN_LIST;   //해당 검색어를 포함하는 도서를 찾지 못함
        }
        public string InputSearchWord(int left, int top, int messageTop)
        {
            string searchWord;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(left, top);
                searchWord = Console.ReadLine();        //(도서명/출판사/저자)를 입력 받음
                if (string.IsNullOrEmpty(searchWord) || !Regex.IsMatch(searchWord, @"^[\w]{1,1}[^\e]{0,49}$") || !IsBookInList(top, searchWord))
                {
                    PrintInputBox(0, messageTop, "(해당 검색어와 일치하는 도서가 없습니다. 다시 입력해주세요.)        ");
                }
                else break;
                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }

            return searchWord;
        }
        public int ControlSearchingBook()  //도서검색하기(검색어 입력 -> 목록 출력)
        {
            SearchingScreen screen = new SearchingScreen();
            screen.PrintSearchingBook(libraryVO.bookList);

            Keyboard keyboard = new Keyboard();
            keyboard.SetPosition(0, 1);     //커서위치
            int menu = keyboard.SelectMenu(1, 3, 1);                  //메뉴선택
            if (menu == Constants.ESCAPE) return Constants.ESCAPE;    //메뉴선택도중 뒤로가기 -> 관리자 메뉴로 돌아감

            menu = keyboard.Top;                //메뉴선택 완료(1.도서명  2.출판사  3.저자)
            string name = InputSearchWord(10, menu, 4);     //검색어 입력받기
            Console.Clear();
            screen.PrintSearchingBook(menu, name, libraryVO.bookList);   //검색결과로 나온 책목록 출력

            return Constants.COMPLETE_FUNCTION;      //검색결과 출력까지 모두 완료
        }

    }
}
