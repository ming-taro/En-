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

        public bool IsBetween0To2(string str)      //숫자가 0~2범위인지 판별(좌표입력시 사용할 예외처리)
        {
            int number = Convert.ToInt32(str);

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

        public bool IsNumber1Or2(string str)      //1 or 2 입력인지 확인하는 함수(한 자리 숫자가 입력되었을 때를 가정하고 사용)
        {
            int number = Convert.ToInt32(str);

            if (number == 1 || number == 2) return true;
            else return false;
        }

        public bool IsValidSpace(Board board, int row, int column)    //입력받은 행,열이 빈칸(유효한)인지 판별하는 함수
        {
            int spaceNumber = board.FindSpaceNumber(row, column);

            if (board.IsValidSpace(spaceNumber)) return true;
            else return false;

        }

        public bool IsValidValue(string str)     //숫자범위를 검사할만한 유효한 입력인가를 확인하는 함수
        {
            if (IsNullOrEmpty(str) || IsStartSpace(str) || !IsSingleDigit(str)) return false;   //한자리 숫자가 아닌 잘못된 입력
            else return true;
        }
    }
}
