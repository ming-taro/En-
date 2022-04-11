using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.View.MemberView
{
    class EditingScreen
    {
        public void PrintEditing()
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintMenu("회원정보수정");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<\n");

            Console.WriteLine("아이디: \n(5~10자의 영어, 숫자만 입력해주세요.)\n");
            Console.WriteLine("비밀번호: \n(5~10자의 영어, 숫자만 입력해주세요.)\n");
            Console.WriteLine("비밀번호 재확인: \n");
            Console.WriteLine("이름: \n(영어, 한글만 입력해주세요.)\n");
            Console.WriteLine("나이: \n(숫자만 입력해주세요.)\n");
            Console.WriteLine("휴대전화: \n(숫자만 입력해주세요.)\n");
            Console.WriteLine("도로명 주소: \n(ex: 서울특별시 광진구 능동로 209)\n");
        }
    }
}
