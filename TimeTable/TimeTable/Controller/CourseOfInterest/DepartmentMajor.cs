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
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            searchByFieldScreen.PrintLine();          

            Department departmentMenu = new Department();
            string department;

            while (Constants.KEYBOARD_OPERATION)
            {
                department = "";
                //departmentMenu.SelectMenu(ref department);      //학과선택
                if (department.Equals("")) return;  //학과선택도중 ESC -> 전공검색종료, 분야별 과목 검색 메뉴로 돌아감


            }


        }
    }
}
