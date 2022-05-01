﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class BookSearch
    {
        private BookDAO bookDatabaseManager;
        private EnteringText text;
        private AdminView adminView;
        private Exception exception;
        private List<BookVO> bookList;
        public BookSearch(BookDAO bookDatabaseManager, EnteringText text, AdminView adminView, Exception exception)
        {
            this.bookDatabaseManager = bookDatabaseManager;
            this.text = text;
            this.adminView = adminView;
            this.exception = exception;
        }
        public List<BookVO> BookList
        {
            get { return bookList; }
        }
        public string InputSearchWord(int left, int top, int exceptionTop)
        {
            string searchWord;

            while (Constants.INPUT_VALUE)
            {
                searchWord = text.EnterText(left, top, "");                           //(도서명/출판사/저자)를 입력 받음
                bookList = bookDatabaseManager.MakeBookList(top, searchWord);         //검색결과(커서의 top값 == 검색유형)

                if (searchWord.Equals(Constants.ESC))   //검색어 입력도중 ESC -> 뒤로가기
                {
                    return Constants.ESC;
                }
                else if(Regex.IsMatch(searchWord, Constants.BOOK_NAME_REGEX) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않은 검색어 입력
                {
                    exception.PrintBookNameRegex((int)Constants.SearchMenu.LEFT, exceptionTop);
                }
                else if(bookList.Count == 0)
                {
                    exception.PrintBookNotInList((int)Constants.SearchMenu.LEFT, exceptionTop);
                }
                else break;

                exception.RemoveLine(left, top);
            }

            return searchWord;  //검색어 반환
        }
        public int SelectSearchType(Keyboard keyboard)
        {
            int searchType;

            adminView.PrintBookSearch(bookDatabaseManager.MakeBookList((int)Constants.SearchMenu.ALL, ""));  //도서검색화면 + 전체 도서목록
            
            keyboard.SetPosition((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.FIRST);    //커서위치
            searchType = keyboard.SelectMenu((int)Constants.SearchMenu.FIRST, (int)Constants.SearchMenu.THIRD, (int)Constants.SearchMenu.STEP);               //메뉴선택
            if (searchType == (int)Constants.Keyboard.ESCAPE) return (int)Constants.Keyboard.ESCAPE;  //검색유형 선택 도중 뒤로가기 -> 관리자 메뉴로 돌아감

            return keyboard.Top;    //Enter를 누른 시점의 커서의 top값으로 선택한 검색유형을 알 수 있음
        }
        public void ShowSearchResult(Keyboard keyboard)
        {
            int searchType;
            string searchWord;

            while (Constants.INPUT_VALUE)
            {
                searchType = SelectSearchType(keyboard);                   //검색유형 선택
                if (searchType == (int)Constants.Keyboard.ESCAPE) break;   //검색유형 선택 중 esc -> 도서검색 종료

                searchWord = InputSearchWord((int)Constants.InputField.SEARCH, searchType, (int)Constants.Exception.SEARCH);   //검색어 입력받기
                if (searchWord.Equals(Constants.ESC)) continue;            //검색어 입력 중 esc -> 검색유형 선택으로
                
                adminView.PrintSearchResult(bookList);               //도서 검색 결과 출력

                if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break; //Esc->뒤로가기, Enter->재검색
            }
        }
    }
}
