using System;

namespace PrintStar
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            for(int i=0; i<n; i++)
            {
                for(int j=0; j<n-i; j++)
                {
                    Console.Write(" ");
                }
                for(int k=0; k<2*i+1; k++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

            /*for(int i=n; i<n; i++)
            {
                for(int j=0; j<; j++)
                {

                }
            }*/

            //Console.WriteLine("Hello World!");
        }
    }
}
