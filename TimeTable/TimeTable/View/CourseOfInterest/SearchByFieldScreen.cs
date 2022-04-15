using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class SearchByFieldScreen
    {
        public void PrintLine()
        {
            Logo logo = new Logo();

            Console.Clear();
            logo.PrintLine((int)Constants.Console.LEFT, (int)Constants.Console.MIN_TOP);
            logo.PrintLine((int)Constants.Console.LEFT, (int)Constants.SEARCH_BUTTON_TOP + 2);
        }
        public void PrintDepartmentSearchResult(string m)
        {
            LectureScheduleScreen lectureScheduleScreen = new LectureScheduleScreen();
            //lectureScheduleScreen.PrintLectureSchedule((int)Constants.SEARCH_BUTTON_TOP + 4, );
        }
    }
}
