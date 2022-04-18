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
        private BookVO bookVO;
        public void PrintInputBox(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }
        public bool IsBookInList(string bookId)
        {
            BookListVO bookListVO = BookListVO.GetBookListVO();   //도서목록

            for (int i = 0; i < bookListVO.bookList.Count; i++)
            {
                if (bookListVO.bookList[i].Id.Equals(bookId))
                {
                    bookVO = bookListVO.bookList[i]; //도서정보 저장   
                    return Constants.BOOK_IN_LIST;   //도서명,번호에 해당하는 책을 찾음
                }
            }
            return !Constants.BOOK_IN_LIST;
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
        public void InputBook(int menu)  //도서정보입력
        {
            RegisteringBook registeringBook = new RegisteringBook();
            string book;

            switch (menu)
            {
                case 16:   //도서번호
                    book = registeringBook.InputBookId(12, 16);
                    bookVO.Id = book;
                    break;
                case 19:   //도서명
                    book = registeringBook.InputBookName(10, 19);
                    bookVO.Name = book;
                    break;
                case 22:   //출판사
                    book = registeringBook.InputBookName(10, 22);
                    bookVO.Publisher = book;
                    break;
                case 25:   //저자
                    book = registeringBook.InputAuthor(8, 25);
                    bookVO.Author = book;
                    break;
                case 28:   //가격
                    book = registeringBook.InputPrice(8, 28);
                    bookVO.Price = book;
                    break;
                case 31:   //수량
                    book = registeringBook.InputQuantity(8, 31);
                    bookVO.Quantity = book;
                    break;
            }
        }
        public int ControlEditingBook()
        {
            EditingBookScreen editingBookScreen = new EditingBookScreen();
            editingBookScreen.PrintSearchingBookId();    //도서번호검색화면 출력

            InputBookId();  //도서번호를 입력받음

            editingBookScreen.PrintBook(bookVO);      //해당 도서정보 출력
            editingBookScreen.PrintQuantityManagement(); //도서정보 수정 화면 출력

            Keyboard keyboard = new Keyboard(0, 16);
            int menu;

            while (Constants.INPUT_VALUE)
            {
                menu = keyboard.SelectMenu(16, 31, 3);
                if (menu == Constants.ESCAPE) return Constants.ESCAPE;   //메뉴선택 중 뒤로가기를 누르면 종료
                InputBook(keyboard.Top);                   //해당 도서정보 수정
                editingBookScreen.PrintSuccessMessage(bookVO);     //수정된 정보가 반영된 화면 출력
            }
        }
    }
}
