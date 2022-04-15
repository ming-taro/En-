using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class DepartmentMajor
    {
        public void SearchMajor()
        {
            string department = "";
            Department departmentMenu = new Department();
            Console.Read();  ///test용ㅇ요요요요
            departmentMenu.SelectMenu(ref department);

            while (Constants.KEYBOARD_OPERATION)
            {

            }


        }
    }
}
