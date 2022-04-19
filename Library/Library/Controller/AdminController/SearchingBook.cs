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
        public bool IsBookOnList(int menu, string searchWord, ref string query)
        {
            switch (menu)
            {
                case (int)Constants.SearchMenu.BOOK_NAME:
                    query = "select*from book where name like '%" + searchWord + "%'";
                    break;
                case (int)Constants.SearchMenu.PUBLISHER:
                    query = "select*from book where publisher like '%" + searchWord + "%'";
                    break;
                case (int)Constants.SearchMenu.AUTHOR:
                    query = "select*from book where author like '%" + searchWord + "%'";
                    break;
            }

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
        public string InputSearchWord(int left, int top, int messageTop)
        {
            EnteringText text = new EnteringText();
            string query = "";
            string searchWord;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(left, top);
                searchWord = text.EnterText(left, top, "");        //(도서명/출판사/저자)를 입력 받음
                
                if (searchWord.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if(string.IsNullOrEmpty(searchWord) || !Regex.IsMatch(searchWord, @"^[\w]{1,1}[^\e]{0,49}$") || IsBookOnList(top, searchWord, ref query) == Constants.BOOK_NOT_IN_LIST)
                {
                    PrintInputBox(0, messageTop, "(해당 검색어와 일치하는 도서가 없습니다. 다시 입력해주세요.)        ");
                } 
                else break;

                PrintInputBox(left, top, Constants.REMOVE_LINE);
            }

            return query; //검색어를 포함하는 도서가 존재함
        }
        public string SearchBook()  //도서검색하기(검색어 입력 -> 목록 출력)
        {
            SearchingScreen screen = new SearchingScreen();
            Keyboard keyboard = new Keyboard();
            string query;
            int menu;

            screen.PrintSearchBox();
            screen.PrintSearchingBook("select*from book", library);   //전체도서 출력

            keyboard.SetPosition(0, 1);     //커서위치
            menu = keyboard.SelectMenu(1, 3, 1);               //메뉴선택
            if (menu == Constants.ESCAPE) return Constants.ESC;    //메뉴선택도중 뒤로가기 -> 관리자 메뉴로 돌아감

            menu = keyboard.Top;                //메뉴선택 완료(1.도서명  2.출판사  3.저자)
            query = InputSearchWord(10, menu, 4); //검색어 입력받기
            if (query.Equals(Constants.ESC)) return Constants.ESC;     //검색어 입력 중 뒤로가기

            return query;  //검색어를 입력하는 쿼리문 리턴
        }
        public void ShowSearchResult()
        {
            SearchingScreen screen = new SearchingScreen();
            Keyboard keyboard = new Keyboard();

            string query = SearchBook();
            if (query.Equals(Constants.ESC)) return;    //도중에 esc -> 도서검색 종료 

            Console.Clear();
            screen.PrintSearchingBook(query, library);   //검색결과로 나온 책목록 출력

            keyboard.PressESC();  //esc -> 뒤로가기
        }
    }
}
