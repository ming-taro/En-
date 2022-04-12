using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class QuantityScreen
    {
        public void PrintQuantity()
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintSearchBox("\n☞정보를 수정할 도서번호:");
            BookListVO bookListVO = BookListVO.GetBookListVO();
            ListScreen listScreen = new ListScreen();
            listScreen.PrintBookList(bookListVO.bookList);
        }

        public void PrintBook(int bookIndex)
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintMenu("도서정보 수정");
            Console.WriteLine("=======================뒤로가기:[ESC]========================\n");
            BookListVO bookListVO = BookListVO.GetBookListVO();
            Console.WriteLine(bookListVO.bookList[bookIndex]);  //해당 도서정보 출력
            Console.WriteLine("\n=============================================================\n");


        }
    }
}
