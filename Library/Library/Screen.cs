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
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            Console.SetCursorPosition(20, 2);
            Console.WriteLine("[" + menu + "]");
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
            //ConsoleKeyInfo keyInfo = Console.ReadKey();
            //if(keyInfo.Key == ConsoleKey.A) Console.Write("AAAAAAAa!\n");
        }
        public void PrintSingUp(string menu)
        {
            
            PrintMenu(menu);
            Console.Write("아이디 : ");
            Console.ReadLine();
            Console.Write("비밀번호 : ");
            Console.ReadLine();
        }
        
    }
}
