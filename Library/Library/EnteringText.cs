using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class EnteringText
    {
        public bool IsModifiers(ConsoleKeyInfo keyInfo)
        {
            if ((keyInfo.Modifiers & ConsoleModifiers.Alt) != 0) return Constants.MODIFIERS;
            //if ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0) return Constants.MODIFIERS;
            if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0) return Constants.MODIFIERS;

            return Constants.NOT_MODIFIERS;
        }
        public bool IsKorean(char oneLetter)   //한글인지 아닌지 확인
        {
            if (oneLetter >= '\uAC00' && oneLetter <= '\uD7AF' || oneLetter >= '\u3130' && oneLetter <= '\u314E' || oneLetter >= '\u314F' && oneLetter <= '\u3163')
            {
                return Constants.KOREAN;
            }
            return Constants.NOT_KOREAN;
        }
        public string EnterText(int left, int top)
        {
            StringBuilder text = new StringBuilder();
            ConsoleKeyInfo keyInfo;

            Console.SetCursorPosition(left, top);

            while (Constants.INPUT_VALUE)
            {
                keyInfo = Console.ReadKey(true);  //키 입력

                if (keyInfo.Key == ConsoleKey.Escape)        //esc
                {
                    return Constants.ESC;                    //입력종료 -> 뒤로가기 키
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;  //문자열 완성
                }
                else if (IsModifiers(keyInfo) == Constants.MODIFIERS || keyInfo.KeyChar == '\u0000')
                {
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && Console.GetCursorPosition().Left == left)
                {
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && IsKorean(text[text.Length - 1])) //지우기
                {
                    text.Remove(text.Length - 1, 1);          //문자열에서 마지막 글자를 지움
                    Console.SetCursorPosition(Console.CursorLeft - 2, top);
                    Console.Write("  ");
                    Console.SetCursorPosition(Console.CursorLeft - 2, top);
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    text.Remove(text.Length - 1, 1);          //문자열에서 마지막 글자를 지움
                    Console.SetCursorPosition(Console.CursorLeft - 1, top);
                    Console.Write("  ");
                    Console.SetCursorPosition(Console.CursorLeft - 2, top);
                }
                else
                {
                    Console.Write(keyInfo.KeyChar);
                    text.Append(keyInfo.KeyChar); //입력값 저장
                }
            }

            return text.ToString();
        }
    }
}
