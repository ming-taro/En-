using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace Library
{
    class NaverSearch
    {
        private EnteringText text;
        private AdminView adminView;
        private Exception exception;
        private List<BookVO> bookList;
        public NaverSearch(EnteringText text, AdminView adminView, Exception exception)
        {
            this.text = text;
            this.adminView = adminView;
            this.exception = exception;
            bookList = new List<BookVO>();
        }
        public void MakeBookList(string searchWord)
        {
            int count;
            string text;
            string url;

            url = string.Format(Constants.API_URL, searchWord);  //네이버 도서검색
            WebRequest request = WebRequest.Create(url);
            request.Headers.Add(Constants.NAVER_CLIENT_ID, Constants.CLIENT_ID);         // 개발자센터에서 발급받은 Client ID
            request.Headers.Add(Constants.NAVER_CLIENT_SECRET, Constants.CLIENT_SECRET); // 개발자센터에서 발급받은 Client Secret

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            text = reader.ReadToEnd();
            text = text.Replace("<b>", "");
            text = text.Replace("</b>", "");
            text = text.Replace("&lt", "<");
            text = text.Replace("&gt", ">");

            JObject searchResult = JObject.Parse(text);
            count = Convert.ToInt32(searchResult["display"]);

            for (int i = 0; i < count; i++) 
            {
                bookList.Add(new BookVO((i + 1).ToString(), searchResult["items"][i]["title"].ToString(), searchResult["items"][i]["publisher"].ToString(), searchResult["items"][i]["author"].ToString(), searchResult["items"][i]["price"].ToString(), ""));
            }
        }
        public void PrintErrorMessage(string regextTect)
        {
            int left = (int)Constants.SearchMenu.LEFT;
            int top = (int)Constants.SearchMenu.SECOND + 1;

            switch (regextTect)
            {
                case Constants.BOOK_NAME_REGEX:
                    exception.PrintBookNameRegex(left, top);
                    break;
                case Constants.BOOK_ID_REGEX:
                    exception.PrintBookNotInList(left, top);
                    break;
            }
        }
        public string InputSearchWord()
        {
            int left = (int)Constants.InputField.SEARCH;
            int top = (int)Constants.SearchMenu.FIRST;
            string searchWord;

            while (Constants.INPUT_VALUE)
            {
                exception.RemoveLine(left, top);
                searchWord = text.EnterText(left, top, "");                       //(도서명/출판사/저자)를 입력 받음

                if (searchWord.Equals(Constants.ESC))   //검색어 입력도중 ESC -> 뒤로가기
                {
                    exception.RemoveLine(left, top);
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(searchWord, Constants.BOOK_NAME_REGEX) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않은 입력
                {
                    exception.PrintBookNameRegex((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.SECOND + 1);
                }
                else break;
            }

            return searchWord;  //검색어 반환
        }
        public bool IsBookInList(string bookId)
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].Id.Equals(bookId)) return Constants.IS_BOOK_IN_LIST;
            }
            return Constants.IS_BOOK_NOT_IN_LIST;
        }
        public string InputBookId()
        {
            int left = (int)Constants.InputField.LEFT;
            int top = (int)Constants.SearchMenu.FIRST;
            int exceptionLeft = (int)Constants.SearchMenu.LEFT;
            int exceptionTop = (int)Constants.SearchMenu.SECOND + 1;
            string searchWord;

            while (Constants.INPUT_VALUE)
            {
                exception.RemoveLine(left, top);
                searchWord = text.EnterText(left, top, "");                       //(도서명/출판사/저자)를 입력 받음

                if (searchWord.Equals(Constants.ESC))   //검색어 입력도중 ESC -> 뒤로가기
                {
                    exception.RemoveLine(left, top);
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(searchWord, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않은 입력
                {
                    exception.PrintBookIdRegex(exceptionLeft, exceptionTop);
                }
                else if (IsBookInList(searchWord) == Constants.IS_BOOK_NOT_IN_LIST)
                {
                    exception.PrintBookNotInList(exceptionLeft, exceptionTop);
                }
                else break;
            }

            return searchWord;  //검색어 반환
        }
        public BookVO FindBook(string bookId)
        {
            int i;
            for (i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].Id.Equals(bookId)) break;
            }
            return bookList[i];
        }
        public void SearchBook(Keyboard keyboard, BookRegistration bookRegistration)
        {
            string searchWord;
            string bookId;
            string quantity;
            BookVO book;

            adminView.PrintNaverSearch();

            while (Constants.INPUT_VALUE)
            {
                bookList.Clear();
                searchWord = InputSearchWord();   //검색어 입력받기
                if (searchWord.Equals(Constants.ESC)) return;               //검색어 입력 중 esc -> 도서검색 종료

                MakeBookList(searchWord);                                  //도서목록
                if(bookList.Count == 0)                                    //검색결과가 없을 경우 -> 검색어 다시 입력받기
                {
                    adminView.PrintNoSearchResult(searchWord);
                    continue;
                }

                adminView.PrintNaverSearchResult(bookList);                //도서 검색 결과 출력
                bookId = InputBookId();  //추가할 도서번호 입력받기
                if (bookId.Equals(Constants.ESC))                          //도서번호 입력 중 Esc -> 도서검색으로 돌아감
                {
                    adminView.PrintNaverSearch();
                    continue;
                }
                break;
            }

            book = FindBook(bookId);
            adminView.PrintBookRegistration(book);   //도서 등록 화면

            bookId = bookRegistration.InputBookId((int)Constants.InputField.REGISTRATION, (int)Constants.EditMenu.FIRST);   //도서번호 입력
            if (bookId.Equals(Constants.ESC)) return;
            quantity = bookRegistration.InputBookName((int)Constants.InputField.REGISTRATION, (int)Constants.EditMenu.SIXTH, Constants.QUENTITY_REGEX);
            if (quantity.Equals(Constants.ESC)) return;

            book.Id = bookId;
            book.Quantity = quantity;
            BookDAO.bookDAO.AddToBookList(Constants.ADDITION_TO_BOOK_LIST, book);  //DB에 도서정보 저장

            adminView.PrintRegisteredBook(book);              //등록 완료 화면 출력
            keyboard.PressESC();                              //Esc -> 종료(뒤로가기)
            Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
        }
    }
}
