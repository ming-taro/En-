using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintStar
{
    public class StartGame
    {
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

        public void SelectMenu(int menu, int line)
        {
            PrintingStar printingStar = new PrintingStar();

            if (menu == 0) return;
            else if (menu == 1) printingStar.PrintTriangle(line);             //1번 : 정삼각형
            else if (menu == 2) printingStar.PrintInversedTriangle(line);     //2번 : 역삼각형
            else if (menu == 3) printingStar.PrintSandglass(line);            //3번 : 모래시계
            else if (menu == 4) printingStar.PrintDiamond(line);              //4번 : 다이아
        }

        public void PlayGame()
        {
            ShowMenu();    //메뉴화면을 보여줌

            int menu;
            int line;

            while (true)
            {
                Console.Write("번호를 입력해주세요 : ");             //찍으려는 별 모양 고르기
                menu = Convert.ToInt32(Console.ReadLine());
                if (menu == 0)                               //0번이 입력되면 종료
                {
                    Console.WriteLine();
                    Console.WriteLine("Game is over...");
                    break;
                }
                else if(menu < 0 || menu > 4)              //0~4번 이외의 옵션을 고를 경우 : 메세지 출력 + 다시 입력
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

                SelectMenu(menu, line);
            }


        }
    }
}