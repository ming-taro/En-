using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class DeletingBookScreen
    {
        public void PrintSearchingBook(string bookName)
        {
            Console.Clear();
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintSearchBox("\n☞삭제할 도서번호 입력:");

            BookListVO bookListVO = BookListVO.GetBookListVO();
            SearchingScreen searchingScreen = new SearchingScreen();
            searchingScreen.PrintSearchingBook(1, bookName, bookListVO.bookList);   //도서명 검색결과
        }
        public void PrintBookList()
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintSearchBox("\n☞삭제할 도서명 검색:");

            BookListVO bookListVO = BookListVO.GetBookListVO();
            ListScreen listScreen = new ListScreen();
            listScreen.PrintBookList(bookListVO.bookList);
        }
        public void PrintSuccessMessage(int bookIndex)
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintMenu("도서삭제 완료");

            BookListVO bookListVO = BookListVO.GetBookListVO();
            Console.WriteLine("\n=====================[삭제한 도서정보]======================\n");
            Console.WriteLine(bookListVO.bookList[bookIndex]);
            Console.WriteLine("\n=======================뒤로가기:[ESC]========================\n");
        }
    }
}
