using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class SignInScreen
    {
        public void PrintSignIn()
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintMenu("로그인");
            Console.WriteLine("아이디: \n비밀번호:");
        }
        public void PrintFailure()
        {
            PrintSignIn();
            Console.WriteLine("\n아이디 또는 비밀번호를 잘못 입력하셨습니다.\n다시 입력해주세요.");
        }
    }
}
