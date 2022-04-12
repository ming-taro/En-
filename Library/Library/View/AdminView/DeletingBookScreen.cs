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
            logoScreen.PrintSearchBox("\n☞삭제할 도서번호 입력");

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
    }
}
