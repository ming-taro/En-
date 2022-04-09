using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class SearchingScreen
    {
        public void PrintSearchBox()  //검색모드 선택 화면
        {
            Console.Clear();
            Console.WriteLine("\n☞도서명: ");
            Console.WriteLine("☞출판사: ");
            Console.WriteLine("☞저자: ");
        }
        public void PrintSearchingBook(List<BookVO> bookList)  //도서검색화면 출력
        {
            PrintSearchBox();                  //검색화면
            ListScreen listscreen = new ListScreen();
            listscreen.PrintBookList(bookList);//도서목록화면
        }
        public void PrintSearchingBook(int menu, string name, List<BookVO> bookList)
        {
            Console.Clear();
            Console.WriteLine("\n=============================================================\n");
            for (int i = 0; i < bookList.Count; i++)
            {
                //1.도서명   2.출판사   3.저자
                if (menu == 1 && bookList[i].Name.Contains(name) || menu == 2 && bookList[i].Publisher.Contains(name) || menu == 3 && bookList[i].Author == name)
                {
                    Console.WriteLine(bookList[i]);
                    Console.WriteLine("\n=============================================================\n");
                }
            }
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<");
        }
    }
}
