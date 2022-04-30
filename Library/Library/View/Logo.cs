using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Logo
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
            Console.WriteLine(" [ESC]:뒤로가기                                [Enter]:선택");
        }
        public void PrintMenu(string menu)
        {
            int left = ((int)Constants.Menu.CONSOLE_WIDTH - (menu.Length * 2)) / 2;
            Console.Clear();
            Console.WriteLine("◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆");
            Console.SetCursorPosition(left, 2);
            Console.WriteLine("[" + menu + "]\n");
            Console.WriteLine("◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆◆");
        }
        public void PrintSearchBox(string menu, string searchWord)
        {
            PrintMenu(menu);
            Console.WriteLine(Constants.ESC_MESSAGE);
            Console.SetCursorPosition(0, (int)Constants.SearchMenu.FIRST);
            Console.WriteLine(searchWord);
            Console.Write("\n>>>>>>>>>>>>>>>>>>>>>>>> 도서 목록 <<<<<<<<<<<<<<<<<<<<<<<<<<");
        }
        public void PrintMessage(int left, int top, string message, ConsoleColor color)
        {
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void PrintLine()
        {
            Console.WriteLine("\n=============================================================\n");
        }
        public void RemoveLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("                                                                           ");
        }
        public void PrintMain(string[] menu)
        {
            Console.Clear();
            Console.SetWindowSize((int)Constants.Menu.CONSOLE_WIDTH, (int)Constants.Menu.CONSOLE_HEIGHT);

            PrintTitle();
            for (int i = 0; i < menu.Length; i++)
            {
                PrintMessage((int)Constants.Menu.LEFT, (int)Constants.Menu.FIRST + (int)Constants.Menu.STEP*i, "☞" + menu[i], ConsoleColor.Gray);
            }
            Console.SetCursorPosition((int)Constants.Menu.LEFT, (int)Constants.Menu.FIRST);
        }
        public void PrintSignIn()
        {
            PrintMenu("로그인");
            PrintMessage(0, Console.CursorTop, Constants.ESC_MESSAGE, ConsoleColor.Gray);
            PrintMessage(0, (int)Constants.SignIn.ID, "아이디  :", ConsoleColor.Gray);
            PrintMessage(0, (int)Constants.SignIn.PASSWORD, "비밀번호:", ConsoleColor.Gray);
        }
        public void PrintSignInFailure()
        {
            PrintSignIn();
            PrintMessage(0, Console.CursorTop + 1, "\n(아이디 또는 비밀번호를 잘못 입력하셨습니다.)", ConsoleColor.Red);
        }
    }
}
