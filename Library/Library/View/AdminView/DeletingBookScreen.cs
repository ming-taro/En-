using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class DeletingBookScreen
    {
        private LibraryVO library;
        public DeletingBookScreen()
        {
            library = LibraryVO.GetLibraryVO();
        }
        public void PrintBookList()
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintSearchBox("\n☞삭제할 도서명 검색:");

            ListScreen listScreen = new ListScreen();
            listScreen.PrintBookList(library.bookList);
        }
        public void PrintSuccessMessage(string bookId, LibraryVO library)
        {
            AdminMenu searchingScreen = new AdminMenu();
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintMenu("도서삭제 완료");

            Console.WriteLine("\n                       [삭제한 도서정보]\n");
            searchingScreen.PrintSearchingBook("select*from book where id='" + bookId + "'", library);
        }
    }
}
