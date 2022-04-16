using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class LogIn
    {

        public bool IsPossibleToLogIn(string id, string password)
        {
            if (id.Equals("19011507") && password.Equals("00000")) return Constants.LOGIN_IN;
            else return Constants.LOGIN_FAILURE;
        }
        public bool LogInToWebsite()  //로그인하기
        {
            EnteringText text = new EnteringText();

            string id = text.EnterText(93, 9, "");  //아이디 입력
            if (id.Equals(Constants.ESC)) return Constants.LOGIN_FAILURE;        //아이디 입력 중 esc -> 종료
            string password = text.EnterText(93, 12, "*");
            if (password.Equals(Constants.ESC)) return Constants.LOGIN_FAILURE;  //비밀번호 입력 중 esc -> 종료

            if (IsPossibleToLogIn(id, password)) return Constants.LOGIN_IN;      //올바르게 입력하면 로그인 성공
            else return Constants.LOGIN_FAILURE;
        }
    }
}
