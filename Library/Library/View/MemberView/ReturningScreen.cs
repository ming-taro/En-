using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class ReturningScreen
    {

        public void PrintErrorMessage(string message)
        {
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("☞반납할 도서 번호:                                                    ");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(message);
        }

    }
}
