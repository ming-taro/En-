using System;
using System.Text;

namespace LectureTimeTable
{
    class LectureTimeTable
    {
        static void Main(string[] args)
        {
            EnteringText enteringText = new EnteringText();

            string text = enteringText.EnterText(5, 5);
            if (text.Equals(Constants.ESC)) Console.WriteLine("[esc라는데]");
            Console.WriteLine("\n"+text);
        }
    }
}
