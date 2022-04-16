using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TimeTable.Controller.CourseOfInterest
{
    class CourseTitle
    {
        private List<CourseVO> lectureSchedule;    //강의목록
        private List<CourseVO> courseOfInterest;   //관심과목목록
        private int appliedCredit;
        public CourseTitle(List<CourseVO> lectureSchedule, List<CourseVO> courseOfInterest, int appliedCredit)
        {
            this.courseOfInterest = courseOfInterest;
            this.lectureSchedule = lectureSchedule;
            this.appliedCredit = appliedCredit;
        }
        public int FindCourseIndexInList(string number, string title)//입력받은 강의의 강의목록 내 인덱스값 찾기
        {
            for (int row = 1; row < lectureSchedule.Count; row++)  //lectureSchedule[0]은 목록이름
            {
                if (lectureSchedule[row].Number.Equals(number) && lectureSchedule[row].CourseTitle.Contains(title))  //선택한 학과와 입력한 순번이 일치하는 과목을 찾음)
                {
                    return row;  //찾은 강좌의 인덱스번호 리턴
                }
            }
            return Constants.COURSE_NOT_ON_LIST;  //강의목록에 없는 순번을 입력한 경우
        }
        public string InputCourseTitle(int top)
        {
            Logo logo = new Logo();
            string title;
            EnteringText text = new EnteringText();

            while (Constants.INPUT_VALUE)
            {
                logo.RemoveLine(Constants.LEFT_VALUE_OF_COURSE_INPUT, top); //이전 입력기록 지움
                title = text.EnterText(Constants.LEFT_VALUE_OF_COURSE_INPUT, top); //검색어 입력

                if (title.Equals(Constants.ESC))  //esc -> 입력중 뒤로가기
                {
                    logo.RemoveInput(Constants.LEFT_VALUE_OF_COURSE_INPUT, top);   //입력값, 경고메세지 지움
                    return Constants.ESC;
                }
                else if (string.IsNullOrEmpty(title) == Constants.IS_NOT_NULL_OR_EXCEPTION && Regex.IsMatch(title, @"^[a-zA-Z가-힣\#\+]{2,20}"))
                {
                    logo.RemoveLine(Constants.LEFT_VALUE_OF_COURSE_INPUT, top + 1);//경고메세지 지움
                    break;
                }
                logo.FailureMessage(Constants.LEFT_VALUE_OF_COURSE_INPUT, top + 1, "영어,한글,+,#만 가능합니다.");  //경고메세지 출력
            }
            return title;
        }
        public int SearchTitle(int top)
        {
            CourseVO courseVO = new CourseVO();
            Logo logo = new Logo();
            logo.PrintTwoLine();
            logo.PrintMenu((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.FIRST, "☞2글자 이상 입력:");
            logo.PrintMenu((int)Constants.ColumnMenu.LOGO_LEFT, (int)Constants.ColumnMenu.LOGO_TOP, "[교과명 검색]");

            string title = InputCourseTitle(top);
            if (title.Equals(Constants.ESC)) return appliedCredit; //입력종료
            
            courseVO.CourseTitle = title;

            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            searchByFieldScreen.PrintResult(appliedCredit, lectureSchedule, courseVO);

            DepartmentMajor departmentMajor = new DepartmentMajor(lectureSchedule, courseOfInterest, appliedCredit);
            departmentMajor.ShowMessage(FindCourseIndexInList());

        }
    }
}
