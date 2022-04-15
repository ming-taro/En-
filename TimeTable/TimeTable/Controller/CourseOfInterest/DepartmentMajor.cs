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
        private int appliedCredit;    //현재 담은 관심과목 학점
        public DepartmentMajor(List<CourseVO> lectureSchedule, List<CourseVO> courseOfInterest, int appliedCredit)
        {
            this.courseOfInterest = courseOfInterest;
            this.lectureSchedule = lectureSchedule;
            this.appliedCredit = appliedCredit;
        }
        public int FindCourseIndexInList(string number, string department)//입력받은 강의의 강의목록 내 인덱스값 찾기
        {
            for (int row = 1; row < lectureSchedule.Count; row++)  //lectureSchedule[0]은 목록이름
            {
                if (lectureSchedule[row].Number.Equals(number) && lectureSchedule[row].Department.Equals(department))  //선택한 학과와 입력한 순번이 일치하는 과목을 찾음)
                {
                    return row;  //찾은 강좌의 인덱스번호 리턴
                }
            }
            return Constants.COURSE_NOT_ON_LIST;  //강의목록에 없는 순번을 입력한 경우
        }
        public bool IsAppyingWithinCredit(int courseIndex)  //24학점 범위 내에서 신청하는지
        {
            if(24 >= appliedCredit + (lectureSchedule[courseIndex].Credit[0] - '0'))
            {
                return Constants.IS_APPLYING_WITHIN_CREDIT;
            }
            return Constants.IS_NOT_APPLYING_WITHIN_CREDIT;
        }
        public bool IsDuplicateCourse(int courseIndex) //추가하려는 강의가 이미 관심과목리스트에 있는지 확인(같은 과목X)
        {
            for(int row = 1; row < courseOfInterest.Count; row++)
            {
                if (courseOfInterest.Equals(lectureSchedule[courseIndex].CourseTitle)) return Constants.IS_DUPLICATE_COURSE; 
            }

            return Constants.IS_FIRST_APPLICATION;    //처음 담는 강의
        }
        public void InputCourseNumber(string department)
        {
            EnteringText text = new EnteringText();
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            Logo logo = new Logo();
            Keyboard keyboard = new Keyboard();
            string number;
            int courseIndex;

            while (Constants.INPUT_VALUE)
            {
                number = text.EnterText((int)Constants.Credit.THIRD + 18, (int)Constants.Credit.TOP);   //담을 순번 입력
                if (number.Equals(Constants.ESC)) break;    //입력도중 esc -> 관심과목 입력 종료

                courseIndex = FindCourseIndexInList(number, department);  //입력받은 과목의 목록 내 인덱스 찾기

                if(courseIndex == Constants.COURSE_NOT_ON_LIST)
                {
                    searchByFieldScreen.PrinFaliureMessage(">입력하신 번호에 해당하는 강의가 없습니다<");     //관심과목 담기 실패 메세지
                }
                else if(IsAppyingWithinCredit(courseIndex) == Constants.IS_NOT_APPLYING_WITHIN_CREDIT)
                {
                    searchByFieldScreen.PrinFaliureMessage(">관심과목은 최대 24학점까지 담을 수 있습니다<");     //관심과목 담기 실패 메세지
                }
                else if (IsDuplicateCourse(courseIndex))
                {
                    searchByFieldScreen.PrinFaliureMessage(">같은 강의는 중복해서 담을 수 없습니다<");     //관심과목 담기 실패 메세지
                }
                else
                {
                    searchByFieldScreen.PrintSuccessMessage();    //관심과목 담기 성공 메세지
                    courseOfInterest.Add(lectureSchedule[courseIndex]);     //관심과목 리스트에 등록
                    appliedCredit += lectureSchedule[courseIndex].Credit[0] - '0'; //담은 학점에 성공한 과목의 학점 추가
                }
                
                if(keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break;   //순번입력 후 esc -> 학과검색으로 돌아가기
                searchByFieldScreen.PrintInputBox(appliedCredit);  //enter입력 -> 현재 페이지에서 과목 입력하기
                
            }
        }

        public int SearchMajor()
        {
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            CourseVO courseVO = new CourseVO();
            Department departmentMenu = new Department();

            while (Constants.KEYBOARD_OPERATION)
            {
                searchByFieldScreen.PrintLine();
                departmentMenu.SelectMenu(courseVO);     //학과 선택하기
                if (courseVO.Department == null) break;  //학과선택 중 esc -> 과목조회를 종료하고 분야별검색 메뉴로 돌아감

                searchByFieldScreen.PrintResult(appliedCredit, lectureSchedule, courseVO);   //학과선택시 -> 학과검색결과를 보여줌
                InputCourseNumber(courseVO.Department);   //순번 입력

                courseVO.Department = "";  //학과별 조회 창으로 -> 검색어 초기화
                Console.Clear();
            }

            return appliedCredit;   //esc클릭 후 종료할 때 담은 관심과목 학점을 리턴
        }
    }
}
