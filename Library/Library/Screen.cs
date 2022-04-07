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
        public void PrintMain()
        {
            PrintTitle();

            Console.SetCursorPosition(25, 15);
            Console.WriteLine("☞회원");
            Console.SetCursorPosition(25, 16);
            Console.WriteLine("☞관리자");
            Console.SetCursorPosition(25, 17);
            Console.WriteLine("☞종료");

            //ConsoleKeyInfo keyInfo = Console.ReadKey();


            //if(keyInfo.Key == ConsoleKey.A) Console.Write("AAAAAAAa!\n");
        }
    }
}
