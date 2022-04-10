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
            BorrowingScreen borrowingScreen = new BorrowingScreen();
            borrowingScreen.PrintBorrowing();
        }
        public void InputBookName()
        {
            Console.Read();
            //Regex regex = new Regex(@"");
        }
        public int ControlBorrowing(string memberId)
        {
            Console.SetCursorPosition(20, 1);
            string bookId = Console.ReadLine();     //도서명으로 검색하기


            return Constants.COMPLETE_FUNCTION;
        }
    }
}
