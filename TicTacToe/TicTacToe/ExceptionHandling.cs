using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class ExceptionHandling
    {
        public bool IsSingleDigit(string str)       //한자리 숫자인지 판별
        {
            if (str.Length != 1) return false;     //문자열 길이가 1이 아니면 false
            else if (str[0] >= '0' && str[0] <= '9') return true;   //숫자라면 true
            else return false;                     //한 글자이지만 숫자가 아니라면 false
        }

        public bool IsBetween0To2(int number)      //숫자가 0~2범위인지 판별(좌표입력시 사용할 예외처리)
        {
            if (number >= 0 && number <= 2) return true;
            else return false;
        }

        public bool IsStartSpace(string str)      //공백으로 시작하는지 판별
        {
            if (str[0] == ' ') return true;
            else return false;
        }

        public bool IsNullOrEmpty(string str)     //enter만 누르는 경우, Ctrl + z 판별
        {
            if (String.IsNullOrEmpty(str)) return true;
            else return false;
        }

        public bool IsNumber1Or2(string str)
        {
            int number = Convert.ToInt32(str);

            if (number == 1 || number == 2) return true;
            else return false;
        }

        public bool IsValidValue(string str)     //숫자범위를 검사할만한 유효한 입력인가를 확인하는 함수
        {
            if (IsStartSpace(str) || IsNullOrEmpty(str) || !IsSingleDigit(str)) return false;   //한자리 숫자가 아닌 잘못된 입력
            else return true;
        }
    }
}
