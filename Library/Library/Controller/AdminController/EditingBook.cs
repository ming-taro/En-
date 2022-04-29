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
        public void PrintInputBox(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }
        public bool IsBookInList(string bookId)
        {
            LibraryVO library = LibraryVO.GetLibraryVO();   //도서목록

            for (int i = 0; i < library.bookList.Count; i++)
            {
                if (library.bookList[i].Id.Equals(bookId))
                {
                    bookVO = library.bookList[i]; //도서정보 저장   
                    return Constants.IS_BOOK_IN_LIST;   //도서명,번호에 해당하는 책을 찾음
                }
            }
            return !Constants.IS_BOOK_IN_LIST;
        }
        public void InputBookId()
        {
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(26, 1);
                bookId = Console.ReadLine();       //도서번호를 입력받음
                if (string.IsNullOrEmpty(bookId) || !Regex.IsMatch(bookId, @"^[0-9]{1,3}$") || !IsBookInList(bookId))
                {
                    PrintInputBox(0, 2, "(존재하지 않는 도서번호입니다.다시 입력해주세요.)");
                }
                else break;   //도서삭제 가능
                PrintInputBox(26, 1, Constants.REMOVE_LINE);
            }
        }
        public void InputBook(int menu, RegisteringBook registeringBook)  //도서정보입력
        {
            string book;

            switch (menu)
            {
                case 16:   //도서번호
                    book = registeringBook.InputBookId(12, 16);
                    bookVO.Id = book;
                    break;
                case 19:   //도서명
                    book = registeringBook.InputBookName(10, 19, Constants.BOOK_NAME_REGEX, Constants.MESSAGE_ABOUT_BOOK_NAME);
                    bookVO.Name = book;
                    break;
                case 22:   //출판사
                    book = registeringBook.InputBookName(10, 22, Constants.PUBLISHER_REGEX, Constants.MESSAGE_ABOUT_PUBLISHER);
                    bookVO.Publisher = book;
                    break;
                case 25:   //저자
                    book = registeringBook.InputBookName(8, 25, Constants.AUTHOR_REGEX, Constants.MESSAGE_ABOUT_AUTHOR);
                    bookVO.Author = book;
                    break;
                case 28:   //가격
                    book = registeringBook.InputBookName(8, 28, Constants.PRICE_REGEX, Constants.MESSAGE_ABOUT_PRICE);
                    bookVO.Price = book;
                    break;
                case 31:   //수량
                    book = registeringBook.InputBookName(8, 31, Constants.QUENTITY_REGEX, Constants.MESSAGE_ABOUT_QUENTITY);
                    bookVO.Quantity = book;
                    break;
            }
        }
        public int ControlEditingBook(RegisteringBook registeringBook, Keyboard keyboard)
        {
            EditingBookScreen editingBookScreen = new EditingBookScreen();
            editingBookScreen.PrintSearchingBookId();    //도서번호검색화면 출력

            InputBookId();  //도서번호를 입력받음

            editingBookScreen.PrintBook(bookVO);      //해당 도서정보 출력
            editingBookScreen.PrintQuantityManagement(); //도서정보 수정 화면 출력

            //Keyboard keyboard = new Keyboard(0, 16);
            int menu;

            while (Constants.INPUT_VALUE)
            {
                menu = keyboard.SelectMenu(16, 31, 3);
                if (menu == Constants.ESCAPE) return Constants.ESCAPE;   //메뉴선택 중 뒤로가기를 누르면 종료
                InputBook(keyboard.Top, registeringBook);                   //해당 도서정보 수정
                editingBookScreen.PrintSuccessMessage(bookVO);     //수정된 정보가 반영된 화면 출력
            }
        }
    }
}
