using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class EnteringText
    {
        StringBuilder text;
        ConsoleKeyInfo keyInfo;
        public EnteringText()
        {
            text = new StringBuilder();
        }
        public bool IsModifiers(ConsoleKeyInfo keyInfo)
        {
            if ((keyInfo.Modifiers & ConsoleModifiers.Alt) != 0) return Constants.MODIFIERS;
            if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0) return Constants.MODIFIERS;

            return Constants.NOT_MODIFIERS;
        }
        public bool IsKorean(char oneLetter)   //한글인지 아닌지 확인(uAC00~uD7AF:가~힣, u3130~u314E:ㄱ~ㅎ, u314F~u3163:ㅏ~ㅣ)
        {
            if (oneLetter >= '\uAC00' && oneLetter <= '\uD7AF' || oneLetter >= '\u3130' && oneLetter <= '\u314E' || oneLetter >= '\u314F' && oneLetter <= '\u3163')
            {
                return Constants.KOREAN;
            }
            return Constants.NOT_KOREAN;
        }
        public string EnterText(int left, int top, string mark)
        {
            text.Clear();
            Console.SetCursorPosition(left, top);

            while (Constants.INPUT_VALUE)
            {
                keyInfo = Console.ReadKey(true);  //키 입력

                if (keyInfo.Key == ConsoleKey.Escape)        
                {
                    return Constants.ESC;                    //입력종료 -> 뒤로가기 키
                }
                else if (keyInfo.Key == ConsoleKey.Enter)    //문자열 완성 -> 입력받기를 멈추고 지금까지 입력받은 문자를 리턴함
                {
                    break;  
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && Console.GetCursorPosition().Left != left && IsKorean(text[text.Length - 1])) //한글입력값 지우기
                {
                    text.Remove(text.Length - 1, 1);          //문자열에서 마지막 글자를 지움
                    Console.SetCursorPosition(Console.CursorLeft - 2, top);
                    Console.Write("  ");                      //Console창에서 한글 한 글자를 지움
                    Console.SetCursorPosition(Console.CursorLeft - 2, top);
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && Console.GetCursorPosition().Left != left) //한글 이외의 문자입력값 지우기(지운 시점의 커서값(left)이 처음 시작할 때의 커서위치(left)와 같아지면 더 이상 지울 문자가 없음)
                {
                    text.Remove(text.Length - 1, 1);          //문자열에서 마지막 글자를 지움
                    Console.SetCursorPosition(Console.CursorLeft - 1, top);
                    Console.Write("  ");                      //Console창에서 한글 이외의 문자 하나를 지움
                    Console.SetCursorPosition(Console.CursorLeft - 2, top);
                }
                else if (mark.Equals("*") && IsModifiers(keyInfo) == Constants.NOT_MODIFIERS && keyInfo.KeyChar != '\u0000' && keyInfo.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");           //비밀번호 입력시
                    text.Append(keyInfo.KeyChar); //입력값 저장
                }
                else if(IsModifiers(keyInfo) == Constants.NOT_MODIFIERS && keyInfo.KeyChar != '\u0000' && keyInfo.Key != ConsoleKey.Backspace)
                {      
                    Console.Write(keyInfo.KeyChar);
                    text.Append(keyInfo.KeyChar); //입력값 저장
                }//shift,ctrl키가 아니고 null('\u0000')문자가 아니며  backspace키가 아닌 문자를 입력한 경우 -> stringbuilder에 입력한 문자 저장
            }

            return text.ToString();
        }
    }
}
