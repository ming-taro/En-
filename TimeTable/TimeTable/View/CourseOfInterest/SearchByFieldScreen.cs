using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class SearchByFieldScreen
    {
        public void PrintDepartment()
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();
            Logo logo = new Logo();

            logo.PrintLine((int)Constants.Console.LEFT, (int)Constants.Console.MIN_TOP);
            searchCategoryScreen.PrintDepartmentMenu();
            logo.PrintLine((int)Constants.Console.LEFT, (int)Constants.Console.MIN_TOP);
        }
    }
}
