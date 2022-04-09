using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LogoScreen
    {
        public void PrintTitle()
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine("┃                                                         ┃");
            Console.WriteLine("┃    **          **                                       ┃");
            Console.WriteLine("┃    **          **                                       ┃");
            Console.WriteLine("┃    **      ★  ****   *  **   **     *  **    *    **   ┃");
            Console.WriteLine("┃    **      **  **☆*  **     *☆*    **       *  **     ┃");
            Console.WriteLine("┃    ******  **  ****   **      **  *  **       **        ┃");
            Console.WriteLine("┃                                             **          ┃");
            Console.WriteLine("┃                                           **            ┃ ");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
            Console.WriteLine("     >>메뉴를 방향키로 이동하고 [Enter]키를 눌러주세요<<");
        }
        public void PrintMenu(string menu)
        {
            Console.Clear();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("[" + menu + "]\n");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
        }
    }
}
