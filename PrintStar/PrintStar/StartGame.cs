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
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<<<<<<<<<<\n");

        }

        public int InputMenu()
        {
            ExceptionHandling exception = new ExceptionHandling();

            String str = Console.ReadLine();

            if (exception.IsEnter(str) || exception.IsStartSpace(str) || exception.IsStringLengthOne(str) == false)
            {
                Console.WriteLine(">>>>>잘못된 입력입니다. 숫자를 입력해주세요.<<<<<\n");
                return -1;
            }
            else if(exception.IsBetween1To4(Convert.ToInt32(str)) == false)
            {
                Console.WriteLine(">>>>>잘못된 입력입니다. 1~4 중 하나를 입력해주세요.<<<<<\n");
                return -1;
            }
            else
            {
                return Convert.ToInt32(str);
            }

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
            Console.SetWindowSize(100, 45);

            int menu;
            int line;
            bool loop = true;

            ShowMenu();    //메뉴화면을 보여줌

            while (loop)
            {
                Console.Write(">번호를 입력해주세요 : ");             //찍으려는 별 모양 고르기
                menu = InputMenu();
                if (menu == -1) continue;          //예외상황 발생시 처음으로 다시

                Console.Write(">줄 수를 입력해주세요 : ");   //찍으려는 별의 줄 수 입력
                line = Convert.ToInt32(Console.ReadLine());

                SelectMenu(menu, line); //메뉴선택 후 화면에 해당 메뉴를 보여줌

                if (isRetry() == false) break;       //게임종료

                Console.Clear();
                ShowMenu();    //메뉴화면을 보여줌
            }

            Console.WriteLine("게임을 종료합니다...");
        }
    }
}