using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class DeletingBook
    {
        public string InputBookName()
        {
            string bookName;

            bookName = Console.ReadLine();

            return bookName;
        }
        public int ControlDeletingBook()
        {
            DeletingBookScreen deletingBookScreen = new DeletingBookScreen();
            deletingBookScreen.PrintBookList();      //책목록 출력

            Keyboard keyboard = new Keyboard(0, 1);
            int menu = keyboard.SelectMenu(1, 1, 0);
            if (menu == Constants.ESCAPE) return Constants.ESCAPE;   //뒤로가기 -> 관리자모드로 돌아감


            string bookName = InputBookName();

            deletingBookScreen.PrintSearchingBook(bookName);
            menu = keyboard.SelectMenu(1, 1, 0);
            if (menu == Constants.ESCAPE) return Constants.ESCAPE;   //뒤로가기 -> 관리자모드로 돌아감

            return Constants.COMPLETE_FUNCTION;
        }
    }
}
