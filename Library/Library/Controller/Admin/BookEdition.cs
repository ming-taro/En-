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
        private Logo logo;
        public BookEdition(EnteringText text, AdminView adminView, Logo logo)
        {
            this.text = text;
            this.adminView = adminView;
            this.logo = logo;
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
                else if (Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH || Int32.Parse(bookId) < 1 || Int32.Parse(bookId) > bookList.Count)  //입력형식에 맞지 않은 입력
                {
                    logo.PrintMessage(exceptionLeft, exceptionTop, "(현재 조회 목록에 없는 도서입니다. 다시 입력해주세요.)", ConsoleColor.Red);
                }
                else break;   //도서삭제 가능

                logo.RemoveLine(left, top);
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
                case (int)Constants.EditMenu.FIRST:  //도서명
                    book.Name = changedItem;
                    break;
                case (int)Constants.EditMenu.SECOND: //저자  
                    book.Author = changedItem;
                    break;
                case (int)Constants.EditMenu.THIRD:  //출판사
                    book.Publisher = changedItem;
                    break;
                case (int)Constants.EditMenu.FOURTH: //출판일
                    book.PublicationDate = changedItem; 
                    break;
                case (int)Constants.EditMenu.FIFTH:  //isbn
                    book.Isbn = changedItem;
                    break;
                case (int)Constants.EditMenu.SIXTH:  //가격
                    book.Price = changedItem;
                    break;
                case (int)Constants.EditMenu.SEVENTH://책소개
                    book.BookIntroduction = changedItem;
                    break;
                case (int)Constants.EditMenu.EIGHT:  //수량
                    book.Quantity = changedItem;
                    break;
            }
        }
        private void SaveChanges(string isbn, BookDTO book)  //변경된 정보 DB에 저장
        {
            bookDAO.AddToBookList(string.Format(Constants.UPDATE_TO_BOOK_LIST, isbn), book); //DB에 수정사항 반영
            logDAO.EditBook(book.Name);
        }
        private string InputBookInformation(int menu, ref string isbn, BookDTO book, BookRegistration registeringBook)  //수정할 도서정보입력
        {
            int left = (int)Constants.InputField.BOOK_EDITION;
            string changedItem = "";

            switch (menu)
            {
                case (int)Constants.EditMenu.FIRST:   //도서명
                    changedItem = registeringBook.InputBookName(left, (int)Constants.EditMenu.FIRST, Constants.BOOK_NAME_REGEX);
                    break;
                case (int)Constants.EditMenu.SECOND:    //저자
                    changedItem = registeringBook.InputBookName(left, (int)Constants.EditMenu.SECOND, Constants.AUTHOR_REGEX);
                    break;
                case (int)Constants.EditMenu.THIRD:     //출판사
                    changedItem = registeringBook.InputBookName(left, (int)Constants.EditMenu.THIRD, Constants.PUBLISHER_REGEX);
                    break; 
                case (int)Constants.EditMenu.FOURTH:    //출판일 
                    changedItem = registeringBook.InputBookName(left, (int)Constants.EditMenu.FOURTH, Constants.PUBLICATION_DATE_REGEX);//
                    break;
                case (int)Constants.EditMenu.FIFTH:     //isbn  
                    changedItem = registeringBook.InputBookName(left, (int)Constants.EditMenu.FIFTH, Constants.QUENTITY_REGEX);//
                    break;
                case (int)Constants.EditMenu.SIXTH:     //가격 
                    changedItem = registeringBook.InputBookName(left, (int)Constants.EditMenu.SIXTH, Constants.PRICE_REGEX);
                    break;
                case (int)Constants.EditMenu.SEVENTH:   //책소개
                    changedItem = registeringBook.InputBookName(left, (int)Constants.EditMenu.SEVENTH, Constants.QUENTITY_REGEX);//
                    break;
                case (int)Constants.EditMenu.EIGHT:     //수량
                    changedItem = registeringBook.InputBookName(left, (int)Constants.EditMenu.EIGHT, Constants.QUENTITY_REGEX);
                    break;
                case (int)Constants.EditMenu.NINTH:     //저장
                    SaveChanges(isbn, book);
                    isbn = book.Isbn;
                    break;
            }

            return changedItem;
        }
        public void EditBook(BookSearch bookSearch, BookRegistration bookRegistration, Keyboard keyboard)
        {
            int menu;
            string bookId;
            string changedItem;
            string isbn;
            BookDTO book;

            bookId = SelectBookId(bookSearch, keyboard);   //도서검색 후 수정할 도서번호 입력
            if (bookId.Equals(Constants.ESC)) return;      //Esc -> 뒤로가기

            book = bookSearch.BookList[Int32.Parse(bookId) - 1];  //bookVO에 도서정보 저장
            isbn = book.Isbn;

            while (Constants.INPUT_VALUE)
            {
                keyboard.SetPosition(0, (int)Constants.EditMenu.FIRST);  //커서 위치 조정
                adminView.PrintBookRevision(book);         //도서 정보 수정 화면 출력

                menu = keyboard.SelectMenu((int)Constants.EditMenu.FIRST, (int)Constants.EditMenu.NINTH, (int)Constants.EditMenu.STEP);  //수정할 항목 선택
                if (menu == (int)Constants.Keyboard.ESCAPE) break;           //메뉴선택 중 Esc -> DB에 수정사항 반영 후 도서정보수정 종료
                menu = keyboard.Top;                                         //수정항목 == 커서의 top값

                changedItem = InputBookInformation(menu, ref isbn, book, bookRegistration);  //해당 도서정보 수정
                if (changedItem.Equals(Constants.ESC)) continue;             //도서정보 입력 중 Esc -> 수정할 항목 다시 선택
                ReflectChangeInVO(menu, changedItem, book);  //bookVO에 수정사항 반영
            }
        }
    }
}
