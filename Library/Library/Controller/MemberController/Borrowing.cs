using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Borrowing
    {
        public Borrowing()
        {
        }
        
        public void ControlBorrowingBook()
        {
            Screen screen = new Screen();
            //screen.PrintBorrowingBook(bookList, "\n☞대여할 도서 번호: ");  //대여한 도서 목록 출력
            Console.SetCursorPosition(20, 1);
            string bookId = Console.ReadLine();     //반납할 도서 번호 입력받기

        }
    }
}
