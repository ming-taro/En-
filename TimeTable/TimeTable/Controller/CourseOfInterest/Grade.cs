using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Grade
    {
        private List<CourseVO> lectureSchedule;    //강의목록
        private List<CourseVO> courseOfInterest;   //관심과목목록
        private int appliedCredit;    //현재 담은 관심과목 학점
        public Grade(List<CourseVO> lectureSchedule, List<CourseVO> courseOfInterest, int appliedCredit)
        {
            this.courseOfInterest = courseOfInterest;
            this.lectureSchedule = lectureSchedule;
            this.appliedCredit = appliedCredit;
        }
        public int SearchGrade()
        {
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            SelectingGrade selectingGrade = new SelectingGrade();
            CourseVO courseVO = new CourseVO();
            DepartmentMajor departmentMajor = new DepartmentMajor(lectureSchedule, courseOfInterest, appliedCredit);

            while (Constants.KEYBOARD_OPERATION)
            {
                searchByFieldScreen.PrintLine();
                selectingGrade.SelectMenu(courseVO);     //학년선택하기
                if (courseVO.Grade == null) break;  //학년선택 중 esc -> 과목조회를 종료하고 분야별검색 메뉴로 돌아감

                searchByFieldScreen.PrintResult(appliedCredit, lectureSchedule, courseVO);   //학년선택시 -> 학년검색결과를 보여줌
                departmentMajor.InputCourseNumber(courseVO.Grade);   //순번 입력
                appliedCredit = departmentMajor.GetAppliedCredit();
                courseVO.Grade = null;  //학과별 조회 창으로 -> 검색어 초기화
                Console.Clear();
            }

            return appliedCredit;   //esc클릭 후 종료할 때 담은 관심과목 학점을 리턴
        }
    }
}
