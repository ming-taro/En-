using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class SignUpScreen
    {
        public void PrintSingUp()
        {
            LogoScreen logoScreen = new LogoScreen();

            Console.Clear();
            logoScreen.PrintMenu("회원가입");
            Console.WriteLine("아이디: \n(5~10자의 영어, 숫자만 입력해주세요.)\n");
            Console.WriteLine("비밀번호: \n(5~10자의 영어, 숫자만 입력해주세요.)\n");
            Console.WriteLine("비밀번호 재확인: \n\n");
            Console.WriteLine("이름: \n(영어, 한글만 입력해주세요.)\n");
            Console.WriteLine("나이: \n(숫자만 입력해주세요.)\n");
            Console.WriteLine("휴대전화: \n(숫자만 입력해주세요.(입력형식: 010-0000-0000))\n");
            Console.WriteLine("주소: \n(ex: 서울특별시 광진구 군자동)\n");
        }
    }
}
