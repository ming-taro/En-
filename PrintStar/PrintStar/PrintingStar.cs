using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintStar
{
    class PrintingStar   
    {

        public void PrintTriangle(int line)                //1번 : 정삼각형
        {
            for (int i = 0; i < line; i++)                 //n줄 출력
            {
                for (int j = 0; j < line - i - 1; j++)     //n-i-1개의 공백 출력(ex : n=5인 1번째 줄의 공백은 5-0-1=4칸) 
                {
                    Console.Write(" ");
                }
                for (int k = 0; k < 2 * i + 1; k++)     //i*2+1개의 별 출력(ex : 2번째 줄의 별은 2*1+1=3개)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        public void PrintInversedTriangle(int line)        //2번 : 역삼각형
        {
            for (int i = line - 1; i >= 0; i--)            //n줄 출력(별의 개수가 점점 줄어들기 때문에 i값이 줄어드는 방향으로 설정함)
            {
                for (int j = 0; j < line - i - 1; j++)     //n-i-1개의 공백 출력(ex : n=5인 1번째 줄의 공백은 5-4-1=0칸)
                {
                    Console.Write(" ");
                }
                for (int k = 0; k < 2 * i + 1; k++)     //2*i+1개의 별 출력(ex : n=5인 2번째 줄의 별은 2*3+1=7개)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        public void PrintSandglass(int line)              //3번 : 모래시계
        {
            PrintInversedTriangle(line);                  //별 모양 = 역삼각형 + 정삼각형
            PrintTriangle(line);
        }

        public void PrintDiamond(int line)                //4번 : 다이아
        {
            PrintTriangle(line);                          //별 모양 = 정삼각형 + (역삼각형 - 1번째 줄)
            for (int i = line - 2; i >= 0; i--)           //역삼각형 출력 함수 코드에서 1번째 줄을 빼고 출력 : 2번째 줄부터 n-1개의 줄 출력
            {
                for (int j = 0; j < line - i - 1; j++)
                {
                    Console.Write(" ");
                }
                for (int k = 0; k < 2 * i + 1; k++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
    }
}
