using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintStar
{
    class ExceptionHandling
    {
        public bool IsStringLengthOne(String str)    //문자열 길이가 1인지 이상인지 판별
        {
            if (str.Length == 1) return true;
            else return false;
        }

        public bool IsBetween1To4(int number)       //숫자가 1~4범위인지 판별(메뉴선택시 사용할 예외처리)
        {
            if (number >= 1 && number <= 4) return true;
            else return false;
        }
    }
}
