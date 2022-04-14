using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Logo
    {
        public void PrintLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
        }
        public void PrintMain()
        {
            Console.SetWindowSize(120,25);
            PrintLine(1,1);
            Console.SetCursorPosition(20, 7);
            Console.WriteLine("   Sejong Univercity");
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
            Console.WriteLine("학번(숫자 8자리):");
            Console.SetCursorPosition(60, 12);
            Console.Write("비밀번호(영어,숫자 5~10자리):");
            PrintLine(1,22);
        }

    }
}
