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
        public void PrintSearchingBook(string bookName)
        {
            Console.Clear();
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintSearchBox("\n☞삭제할 도서번호 입력:");

            SearchingScreen searchingScreen = new SearchingScreen();
            searchingScreen.PrintSearchingBook(1, bookName, library.bookList);   //도서명 검색결과
        }
        public void PrintBookList()
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintSearchBox("\n☞삭제할 도서명 검색:");

            ListScreen listScreen = new ListScreen();
            listScreen.PrintBookList(library.bookList);
        }
        public void PrintSuccessMessage(int bookIndex)
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintMenu("도서삭제 완료");

            Console.WriteLine("\n=====================[삭제한 도서정보]======================\n");
            Console.WriteLine(library.bookList[bookIndex]);
            Console.WriteLine("\n=======================뒤로가기:[ESC]========================\n");
        }
    }
}
