using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class PrintScreen
    {
        public void PrintMainScreen()
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>TicTacToe 게임<<<<<<<<<<<<<<<<<<<<");
            Console.WriteLine("======================모드 선택=======================\n");
            Console.WriteLine("              1. User    vs    Computer               \n");
            Console.WriteLine("              2. User1   vs    User2                  \n\n");
            Console.Write(">모드를 입력해주세요 : "); 
        }

        public void PrintModeScreen(int mode)
        {
            switch (mode)
            {
                case 1:
                    Console.WriteLine("==================User  vs  Computer==================");
                    break;
                case 2:
                    Console.WriteLine("===================User1  vs  User2===================");
                    break;
            }
        }

        public void PrintScoreBoard(int mode, int player1, int player2)
        {
            switch (mode)
            {
                case 1:
                    Console.WriteLine("Computer : " + player1 + "   vs   User : " + player2);
                    break;
                case 2:
                    Console.WriteLine("User1 : " + player1 + "   vs   User2 : " + player2);
                    break;
            }
        }
    }
}
