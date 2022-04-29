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

        private BookVO bookVO;
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
                bookId = text.EnterText(26, 1, "");       //도서번호를 입력받음

                if (bookId.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (Regex.IsMatch(bookId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH)  //입력형식에 맞지 않은 입력
                {
                    logo.PrintMessage(0, 2, Constants.MESSAGE_ABOUT_BOOK_ID_NOT_MATCH, ConsoleColor.Red);
                }
                else if (IsBookInList(bookId, bookList) == Constants.IS_BOOK_NOT_IN_LIST)      //현재 조회목록에 없는 도서
                {
                    logo.PrintMessage(0, 2, Constants.MESSAGE_ABOUT_BOOK_NOT_IN_LIST, ConsoleColor.Red);
                }
                else break;   //도서삭제 가능

                logo.RemoveLine(26, 1);
            }

            return bookId;
        }
        public void InputBook(int menu, BookVO book, RegisteringBook registeringBook)  //도서정보입력
        {
            string changedItem;

            switch (menu)
            {
                case (int)Constants.Registration.FIRST:   //도서번호
                    changedItem = registeringBook.InputBookId(12, (int)Constants.Registration.FIRST);
                    bookVO.Id = changedItem;
                    break;
                case (int)Constants.Registration.SECOND:   //도서명
                    changedItem = registeringBook.InputBookName(10, (int)Constants.Registration.SECOND, Constants.BOOK_NAME_REGEX, Constants.MESSAGE_ABOUT_BOOK_NAME);
                    bookVO.Name = changedItem;
                    break;
                case (int)Constants.Registration.THIRD:   //출판사
                    changedItem = registeringBook.InputBookName(10, (int)Constants.Registration.THIRD, Constants.PUBLISHER_REGEX, Constants.MESSAGE_ABOUT_PUBLISHER);
                    bookVO.Publisher = changedItem;
                    break;
                case (int)Constants.Registration.FOURTH:   //저자
                    changedItem = registeringBook.InputBookName(8, (int)Constants.Registration.FOURTH, Constants.AUTHOR_REGEX, Constants.MESSAGE_ABOUT_AUTHOR);
                    bookVO.Author = changedItem;
                    break;
                case (int)Constants.Registration.FIFTH:   //가격
                    changedItem = registeringBook.InputBookName(8, (int)Constants.Registration.FIFTH, Constants.PRICE_REGEX, Constants.MESSAGE_ABOUT_PRICE);
                    bookVO.Price = changedItem;
                    break;
                case (int)Constants.Registration.SIXTH:   //수량
                    changedItem = registeringBook.InputBookName(8, (int)Constants.Registration.SIXTH, Constants.QUENTITY_REGEX, Constants.MESSAGE_ABOUT_QUENTITY);
                    bookVO.Quantity = changedItem;
                    break;
            }
        }
        public void EditBook(SearchingBook searchingBook, RegisteringBook registeringBook, Keyboard keyboard)
        {
            int menu;
            int searchType;
            string searchWord;
            string bookId;
            BookVO book;

            keyboard.SetPosition(0, (int)Constants.Registration.FIRST);  //커서 위치 조정

            while (Constants.INPUT_VALUE)
            {
                searchType = searchingBook.SelectSearchType(keyboard);     //검색유형 선택
                if (searchType == (int)Constants.Keyboard.ESCAPE) break;   //검색유형 선택 중 Esc -> 도서검색 종료

                searchWord = searchingBook.InputSearchWord((int)Constants.SearchMenu.LEFT_VALUE_OF_INPUT, searchType, (int)Constants.SearchMenu.FOURTH);   //검색어 입력받기
                if (searchWord.Equals(Constants.ESC)) continue;            //검색어 입력 중 Esc -> 검색유형 선택으로 돌아감

                adminView.PrintBookIdInputScreen(searchingBook.BookList, "☞정보를 수정할 도서번호:");  //도서번호 입력창
                
                bookId = InputBookId(searchingBook.BookList);  //도서번호를 입력받음
                if (bookId.Equals(Constants.ESC)) continue;    //도서번호 입력 중 Esc -> 검색유형 선택으로 돌아감

                adminView.PrintBookInputScreen("도서 정보 수정");  //도서 정보 수정 화면 출력

                keyboard.SetPosition(0, (int)Constants.Registration.FIRST);
                menu = keyboard.SelectMenu((int)Constants.Registration.FIRST, (int)Constants.Registration.SIXTH, (int)Constants.Registration.STEP);  //수정할 항목 선택
                if (menu == Constants.ESCAPE) continue;        //메뉴선택 중 Esc -> 검색유형 선택으로 돌아감
                menu = keyboard.Top;

                InputBook(menu, book, registeringBook);                   //해당 도서정보 수정
                //editingBookScreen.PrintSuccessMessage(bookVO);     //수정된 정보가 반영된 화면 출력
            }
        }
    }
}
