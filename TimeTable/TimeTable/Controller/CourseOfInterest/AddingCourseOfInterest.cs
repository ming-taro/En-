using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class AddingCourseOfInterest
    {
        public void ShowCourseOfInterestMenu()
        {
            CourseOfInterestScreen courseOfInterestScreen = new CourseOfInterestScreen();
            Keyboard keyboard = new Keyboard((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FIRST);

            while (Constants.KEYBOARD_OPERATION)
            {
                courseOfInterestScreen.PrintMenu();



            }
            

        }
    }
}
