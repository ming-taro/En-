using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace TimeTable
{
    class DepartmentMajor
    {
        private List<CourseVO> lectureSchedule;    //강의목록
        private List<CourseVO> courseOfInterest;   //관심과목목록
        public DepartmentMajor(List<CourseVO> lectureSchedule, List<CourseVO> courseOfInterest)
        {
            this.courseOfInterest = courseOfInterest;
            this.lectureSchedule = lectureSchedule;
        }
        public bool IsCourseInList(string number, string department)
        {
            for (int row = 1; row <= lectureSchedule.Count; row++)
            {
                if (lectureSchedule[row].Number.Equals(number) && lectureSchedule[row].Department.Equals(department))  //선택한 학과와 입력한 순번이 일치하는 과목을 찾음)
                {
                    courseOfInterest.Add(lectureSchedule[row]);     //관심과목 리스트에 등록
                    return Constants.IS_COURSE_IN_LIST;
                }
            }

            return Constants.IS_NOT_COURSE_IN_LIST;      //강의목록에 없는 순번을 입력한 경우
        }
        public void InputCourseNumber(string department)
        {
            EnteringText text = new EnteringText();
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();

            while (Constants.INPUT_VALUE)
            {
                string CourseNumber = text.EnterText((int)Constants.Credit.THIRD + 18, (int)Constants.Credit.TOP);   //담을 순번 입력
                if (CourseNumber.Equals(Constants.ESC)) break;    //입력도중 esc -> 관심과목 입력 종료
                
                if (IsCourseInList(CourseNumber, department))      //선택한 학과에 입력한 순번이 있는 경우 
                {
                    searchByFieldScreen.PrintSuccessMessage();    //관심과목 담기 성공 메세지
                    Console.Read();
                }
                else
                {

                }
            }
        }

        public void SearchMajor(ref int appliedCredit)
        {
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            searchByFieldScreen.PrintLine();

            CourseVO courseVO = new CourseVO();
            Department departmentMenu = new Department();
            departmentMenu.SelectMenu(courseVO);
            if (courseVO.Department == null) return;  //학과선택 중 esc -> 과목조회를 종료하고 분야별검색 메뉴로 돌아감

            searchByFieldScreen.PrintInputBox(appliedCredit, lectureSchedule, courseVO);   //학과선택시 -> 학과검색결과를 보여줌
            InputCourseNumber(courseVO.Department);   //순번 입력
        }
    }
}
