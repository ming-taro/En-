using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class EditingBook
    {
        private BookDAO bookDatabaseManager;
        private EnteringText text;
        private AdminView adminView;
        private Logo logo;
        public EditingBook(BookDAO bookDatabaseManager)
        {
            this.bookDatabaseManager = bookDatabaseManager;
            text = new EnteringText();
            adminView = new AdminView();
            logo = new Logo();
        }
        public bool IsBookInList(string bookId, List<BookVO> bookList)
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].Id.Equals(bookId)) return Constants.IS_BOOK_IN_LIST;   //현재 조회목록에 있는 도서를 입력함
            }
            return Constants.IS_BOOK_NOT_IN_LIST;
        }
        public string InputBookId(List<BookVO> bookList)
        {
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                bookId = text.EnterText(26, (int)Constants.SearchMenu.FIRST, "");       //도서번호를 입력받음

                if (bookId.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않은 입력
                {
                    logo.PrintMessage(0, (int)Constants.SearchMenu.SECOND, Constants.MESSAGE_ABOUT_BOOK_ID_NOT_MATCH, ConsoleColor.Red);
                }
                else if (IsBookInList(bookId, bookList) == Constants.IS_BOOK_NOT_IN_LIST)      //현재 조회목록에 없는 도서
                {
                    logo.PrintMessage(0, (int)Constants.SearchMenu.SECOND, Constants.MESSAGE_ABOUT_BOOK_NOT_IN_LIST, ConsoleColor.Red);
                }
                else break;   //도서삭제 가능

                logo.RemoveLine(26, (int)Constants.SearchMenu.FIRST);
            }

            return bookId;
        }
        public string SelectBookId(SearchingBook searchingBook, Keyboard keyboard)  //수정할 도서번호 입력
        {
            int searchType;
            string searchWord;
            string bookId;

            keyboard.SetPosition(0, (int)Constants.Registration.FIRST);  //커서 위치 조정

            while (Constants.INPUT_VALUE)
            {
                searchType = searchingBook.SelectSearchType(keyboard);     //검색유형 선택
                if (searchType == (int)Constants.Keyboard.ESCAPE) return Constants.ESC;   //검색유형 선택 중 Esc -> 도서검색 종료

                searchWord = searchingBook.InputSearchWord((int)Constants.SearchMenu.LEFT_VALUE_OF_INPUT, searchType, (int)Constants.SearchMenu.FOURTH);   //검색어 입력받기
                if (searchWord.Equals(Constants.ESC)) continue;            //검색어 입력 중 Esc -> 검색유형 선택으로 돌아감

                adminView.PrintBookIdInputScreen("도서 정보 수정", "☞정보를 수정할 도서번호:", searchingBook.BookList);  //도서번호 입력창

                bookId = InputBookId(searchingBook.BookList);  //도서번호를 입력받음
                if (bookId.Equals(Constants.ESC)) continue;    //도서번호 입력 중 Esc -> 검색유형 선택으로 돌아감

                return bookId;
            }
        }
        public void ReflectChangeInVO(int menu, string changedItem, BookVO book)  //수정된 항목 VO에 반영
        {
            switch (menu)
            {
                case (int)Constants.EditMenu.FIRST:   //도서번호
                    bookDatabaseManager.UpdateOnBookId(changedItem, book.Id); //DB에 변경된 도서번호 반영(도서번호가 기본키이므로 즉시 DB에 반영해야함)
                    book.Id = changedItem;
                    break;
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
        public string InputBookInformation(int menu, RegisteringBook registeringBook)  //수정할 도서정보입력
        {
            string changedItem = "";

            switch (menu)
            {
                case (int)Constants.EditMenu.FIRST:   //도서번호
                    changedItem = registeringBook.InputBookId(12, (int)Constants.EditMenu.FIRST);
                    break;
                case (int)Constants.EditMenu.SECOND:   //도서명
                    changedItem = registeringBook.InputBookName(10, (int)Constants.EditMenu.SECOND, Constants.BOOK_NAME_REGEX, Constants.MESSAGE_ABOUT_BOOK_NAME);
                    break;
                case (int)Constants.EditMenu.THIRD:   //출판사
                    changedItem = registeringBook.InputBookName(10, (int)Constants.EditMenu.THIRD, Constants.PUBLISHER_REGEX, Constants.MESSAGE_ABOUT_PUBLISHER);
                    break;
                case (int)Constants.EditMenu.FOURTH:   //저자
                    changedItem = registeringBook.InputBookName(8, (int)Constants.EditMenu.FOURTH, Constants.AUTHOR_REGEX, Constants.MESSAGE_ABOUT_AUTHOR);
                    break;
                case (int)Constants.EditMenu.FIFTH:   //가격
                    changedItem = registeringBook.InputBookName(8, (int)Constants.EditMenu.FIFTH, Constants.PRICE_REGEX, Constants.MESSAGE_ABOUT_PRICE);
                    break;
                case (int)Constants.EditMenu.SIXTH:   //수량
                    changedItem = registeringBook.InputBookName(8, (int)Constants.EditMenu.SIXTH, Constants.QUENTITY_REGEX, Constants.MESSAGE_ABOUT_QUENTITY);
                    break;
            }

            return changedItem;
        }
        public BookVO FindBook(string bookId, List<BookVO> bookList)
        {
            int i;

            for (i = 0; i < bookList.Count; i++) 
            {
                if (bookList[i].Id.Equals(bookId)) break;
            }

            return bookList[i];
        }
        
        public void EditBook(SearchingBook searchingBook, RegisteringBook registeringBook, Keyboard keyboard)
        {
            int menu;
            string bookId;
            string changedItem;
            BookVO book;

            bookId = SelectBookId(searchingBook, keyboard);   //도서검색 후 수정할 도서번호 입력
            if (bookId.Equals(Constants.ESC)) return;         //Esc -> 뒤로가기

            book = FindBook(bookId, searchingBook.BookList);  //bookVO에 도서정보 저장

            while (Constants.INPUT_VALUE)
            {
                adminView.PrintBookRevision(book);            //도서 정보 수정 화면 출력
                keyboard.SetPosition(0, (int)Constants.EditMenu.FIRST);  //커서 위치 조정

                menu = keyboard.SelectMenu((int)Constants.EditMenu.FIRST, (int)Constants.EditMenu.SIXTH, (int)Constants.EditMenu.STEP);  //수정할 항목 선택
                if (menu == (int)Constants.Keyboard.ESCAPE) break;           //메뉴선택 중 Esc -> DB에 수정사항 반영 후 도서정보수정 종료
                menu = keyboard.Top;                                         //수정항목 == 커서의 top값

                changedItem = InputBookInformation(menu, registeringBook);   //해당 도서정보 수정
                if (changedItem.Equals(Constants.ESC)) continue;             //도서정보 입력 중 Esc -> 수정할 항목 다시 선택

                ReflectChangeInVO(menu, changedItem, book);  //bookVO에 수정사항 반영
            }

            bookDatabaseManager.AddToBookList(Constants.UPDATE_TO_BOOK_LIST, book); //DB에 수정사항 반영
        }
    }
}
