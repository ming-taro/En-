using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintStar
{
    public class StartGame
    {
        int option;
        int line;

        public void ShowMenu()
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>별찍기 프로그램<<<<<<<<<<<<<<<<<<<<<\n\n");
            Console.WriteLine("                *                       ***");
            Console.WriteLine("               ***                       *\n");
            Console.WriteLine("          1. 정삼각형              2. 역삼각형\n\n");
            Console.WriteLine("               ***                       *");
            Console.WriteLine("                *                       ***");
            Console.WriteLine("               ***                       *\n");
            Console.WriteLine("          3. 모래시계              4.  다이아\n\n");
            Console.WriteLine(">>>>>>>>>>>>>>>>0번을 입력하면 종료됩니다<<<<<<<<<<<<<<<<\n");

        }

        public void PlayGame()
        {
            PrintingStar printingStar = new PrintingStar();

            ShowMenu();

            while (true)
            {
                Console.Write("Select Option : ");             //찍으려는 별 모양 고르기
                option = Convert.ToInt32(Console.ReadLine());
                if (option == 0)                               //0번이 입력되면 종료
                {
                    Console.WriteLine();
                    Console.WriteLine("Game is over...");
                    break;
                }
                else if(option < 0 || option > 4)              //0~4번 이외의 옵션을 고를 경우 : 메세지 출력 + 다시 입력
                {
                    Console.WriteLine("You entered an incorrect option. Please re-enter. ");
                    continue;
                }

                while(true)                                    //음수가 아닌 자연수를 입력받을때까지 반복
                {
                    Console.Write("The number of lines : ");   //찍으려는 별의 줄 수 입력
                    line = Convert.ToInt32(Console.ReadLine());
                    if (line <= 0) Console.WriteLine("\nPlease enter only natural numbers.");   //음수가 입력되는 경우 : 메세지 출력 + 다시 입력
                    else break;
                }
                Console.WriteLine();

                if (option == 1) printingStar.PrintTriangle(line);                  //1번 : 정삼각형
                else if (option == 2) printingStar.PrintInversedTriangle(line);     //2번 : 역삼각형
                else if (option == 3) printingStar.PrintSandglass(line);            //3번 : 모래시계
                else if (option == 4) printingStar.PrintDiamond(line);              //4번 : 다이아
                Console.WriteLine();
            }


        }
    }
}