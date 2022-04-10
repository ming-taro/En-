using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class RegisteringScreen
    {
        public void PrintRegistering()
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintMenu("도서 등록");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<");
            Console.SetCursorPosition(0, 7);
            Console.WriteLine("도서번호: \n(0~999사이의 숫자만 입력 가능합니다.(ex: 123))\n");
            Console.WriteLine("도서명: \n(50자 이내로 입력해주세요.(ex: 이것이 C#이다))\n");
            Console.WriteLine("출판사: \n(50자 이내로 입력해주세요.(ex: 한빛미디어)\n");
            Console.WriteLine("저자: \n(50자 이내의 영어, 한글만 입력 가능합니다.(ex: 박상현)\n");
            Console.WriteLine("가격: \n(숫자만 입력 가능합니다.(ex: 34000)\n");
            Console.WriteLine("수량: \n(1~99사이의 숫자만 입력 가능합니다.(ex: 5))");
        }
    }
}
