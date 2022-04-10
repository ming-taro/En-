using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class ReturningScreen
    {
        public void PrintReturning(List<BookVO> myBorrowList)
        {
            Console.Clear();
            Console.WriteLine("\n☞반납할 도서 번호: ");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<\n");

            for(int i=0; i<myBorrowList.Count; i++)
            {
                Console.WriteLine(myBorrowList[i]);  //회원의 대여 도서 목록 출력
                Console.WriteLine("\n=============================================================\n");
            }

        }
        public void PrintErrorMessage(string message)
        {
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("☞반납할 도서 번호:                                                    ");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(message);
        }

        public void PrintSuccessMessage(List<BookVO> bookList)
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintMenu("도서반납 완료");

            ListScreen listScreen = new ListScreen();
            listScreen.PrintBookList(bookList);
        }
    }
}
