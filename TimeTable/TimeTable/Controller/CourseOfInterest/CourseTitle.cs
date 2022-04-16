using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TimeTable
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
        private int FindCourseIndexInList(string number, string title, string inputType)//입력받은 강의의 강의목록 내 인덱스값 찾기
        {
            for (int row = 1; row < lectureSchedule.Count; row++)  //lectureSchedule[0]은 목록이름
            {
                if (inputType.Equals("courseTitle") && lectureSchedule[row].Number.Equals(number) && lectureSchedule[row].CourseTitle.Contains(title))  //선택한 학과와 입력한 순번이 일치하는 과목을 찾음)
                {
                    return row;  //찾은 강좌의 인덱스번호 리턴
                }
                else if (inputType.Equals("instructor") && lectureSchedule[row].Number.Equals(number) && lectureSchedule[row].Instructor.Contains(title))
                {
                    return row;
                }
                else if (inputType.Equals("classNumber") && lectureSchedule[row].Number.Equals(number) && lectureSchedule[row].ClassNumber.Equals(title))
                {
                    return row;
                }
            }
            return Constants.COURSE_NOT_ON_LIST;  //강의목록에 없는 순번을 입력한 경우
        }
        private string InputCourseTitle(int top)
        {
            Logo logo = new Logo();
            string title;
            EnteringText text = new EnteringText();

            while (Constants.INPUT_VALUE)
            {
                logo.RemoveLine(Constants.LEFT_VALUE_OF_COURSE_INPUT, top); //이전 입력기록 지움
                title = text.EnterText(Constants.LEFT_VALUE_OF_COURSE_INPUT, top, ""); //검색어 입력

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
        public void InputCourseNumber(string title, string inputType)
        {
            EnteringText text = new EnteringText();
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            Keyboard keyboard = new Keyboard();
            DepartmentMajor departmentMajor = new DepartmentMajor(lectureSchedule, courseOfInterest, appliedCredit);
            string number;
            int courseIndex;

            while (Constants.INPUT_VALUE)
            {
                searchByFieldScreen.PrintInputBox(appliedCredit);  //현재 신청한 학점 표시
                number = text.EnterText((int)Constants.Credit.THIRD + 18, (int)Constants.Credit.TOP, "");   //담을 순번 입력
                if (number.Equals(Constants.ESC)) break;    //입력도중 esc -> 관심과목 입력 종료

                courseIndex = FindCourseIndexInList(number, title, inputType);  //입력받은 과목의 목록 내 인덱스 찾기
                departmentMajor.ShowMessage(courseIndex);
                appliedCredit = departmentMajor.GetAppliedCredit();  //학점수정

                if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break;   //순번입력 후 esc -> 학과검색으로 돌아가기
            }
        }
        public int SearchTitle(string InputType)
        {
            CourseVO courseVO = new CourseVO();
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            Logo logo = new Logo();
            
            while (Constants.INPUT_VALUE)
            {
                Console.Clear();
                searchByFieldScreen.PrintLine();
                logo.PrintMenu((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.FIRST, "☞2글자 이상 입력:");
                if(InputType.Equals("courseTitle")) logo.PrintMenu((int)Constants.ColumnMenu.LOGO_LEFT, (int)Constants.ColumnMenu.LOGO_TOP, "[교과목명 검색]");
                else if(InputType.Equals("instructor")) logo.PrintMenu((int)Constants.ColumnMenu.LOGO_LEFT, (int)Constants.ColumnMenu.LOGO_TOP, "[교수명 검색]");

                string title = InputCourseTitle((int)Constants.ColumnMenu.FIRST);  //교과명 입력
                if (title.Equals(Constants.ESC)) return appliedCredit; //교과명 입력중 esc -> 입력종료

                if (InputType.Equals("courseTitle")) courseVO.CourseTitle = title;     //교과명 입력메뉴
                else if (InputType.Equals("instructor")) courseVO.Instructor = title;  //교수명 입력메뉴

                searchByFieldScreen.PrintResult(appliedCredit, lectureSchedule, courseVO);   //강의목록 출력

                InputCourseNumber(title, InputType);  //순번 입력
                courseVO.CourseTitle = null;
                courseVO.Instructor = null;
            }

        }
    }
}
