using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Screen
    {
        public void PrintTitle()
        {
            Console.WriteLine("\n  ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine("  ┃                                                         ┃");
            Console.WriteLine("  ┃    **          **                                       ┃");
            Console.WriteLine("  ┃    **          **                                       ┃");
            Console.WriteLine("  ┃    **      ★  ****   *  **   **     *  **    *    **   ┃");
            Console.WriteLine("  ┃    **      **  **☆*  **     *☆*    **       *  **     ┃");
            Console.WriteLine("  ┃    ******  **  ****   **      **  *  **       **        ┃");
            Console.WriteLine("  ┃                                             **          ┃");
            Console.WriteLine("  ┃                                           **            ┃ ");
            Console.WriteLine("  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
        }
        public void PrintMenu(string menu)
        {
            Console.Clear();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("[" + menu + "]\n");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
        }
        //string
        //메인화면 -> (회원,관리자,종료)
        //회원메뉴 -> (회원가입,로그인,종료)
        //관리자모드 -> (도서검색,도서등록,도서수량관리,도서삭제,회원정보관리,종료)
        //회원모드 -> (도서검색,도서대여,도서반납,개인정보수정,종료)
        public void PrintMain(string[] menu)
        {
            Console.Clear();
            PrintTitle();
            for(int i=0; i < menu.Length; i++)
            {
                Console.SetCursorPosition(25, 13 + i);
                Console.WriteLine("☞" + menu[i]);
            }
            Console.SetCursorPosition(25,13);
        }
        public void PrintSingUp(string menu)
        {
            PrintMenu(menu);
            Console.WriteLine("아이디: ");
            Console.Write("비밀번호: ");
            Console.SetCursorPosition(8, 5);
            Console.ReadLine();
            Console.SetCursorPosition(10, 6);
            Console.ReadLine();
        }
        
    }
}
