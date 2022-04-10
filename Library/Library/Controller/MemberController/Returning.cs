using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Returning
    {
        public Returning(string memberId)
        {
            ReturningScreen returningScreen = new ReturningScreen();
            returningScreen.PrintReturning(memberId);      //회원의 도서대여목록 출력
        }
        public void ControlReturning()
        {
            Console.SetCursorPosition(17, 1);
            Console.Read();
        }
    }
}
