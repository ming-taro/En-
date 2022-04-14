using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable.Controller.LectureScheduleAndRegistraion
{
    class Department
    {
        public void SelectMenu()  //학과전공 선택
        {
            Keyboard keyboard = new Keyboard((int)Constants.Grade.FRESHMAN, (int)Constants.LectureSchedule.DEPARTMENT);
            keyboard.SelectMenu();

        }
    }
}
