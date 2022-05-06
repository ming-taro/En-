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
        private BookDAO bookDAO = BookDAO.GetInstance();
        private LogDAO logDAO = LogDAO.GetInstance();
        private EnteringText text;
        private AdminView adminView;
        private Exception exception;
        private Logo logo;
        private List<BookDTO> bookList;
        private string searchWord;
        private string numberOfBook;
        public NaverSearch(EnteringText text, AdminView adminView, Exception exception, Logo logo)
        {
            this.text = text;
            this.adminView = adminView;
            this.exception = exception;
            this.logo = logo;
            bookList = new List<BookDTO>();
        }
        private void CreateBookList()
        {
            int count;
            string text;
            string url;
            string publicationDate;

            url = string.Format(Constants.API_URL, searchWord, numberOfBook);            //네이버 도서검색
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
                publicationDate = searchResult["items"][i]["pubdate"].ToString();
                publicationDate = publicationDate.Insert(6, ".").Insert(4, ".");

                bookList.Add(new BookDTO((i + 1).ToString(), searchResult["items"][i]["title"].ToString(), searchResult["items"][i]["author"].ToString(), searchResult["items"][i]["publisher"].ToString(), publicationDate,
                    searchResult["items"][i]["isbn"].ToString(), searchResult["items"][i]["price"].ToString(), searchResult["items"][i]["description"].ToString(), ""));
            }
        }
        private void InputSearchWord()
        {
            int left = (int)Constants.InputField.NAVER_SEARCH;
            int top = (int)Constants.SearchMenu.FIRST;

            while (Constants.INPUT_VALUE)
            {
                exception.RemoveLine(left, top);
                searchWord = text.EnterText(left, top, "");                                          //(도서명/출판사/저자)를 입력 받음

                if (searchWord.Equals(Constants.ESC))
                {
                    logo.RemoveLine(left, top);      //검색어 입력 중 Esc -> 입력값 지움
                    searchWord = "";
                }
                else if (Regex.IsMatch(searchWord, Constants.BOOK_NAME_REGEX) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않은 입력
                {
                    exception.PrintBookNameRegex(left, top + 1);
                    continue;
                }
                
                break;    //검색어 입력 중 Esc or 검색어 입력 -> while문 종료
            }
            logo.RemoveLine(left, top + 1);   //예외처리 문구 지움
        }
        private void InputNumberOfBook()
        {
            int left = (int)Constants.InputField.NAVER_SEARCH;
            int top = (int)Constants.SearchMenu.SECOND;

            while (Constants.INPUT_VALUE)
            {
                logo.RemoveLine(left, top);

                numberOfBook = text.EnterText(left, top, "");

                if (numberOfBook.Equals(Constants.ESC))
                {
                    logo.RemoveLine(left, top);      //수량 입력 중 Esc -> 입력값 지움
                    numberOfBook = "";
                }
                else if (Regex.IsMatch(numberOfBook, Constants.DISPLAY_REGEX) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않은 입력
                {
                    exception.PrintNumberOfBook(left, top + 1);   //예외메세지 + 검색어를 다시 입력받음
                    continue;
                }

                break;    //입력 중 Esc or 수량 입력시 while문 종료
            }
            logo.RemoveLine(left, top + 1);   //예외처리 문구 지움
        }
        public void InquireAboutBook()
        {
            if (searchWord == null || searchWord == "")
            {
                logo.PrintMessage((int)Constants.InputField.NAVER_SEARCH, (int)Constants.SearchMenu.FIRST + 1, "(검색어를 입력해주세요.)                 ", ConsoleColor.Red);                  //검색어를 입력하지 않은 경우
                searchWord = "";
            }
            if (numberOfBook == null ||  numberOfBook == "")
            {
                logo.PrintMessage((int)Constants.InputField.NAVER_SEARCH, (int)Constants.SearchMenu.SECOND + 1, "(1~100 이내의 숫자를 입력해주세요.)                 ", ConsoleColor.Red);      //조회수량을 입력하지 않음 경우
            }
            if (searchWord != null && numberOfBook != null && searchWord != "" && numberOfBook != "")
            {
                CreateBookList();   //검색 결과 도서목록 생성
            }
        }
        public void SelectMenu(int top)
        {
            switch (top)
            {
                case (int)Constants.SearchMenu.FIRST:     //검색어 입력
                    InputSearchWord();
                    break;
                case (int)Constants.SearchMenu.SECOND:    //책 권 수 입력
                    InputNumberOfBook();
                    break;
                case (int)Constants.SearchMenu.THIRD:     //조회
                    InquireAboutBook();
                    break;
            }
        }
        private bool IsBookInList(string bookId)
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].Id.Equals(bookId)) return Constants.IS_BOOK_IN_LIST;
            }
            return Constants.IS_BOOK_NOT_IN_LIST;
        }
        private string InputBookId()
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
                    exception.PrintBookNotInList(exceptionLeft, exceptionTop);/////////----->모듈화로빼기(다)
                }
                else break;
            }

            return searchWord;  //검색어 반환
        }
        private BookDTO FindBook(string bookId)
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
            string bookId;
            string quantity;
            int menu;
            BookDTO book;

            adminView.PrintNaverSearch();
            
            while (Constants.INPUT_VALUE)
            {
                keyboard.SetPosition((int)Constants.SearchMenu.LEFT, (int)Constants.SearchMenu.FIRST);
                menu = keyboard.SelectMenu((int)Constants.SearchMenu.FIRST, (int)Constants.SearchMenu.THIRD, (int)Constants.SearchMenu.STEP);
                if (menu == (int)Constants.Keyboard.ESCAPE) return;   //검색어 or 수량 or 조회 선택 중 Esc -> 네이버 검색 종료

                SelectMenu(keyboard.Top);                             //검색어, 조회 수량 입력, 조회 가능 여부 판단
                if (keyboard.Top != (int)Constants.SearchMenu.THIRD) continue;    //메뉴선택에서 검색어 or 조회수량 입력을 선택했다면 다시 돌아가서 메뉴를 선택함

                if (searchWord == "" || searchWord == null || numberOfBook == "" || numberOfBook == null) continue;//검색어나 수량을 입력하지 않고 검색했을 경우
                if (bookList.Count == 0)                             //검색결과가 없을 경우 -> 검색어 다시 입력받기
                {
                    adminView.PrintNoSearchResult(searchWord);
                    continue;
                }

                logDAO.SearchBookOnNaver("12345", searchWord);      //관리자 검색기록 로그에 저장
                adminView.PrintNaverSearchResult(bookList);         //도서 검색 결과 출력

                bookId = InputBookId();                             //추가할 도서번호 입력받기
                if (bookId.Equals(Constants.ESC))                   //도서번호 입력 중 Esc -> 도서검색으로 돌아감
                {
                    adminView.PrintNaverSearch();
                    bookList.Clear();                 //이전 검색어로 만들어진 도서목록, 검색어, 조회할 수량 초기화
                    searchWord = "";
                    numberOfBook = "";
                    continue;
                }
                break;
            }

            book = FindBook(bookId);
            adminView.PrintBookRegistration(book);   //도서 등록 화면

            quantity = bookRegistration.InputBookName((int)Constants.InputField.REGISTRATION, (int)Constants.EditMenu.EIGHT, Constants.QUENTITY_REGEX);
            if (quantity.Equals(Constants.ESC)) return;

            book.Quantity = quantity;
            bookDAO.AddToBookList(Constants.ADDITION_TO_BOOK_LIST, book);  //DB에 도서정보 저장
            logDAO.RegisterBook(book.Name);                                //log에 도서등록 기록

            adminView.PrintRegisteredBook(book);              //등록 완료 화면 출력
            keyboard.PressESC();                              //Esc -> 종료(뒤로가기)
            Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
        }
    }
}
