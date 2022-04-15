using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Logo
    {
        public void RemoveInput(int left, int top)
        {
            RemoveLine(left, top);
            RemoveLine(left, top + 1);
        }
        public void PrintMenu(int left, int top, string label)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(label);
        }
        public void FailureMessage(int left, int top, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintMenu(left, top, "(" + message + ")");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void RemoveLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("                                                                               ");
        }
        public void PrintLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
        }
        public void PrintLongLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("=================================================================================================================================================");

        }
        public void PrintTwoLine()
        {
            Console.SetCursorPosition((int)Constants.Console.LEFT, (int)Constants.Console.MIN_TOP);
            Console.Write(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            Console.SetCursorPosition((int)Constants.Console.LEFT, (int)Constants.Console.MAX_TOP);
            Console.Write(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
        }
        public void PrintMain()
        {
            Console.Clear();
            Console.SetWindowSize((int)Constants.Console.WIDTH, (int)Constants.Console.HEIGHT);
            PrintTwoLine();
            PrintMenu(40, 6, "   Sejong University\n");
            Console.WriteLine("                                ■");
            Console.WriteLine("                                ■                             ■");
            Console.WriteLine("                                ■");
            Console.WriteLine("                                ■            ■■     ■■    ■  ■ ■");
            Console.WriteLine("                                ■           ■  ■   ■  ■   ■  ■   ■");
            Console.WriteLine("                                ■■■■■    ■■     ■■    ■  ■   ■");
            Console.WriteLine("                                                          ■");
            Console.WriteLine("                                                      ■  ■");
            Console.WriteLine("                                                       ■■");
            Console.SetCursorPosition(80, 9);
            Console.WriteLine("▷학번(숫자 8자리):");
            Console.SetCursorPosition(80, 12);
            Console.Write("▷비밀번호(영어,숫자 5~10자리):");
            PrintMenu(40, 18, "[ESC를 누르면 종료합니다]");
        }
        
    }
}
