using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class PlayGame  
    {
        Player player1 = new Player();
        Player player2 = new Player();
        Board board = new Board();

        public void InputUserPoint(char drawType)
        {
            Console.WriteLine(">>>>>>>>좌표를 입력해주세요<<<<<<<<");
            Console.Write(">행 : ");
            string input = Console.ReadLine();
            int row = Convert.ToInt32(input);
            Console.Write(">열 : ");
            input = Console.ReadLine();
            int column = Convert.ToInt32(input);

            board.SetOneSpace(row, column, drawType);
        }

        public void InputComputerPoint(char drawType)
        {
            
        }
        public void UserVersusComputer()
        {
            bool loop = true;
            
            player1.DrawType = 'X';    //user
            player2.DrawType = 'O';    //computer

            while (loop)
            {
                InputUserPoint(player1.DrawType);   //유저 입력
                InputComputerPoint(player2.DrawType);
            }


        }

        public void StartGame()
        {
            PrintScreen printScreen = new PrintScreen();
            printScreen.PrintMainScreen();

            int mode = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            printScreen.PrintModeScreen(mode);
            printScreen.PrintScoreBoard(mode, 0, 0);
            printScreen.PrintBoardScreen(board);

            bool loop = true;

            while (loop)
            {


            }
        }


    }
}
