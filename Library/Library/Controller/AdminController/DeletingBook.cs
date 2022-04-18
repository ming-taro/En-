using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class DeletingBook
    {
        private LibraryVO library;
        int bookIndex;
        public DeletingBook()
        {
            library = LibraryVO.GetLibraryVO();
        }

        public void PrintInputBox(int left, int top, string message)///---->뷰로빼기(겹침)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }
        public bool IsBorrowedBook(string bookId)
        {
            for(int i=0; i<library.borrowList.Count; i++)
            {
                if (library.borrowList[i].BookVO.Id.Equals(bookId))   //해당 도서를 회원이 대여중인 경우 -> 도서삭제 불가
                {
                    return Constants.BOOK_I_BORROWED;
                }
            }
            return !Constants.BOOK_I_BORROWED;  //도서를 대여중인 회원이 없는 경우 -> 도서 대여 가능
        }
        public bool IsBookInList(string bookName, string bookId)
        {
            for(int i=0; i<library.bookList.Count; i++)
            {
                if(library.bookList[i].Name.Contains(bookName) && library.bookList[i].Id.Equals(bookId))
                {
                    bookIndex = i;   //도서 삭제할때 사용
                    return Constants.BOOK_IN_LIST;   //도서명,번호에 해당하는 책을 찾음
                }
            }
            return !Constants.BOOK_IN_LIST;
        }
        public void InputBookId(string bookName)
        {
            string bookId;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(24, 1);
                bookId = Console.ReadLine();       //도서번호를 입력받음
                if(string.IsNullOrEmpty(bookId) || !Regex.IsMatch(bookId, @"^[0-9]{1,3}$") || !IsBookInList(bookName, bookId))
                {
                    PrintInputBox(0, 2, "(현재 조회 목록에 없는 도서번호입니다.다시 입력해주세요.)          ");
                }
                else if (IsBorrowedBook(bookId))   //도서목록에 있지만 대여중인 회원이 있는 경우 -> 도서 대여 불가
                {
                    PrintInputBox(0, 2, "(회원이 대여중인 도서는 삭제가 불가능합니다.)                 ");
                }
                else break;   //도서삭제 가능
                PrintInputBox(24, 1, Constants.REMOVE_LINE);
            }
        }
        public string InputBookName()
        {
            string bookName;

            SearchingBook searchingBook = new SearchingBook();
            bookName = searchingBook.InputSearchWord(22, 1, 2);    //도서명을 입력받음

            return bookName;
        }
        public int ControlDeletingBook()
        {
            DeletingBookScreen deletingBookScreen = new DeletingBookScreen();
            deletingBookScreen.PrintBookList();      //책목록 출력

            Keyboard keyboard = new Keyboard(0, 1);
            int menu = keyboard.SelectMenu(1, 1, 0);
            if (menu == Constants.ESCAPE) return Constants.ESCAPE;   //뒤로가기 -> 관리자모드로 돌아감

            string bookName = InputBookName();                       //도서명을 입력받음
            deletingBookScreen.PrintSearchingBook(bookName);         //입력받은 도서명 검색결과 출력

            menu = keyboard.SelectMenu(1, 1, 0);
            if (menu == Constants.ESCAPE) return Constants.ESCAPE;   //뒤로가기 -> 관리자모드로 돌아감
            InputBookId(bookName);

            deletingBookScreen.PrintSuccessMessage(bookIndex);   //도서삭제 완료 메세지 출력
            library.bookList.RemoveAt(bookIndex);                //리스트에서 도서삭제

            return Constants.COMPLETE_FUNCTION;
        }
    }
}
