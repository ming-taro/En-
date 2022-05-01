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
            Console.WriteLine("\n\n\n                 〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓");
            Console.WriteLine("                ┃                                                                 ┃");
            Console.WriteLine("                ┃                                                                 ┃");
            Console.WriteLine("                ┃       ■          ■                                            ┃");
            Console.WriteLine("                ┃       ■          ■                                            ┃");
            Console.WriteLine("                ┃       ■       ◆ ■                                            ┃");
            Console.WriteLine("                ┃       ■       ■ ■■■  ■  ■  ■■     ■  ■  ■  ■       ┃");
            Console.WriteLine("                ┃       ■       ■ ■   ■ ■■   ■  ■    ■■    ■  ■       ┃");
            Console.WriteLine("                ┃       ■■■■ ■ ■■■  ■      ■■ ■  ■       ■■        ┃");
            Console.WriteLine("                ┃                                                     ■          ┃");
            Console.WriteLine("                ┃                                                    ■           ┃ ");
            Console.WriteLine("                ┃                                                  ■             ┃ ");
            Console.WriteLine("                ┃                                                                 ┃");
            Console.WriteLine("                 〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓");
            Console.WriteLine("                 [ESC]:뒤로가기                                        [Enter]:선택");
        }
        public void PrintMenu(string menu)
        {
            int left = ((int)Constants.Menu.CONSOLE_WIDTH - (menu.Length * 2)) / 2;
            Console.Clear();
            Console.WriteLine("▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧");
            Console.SetCursorPosition(left, 3);
            Console.WriteLine("[ " + menu + " ]\n\n");
            Console.WriteLine("▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨▧▨");
            Console.WriteLine("                                                                                    ☜[ESC]:뒤로가기");
        }
        public void PrintSearchBox(string menu, string searchWord)
        {
            PrintMenu(menu);
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
        public void PrintEsc(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine("                                                                                    ☜[ESC]:뒤로가기");
        }
        public void PrintEscAndEnter(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine("                                                                  [ESC]:뒤로가기      [ENTER]:재조회");
        }
        public void PrintLine()
        {
            Console.WriteLine("\n====================================================================================================\n");
        }
        public void PrintSingleLine()
        {
            Console.WriteLine("\n----------------------------------------------------------------------------------------------------\n");
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
            PrintMessage((int)Constants.SignIn.LEFT - 5, (int)Constants.SignIn.ID - 3, "〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓", ConsoleColor.Gray);
            PrintMessage((int)Constants.SignIn.LEFT, (int)Constants.SignIn.ID, "아이디  :", ConsoleColor.Gray);
            PrintMessage((int)Constants.SignIn.LEFT, (int)Constants.SignIn.PASSWORD, "비밀번호:", ConsoleColor.Gray);
            PrintMessage((int)Constants.SignIn.LEFT - 5, (int)Constants.SignIn.PASSWORD + 3, "〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓", ConsoleColor.Gray);
        }
        public void PrintSignInFailure()
        {
            PrintSignIn();
            PrintMessage((int)Constants.SignIn.LEFT, (int)Constants.SignIn.MESSAGE, "(아이디 또는 비밀번호를 잘못 입력하셨습니다.)", ConsoleColor.Red);
        }
    }
}
