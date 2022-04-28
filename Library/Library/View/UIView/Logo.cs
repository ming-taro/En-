﻿using System;
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
            Console.Write("\n>>>>>>>>>>>>>>>>>>>>>>>[ESC]:뒤로가기<<<<<<<<<<<<<<<<<<<<<<<<");
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
            Console.SetWindowSize(61, 40);
            
            PrintTitle();
            for (int i = 0; i < menu.Length; i++)
            {
                Console.SetCursorPosition(25, 13 + i);
                Console.WriteLine("☞" + menu[i]);
            }
            Console.SetCursorPosition(25, 13);
        }
        public void PrintSignIn()
        {
            PrintMenu("로그인");
            Console.WriteLine("아이디: \n비밀번호:");
        }
        public void PrintSignInFailure()
        {
            PrintSignIn();
            PrintMessage(0, Console.CursorTop + 1, "\n(아이디 또는 비밀번호를 잘못 입력하셨습니다.\n다시 입력해주세요.)", ConsoleColor.Red);
        }
    }
}
