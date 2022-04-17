using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Logo
    {
        public void RemoveInput(int left, int top)                      //입력 후 입력값, 메세지 출력부분 삭제
        {
            RemoveLine(left, top);
            RemoveLine(left, top + 1);
        }
        public void PrintMenu(int left, int top, string label)          //현재 조회중인 목록의 카테고리 이름
        {
            Console.SetCursorPosition(left, top);
            Console.Write(label);
        }
        public void FailureMessage(int left, int top, string message)   //입력값이 올바르지 않음
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintMenu(left, top, "(" + message + ")");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void RemoveLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("                                                                                                                                ");
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
            Console.Write("▷    학번 :");
            Console.SetCursorPosition(80, 12);
            Console.Write("▷비밀번호 :");
            PrintMenu(40, 18, "[ESC를 누르면 종료합니다]");
        }
        
    }
}
