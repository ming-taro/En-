using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class DepartmentMajor
    {
        private List<CourseVO> courseOfInterest;
        public DepartmentMajor(List<CourseVO> courseOfInterest)
        {
            this.courseOfInterest = courseOfInterest;
        }

        public void SearchMajor(ref int appliedCredit)
        {
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            searchByFieldScreen.PrintLine();          

            Department departmentMenu = new Department();
            CourseVO courseVO = new CourseVO();

            departmentMenu.SelectMenu(courseVO);
            if (courseVO.Department == null) return;  //학과선택 중 esc -> 과목조회를 종료하고 분야별검색 메뉴로 돌아감

            searchByFieldScreen.PrintInputBox(appliedCredit);                                   //학과선택시
            LectureScheduleScreen lectureScheduleScreen = new LectureScheduleScreen();
            lectureScheduleScreen.PrintLectureSchedule((int)Constants.Credit.TOP + 2, courseVO);//목록을 보여줌

            Console.SetCursorPosition((int)Constants.Credit.THIRD + 20, (int)Constants.Credit.TOP);
            Console.Read();
        }
    }
}
