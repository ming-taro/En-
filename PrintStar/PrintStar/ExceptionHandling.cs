﻿using System;
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

        public bool IsNaturalNumber(String str)  //'z'처럼 한 글자만 입력했을때도 숫자인지 아닌지 판별
        {
            if (Convert.ToInt32(str[0]) >= 49 && Convert.ToInt32(str[0]) <= 57) return true;
            else return false;
        }
    }
}
