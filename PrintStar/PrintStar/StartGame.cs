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
            Console.Clear();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>별찍기 프로그램<<<<<<<<<<<<<<<<<<<<<\n\n");
            Console.WriteLine("                *                       ***");
            Console.WriteLine("               ***                       *\n");
            Console.WriteLine("          1. 정삼각형              2. 역삼각형\n\n");
            Console.WriteLine("               ***                       *");
            Console.WriteLine("                *                       ***");
            Console.WriteLine("               ***                       *\n");
            Console.WriteLine("          3. 모래시계              4.  다이아\n\n");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<\n");

        }

        public void SelectMenu(int menu, int line)
        {
            PrintingStar printingStar = new PrintingStar();

            switch(menu){
                case 1:
                    printingStar.PrintTriangle(line);             //1번 : 정삼각형
                    break;
                case 2:
                    printingStar.PrintInversedTriangle(line);     //2번 : 역삼각형
                    break;
                case 3:
                    printingStar.PrintSandglass(line);            //3번 : 모래시계
                    break;
                case 4:
                    printingStar.PrintDiamond(line);              //4번 : 다이아
                    break;
            }
        }

        public bool isRetry()
        {
            Console.Write(">>>다시 하시겠습니까?(0.종료      1.처음화면으로) : ");
            int retry = Convert.ToInt32(Console.ReadLine());

            if (retry == 0) return false;
            else return true;
        }

        public void PlayGame()
        {
            int menu;
            int line;

            Console.SetWindowSize(100, 45);

            while (true)
            {
                ShowMenu();    //메뉴화면을 보여줌
                Console.Write(">>>번호를 입력해주세요 : ");             //찍으려는 별 모양 고르기
                menu = Convert.ToInt32(Console.ReadLine());

                Console.Write(">>>줄 수를 입력해주세요 : ");   //찍으려는 별의 줄 수 입력
                line = Convert.ToInt32(Console.ReadLine());

                SelectMenu(menu, line); //메뉴선택 후 화면에 해당 메뉴를 보여줌

                if (isRetry()) continue;  //게임 다시 시작
                else break;  //
            }

            Console.WriteLine("게임을 종료합니다...");
        }
    }
}