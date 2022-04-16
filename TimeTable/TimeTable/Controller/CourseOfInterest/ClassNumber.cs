using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TimeTable
{
    class ClassNumber
    {
        private string InputClassNumber()
        {
            Logo logo = new Logo();
            EnteringText text = new EnteringText();
            string classNumber;

            while (Constants.INPUT_VALUE)
            {
                classNumber = text.EnterText((int)Constants.RowMenu.FIRST + 24, (int)Constants.ColumnMenu.FIRST, "");
                if (classNumber.Equals(Constants.ESC)) return Constants.ESC;

                if (Regex.IsMatch(classNumber, @"^[0-9]{6}"))
                {
                    logo.RemoveLine((int)Constants.RowMenu.FIRST + 24, (int)Constants.ColumnMenu.FIRST + 1);   //경고메세지를 지움
                    break;
                }
                else
                {
                    logo.FailureMessage((int)Constants.RowMenu.FIRST + 23, (int)Constants.ColumnMenu.FIRST + 1, "숫자만 입력 가능합니다.");
                    logo.RemoveLine((int)Constants.RowMenu.FIRST + 24, (int)Constants.ColumnMenu.FIRST);   //입력을 지움
                }
            }

            return classNumber;
        }
        public void SearchClassNumber()
        {
            CourseVO courseVO = new CourseVO();
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            Logo logo = new Logo();

            Console.Clear();
            searchByFieldScreen.PrintLine();
            logo.PrintMenu((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.FIRST, "☞학수번호(6자리 숫자):");
            logo.PrintMenu((int)Constants.RowMenu.FOURTH, (int)Constants.ColumnMenu.FIRST, "☞분반(3자리 숫자):");
            logo.PrintMenu((int)Constants.ColumnMenu.LOGO_LEFT, (int)Constants.ColumnMenu.LOGO_TOP, "[학수번호/분반 검색]");

            string classNumber = InputClassNumber();   //학수번호
            if (classNumber.Equals(Constants.ESC)) return;

            Console.ReadLine();

            string distribution;

        }
    }
}
