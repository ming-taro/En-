using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class BorrowingScreen
    {
        public void PrintSuccessMessage()
        {
            Logo logoScreen = new Logo();
            logoScreen.PrintMenu("도서대여 완료");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<\n");
        }
    }
}
