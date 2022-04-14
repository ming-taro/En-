using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Logo
    {
        public void PrintMenu(int left, int top, string label)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(label);
        }
        public void FailureMessage(int left, int top, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintMenu(left, top, message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void RemoveLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("                                                      ");
        }
        public void PrintLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
        }
        public void PrintMain()
        {
            Console.SetWindowSize(120,25);
            PrintLine(1,1);
            PrintMenu(20, 6, "   Sejong University\n");
            Console.WriteLine("              ■");
            Console.WriteLine("              ■                             ■");
            Console.WriteLine("              ■");
            Console.WriteLine("              ■            ■■     ■■    ■  ■ ■");
            Console.WriteLine("              ■           ■  ■   ■  ■   ■  ■   ■");
            Console.WriteLine("              ■■■■■    ■■     ■■    ■  ■   ■");
            Console.WriteLine("                                        ■");
            Console.WriteLine("                                    ■  ■");
            Console.WriteLine("                                     ■■");
            Console.SetCursorPosition(60, 9);
            Console.WriteLine("▷학번(숫자 8자리):");
            Console.SetCursorPosition(60, 12);
            Console.Write("▷비밀번호(영어,숫자 5~10자리):");
            PrintMenu(20, 18, "[ESC를 누르면 종료합니다]");
            PrintLine(1,22);
        }
        
    }
}
