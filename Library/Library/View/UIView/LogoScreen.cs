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

        public void PrintSearchBox(string searchWord)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 1);
            Console.WriteLine(searchWord);
        }
        public void RemoveLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("                                                                           ");
        }

        public void PrintMain(string[] menu)
        {
            Console.Clear();
            Console.SetWindowSize(61, 40);
            
            PrintTitle();
            for (int i = 0; i < menu.Length; i++)
            {
                Console.SetCursorPosition(25, 13 + i);
                Console.WriteLine("☞" + menu[i]);
            }
            Console.SetCursorPosition(25, 13);
        }
    }
}
