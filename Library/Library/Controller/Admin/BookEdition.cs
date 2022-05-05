using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class BookEdition
    {
        private BookDAO bookDAO = BookDAO.GetInstance();
        private LogDAO logDAO = LogDAO.GetInstance();
        private EnteringText text;
        private AdminView adminView;
        private Exception exception;
        public BookEdition(EnteringText text, AdminView adminView, Exception exception)
        {
            this.text = text;
            this.adminView = adminView;
            this.exception = exception;
        }
        private bool IsBookInList(string bookId, List<BookDTO> bookList)
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].Id.Equals(bookId)) return Constants.IS_BOOK_IN_LIST;   //현재 조회목록에 있는 도서를 입력함
            }
            return Constants.IS_BOOK_NOT_IN_LIST;
        }
        private string InputBookId(List<BookDTO> bookList)
        {
            int top = (int)Constants.SearchMenu.FIRST;
            int left = (int)Constants.InputField.ID_TO_MODIFY;
            int exceptionLeft = (int)Constants.SearchMenu.LEFT;
            int exceptionTop = (int)Constants.Exception.TOP;
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(left, top, "");       //도서번호를 입력받음

                if (bookId.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않은 입력
                {
                    exception.PrintBookIdRegex(exceptionLeft, exceptionTop);
                }
                else if (IsBookInList(bookId, bookList) == Constants.IS_BOOK_NOT_IN_LIST)      //현재 조회목록에 없는 도서
                {
                    exception.PrintBookNotInList(exceptionLeft, exceptionTop);
                }
                else break;   //도서삭제 가능

                exception.RemoveLine(left, top);
            }

            return bookId;
        }
        private string SelectBookId(BookSearch searchingBook, Keyboard keyboard)  //수정할 도서번호 입력
        {
            int searchType;
            string searchWord;
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                searchType = searchingBook.SelectSearchType(keyboard);     //검색유형 선택
                if (searchType == (int)Constants.Keyboard.ESCAPE) return Constants.ESC;   //검색유형 선택 중 Esc -> 도서검색 종료

                searchWord = searchingBook.InputSearchWord((int)Constants.InputField.SEARCH, searchType, (int)Constants.Exception.SEARCH);   //검색어 입력받기
                if (searchWord.Equals(Constants.ESC)) continue;            //검색어 입력 중 Esc -> 검색유형 선택으로 돌아감

                adminView.PrintBookEdition(searchingBook.BookList);  //도서번호 입력창

                bookId = InputBookId(searchingBook.BookList);  //도서번호를 입력받음
                if (bookId.Equals(Constants.ESC)) continue;    //도서번호 입력 중 Esc -> 검색유형 선택으로 돌아감

                return bookId;
            }
        }
        private void ReflectChangeInVO(int menu, string changedItem, BookDTO book)  //수정된 항목 VO에 반영
        {
            switch (menu)
            {
                case (int)Constants.EditMenu.SECOND:  //도서명
                    book.Name = changedItem;
                    break;
                case (int)Constants.EditMenu.THIRD:   //출판사
                    book.Publisher = changedItem;
                    break;
                case (int)Constants.EditMenu.FOURTH:  //저자
                    book.Author = changedItem;
                    break;
                case (int)Constants.EditMenu.FIFTH:   //가격
                    book.Price = changedItem; 
                    break;
                case (int)Constants.EditMenu.SIXTH:   //수량
                    book.Quantity = changedItem;
                    break;
            }
        }
        private string InputBookInformation(int menu, BookRegistration registeringBook)  //수정할 도서정보입력
        {
            int left = (int)Constants.InputField.BOOK_EDITION;
            string changedItem = "";

            switch (menu)
            {
                case (int)Constants.EditMenu.SECOND:   //도서명
                    changedItem = registeringBook.InputBookName(left, (int)Constants.EditMenu.SECOND, Constants.BOOK_NAME_REGEX);
                    break;
                case (int)Constants.EditMenu.THIRD:   //출판사
                    changedItem = registeringBook.InputBookName(left, (int)Constants.EditMenu.THIRD, Constants.PUBLISHER_REGEX);
                    break;
                case (int)Constants.EditMenu.FOURTH:   //저자
                    changedItem = registeringBook.InputBookName(left, (int)Constants.EditMenu.FOURTH, Constants.AUTHOR_REGEX);
                    break;
                case (int)Constants.EditMenu.FIFTH:   //가격
                    changedItem = registeringBook.InputBookName(left, (int)Constants.EditMenu.FIFTH, Constants.PRICE_REGEX);
                    break;
                case (int)Constants.EditMenu.SIXTH:   //수량
                    changedItem = registeringBook.InputBookName(left, (int)Constants.EditMenu.SIXTH, Constants.QUENTITY_REGEX);
                    break;
            }

            return changedItem;
        }
        private BookDTO FindBook(string bookId, List<BookDTO> bookList)
        {
            int i;

            for (i = 0; i < bookList.Count; i++) 
            {
                if (bookList[i].Id.Equals(bookId)) break;
            }

            return bookList[i];
        }
        
        public void EditBook(BookSearch bookSearch, BookRegistration bookRegistration, Keyboard keyboard)
        {
            int menu;
            string bookId;
            string changedItem;
            BookDTO book;

            bookId = SelectBookId(bookSearch, keyboard);   //도서검색 후 수정할 도서번호 입력
            if (bookId.Equals(Constants.ESC)) return;      //Esc -> 뒤로가기

            book = FindBook(bookId, bookSearch.BookList);  //bookVO에 도서정보 저장

            while (Constants.INPUT_VALUE)
            {
                adminView.PrintBookRevision(book);            //도서 정보 수정 화면 출력
                keyboard.SetPosition((int)Constants.EditMenu.LEFT, (int)Constants.EditMenu.SECOND);  //커서 위치 조정

                menu = keyboard.SelectMenu((int)Constants.EditMenu.SECOND, (int)Constants.EditMenu.SIXTH, (int)Constants.EditMenu.STEP);  //수정할 항목 선택
                if (menu == (int)Constants.Keyboard.ESCAPE) break;           //메뉴선택 중 Esc -> DB에 수정사항 반영 후 도서정보수정 종료
                menu = keyboard.Top;                                         //수정항목 == 커서의 top값

                changedItem = InputBookInformation(menu, bookRegistration);  //해당 도서정보 수정
                if (changedItem.Equals(Constants.ESC)) continue;             //도서정보 입력 중 Esc -> 수정할 항목 다시 선택

                ReflectChangeInVO(menu, changedItem, book);  //bookVO에 수정사항 반영
            }

            bookDAO.AddToBookList(Constants.UPDATE_TO_BOOK_LIST, book); //DB에 수정사항 반영
            logDAO.EditBook(book.Name);
        }
    }
}
