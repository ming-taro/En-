using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Exception
    {
        public void PrintMessage(int left, int top, string message, ConsoleColor color)
        {
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void RemoveLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("                                                                           ");
        }
        public void PrintBookNotInList(int left, int top)
        {
            PrintMessage(left, top, "(현재 조회 목록에 없는 도서입니다. 다시 입력해주세요.)           ", ConsoleColor.Red);
        }
        public void PrintBookNameRegex(int left, int top)
        {
            PrintMessage(left, top, "(1~50자 이내의 문자를 입력해주세요.)                         ", ConsoleColor.Red);
        }
        public void PrintBookIdRegex(int left, int top)
        {
            PrintMessage(left, top, "(0~999사이의 숫자가 아닙니다.다시 입력해주세요.)               ", ConsoleColor.Red);
        }
        public void PrintBookIBorrowed(int left, int top)
        {
            PrintMessage(left, top, "(이미 대여중인 도서입니다. 다른 도서를 선택해주세요.)                  ", ConsoleColor.Red);
        }
        public void PrintQuantityZero(int left, int top)
        {
            PrintMessage(left, top, "(대여가능한 도서가 0권입니다. 다른 도서를 선택해주세요.)              ", ConsoleColor.Red);
        }
    }
}
