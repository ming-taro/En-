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

        public void PrintInversedTriangle(int line, int startValue)        //2번 : 역삼각형
        {
            for (int i = startValue; i < line; i++)            //line줄 출력
            {
                for (int j = 0; j < i; j++)           //i개의 공백 출력(ex : n=5인 1번째 줄의 공백은 0칸)
                {
                    Console.Write(" ");
                }
                for (int k = 0; k < 2 * (line - i) - 1; k++)     //2*(line - i) - 1개의 별 출력(ex : n=5인 2번째 줄의 별은 2*(5-1)-1=7개)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        public void PrintSandglass(int line)              //3번 : 모래시계
        {
            PrintInversedTriangle(line, 0);               //별 모양 = 역삼각형 + 정삼각형
            PrintTriangle(line);
        }

        public void PrintDiamond(int line)                //4번 : 다이아
        {
            PrintTriangle(line);                          //별 모양 = 정삼각형 + (역삼각형 - 1번째 줄)
            PrintInversedTriangle(line, 1);
        }
    }
}
