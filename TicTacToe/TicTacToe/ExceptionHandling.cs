using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class ExceptionHandling
    {
        public bool IsSingleDigit(String str)       //한자리 숫자인지 판별
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

        public bool IsStartSpace(String str)      //공백으로 시작하는지 판별
        {
            if (str[0] == ' ') return true;
            else return false;
        }

        public bool IsNullOrEmpty(String str)     //enter만 누르는 경우, Ctrl + z 판별
        {
            if (String.IsNullOrEmpty(str)) return true;
            else return false;
        }
    }
}
