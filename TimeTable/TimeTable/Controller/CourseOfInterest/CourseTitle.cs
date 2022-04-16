using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable.Controller.CourseOfInterest
{
    class CourseTitle
    {

        public int InputCourseTitle()
        {
            CourseVO courseVO = new CourseVO();
            Logo logo = new Logo();
            logo.PrintTwoLine();
            //logo.PrintMenu();

            logo.PrintMenu((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.FIRST, "☞2글자 이상 입력:");

            EnteringText text = new EnteringText();
            //text.EnterText()

            return 1;
        }
    }
}
