using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintStar
{
    class ExceptionHandling
    {
        public bool IsLengthLessThanTen(String str)    //문자열 길이가 10이하인지 판별
        {
            if (str.Length <= 10) return true;
            else return false;
        }

        public bool IsBetween1To4(int number)       //숫자가 1~4범위인지 판별(메뉴선택시 사용할 예외처리)
        {
            if (number >= 1 && number <= 4) return true;
            else return false;
        }

        public bool IsStartSpace(String str)     //공백으로 시작하는지 판별
        {
            if (str[0] == ' ') return true;
            else return false;
        }

        public bool IsNullOrEmpty(String str)          //enter만 누르는 경우 판별
        {
            if (String.IsNullOrEmpty(str)) return true;
            else return false;
        }

        public bool IsStartNaturalNumber(String str)  //첫글자가 숫자로 시작하는지 판별(ex 'z'처럼 영문자 하나 입력시 예외판별)
        {
            if (str[0] >= '1' && str[0] <= '9') return true;
            else return false;
        }

        public bool IsContainOnlyNumbers(String str)  //입력값이 숫자로만 이루어져 있는지 판별
        {
            foreach(char word in str)
            {
                if (!(word >= '0' && word <= '9')) return false;      //숫자가 아닌 값이 포함되어 있다면 false
            }

            return true;
        }
    }
}
