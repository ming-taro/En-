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
            Console.WriteLine("도서번호: \n\n도서명: \n\n출판사: \n\n저자: \n\n가격: \n\n수량: ");
        }
    }
}
