using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TimeTable
{
    class ClassNumber
    {
        private List<CourseVO> lectureSchedule;    //강의목록
        private List<CourseVO> courseOfInterest;   //관심과목목록
        private int appliedCredit;
        public ClassNumber(List<CourseVO> lectureSchedule, List<CourseVO> courseOfInterest, int appliedCredit)
        {
            this.courseOfInterest = courseOfInterest;
            this.lectureSchedule = lectureSchedule;
            this.appliedCredit = appliedCredit;
        }

        private int FindCourseIndexInList(string classNumber, string distribution)//입력받은 강의의 강의목록 내 인덱스값 찾기
        {
            for (int row = 1; row < lectureSchedule.Count; row++)  //lectureSchedule[0]은 목록이름
            {
                if (lectureSchedule[row].ClassNumber.Equals(classNumber) && lectureSchedule[row].Distribution.Equals(distribution))  //선택한 학과와 입력한 순번이 일치하는 과목을 찾음)
                {
                    return row;  //찾은 강좌의 인덱스번호 리턴
                }
            }
            return Constants.COURSE_NOT_ON_LIST;  //강의목록에 없는 순번을 입력한 경우
        }
        private string InputClassNumber(int left, string regex)
        {
            Logo logo = new Logo();
            EnteringText text = new EnteringText();
            string classNumber;

            while (Constants.INPUT_VALUE)
            {
                classNumber = text.EnterText(left, (int)Constants.ColumnMenu.FIRST, "");
                if (classNumber.Equals(Constants.ESC)) return Constants.ESC;

                if (Regex.IsMatch(classNumber, regex))
                {
                    logo.RemoveLine(left, (int)Constants.ColumnMenu.FIRST + 1);   //경고메세지를 지움
                    break;
                }
                else
                {
                    logo.FailureMessage(left, (int)Constants.ColumnMenu.FIRST + 1, "숫자만 입력 가능합니다.");
                    logo.RemoveLine(left, (int)Constants.ColumnMenu.FIRST);   //입력을 지움
                }
            }

            return classNumber;
        }
        public void InputCourseNumber(string classNumber, string distribution)
        {
            EnteringText text = new EnteringText();
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            Keyboard keyboard = new Keyboard();
            string number;
            int courseIndex;

            while (Constants.INPUT_VALUE)
            {
                searchByFieldScreen.PrintInputBox(appliedCredit);  //현재 신청한 학점 표시
                number = text.EnterText((int)Constants.Credit.THIRD + 18, (int)Constants.Credit.TOP, "");   //담을 순번 입력
                if (number.Equals(Constants.ESC)) break;           //입력도중 esc -> 관심과목 입력 종료

                courseIndex = FindCourseIndexInList(classNumber, distribution);  //입력받은 과목의 목록 내 인덱스 찾기
                if (courseIndex != Constants.COURSE_NOT_ON_LIST && courseOfInterest[courseIndex].Number.Equals(number))
                {
                    appliedCredit += Int32.Parse(courseOfInterest[courseIndex].Credit);   //관심과목 학점 ++
                    break;
                }
                else
                {
                    searchByFieldScreen.PrintFailureMessage((int)Constants.Credit.TOP - 2, ">조회 목록에 없는 강의입니다<");
                }

                if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break;   //순번입력 후 esc -> 학과검색으로 돌아가기
            }
        }
        public void SearchClassNumber()
        {
            CourseVO courseVO = new CourseVO();
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            Logo logo = new Logo();
            int courseIndex;

            while (Constants.INPUT_VALUE)
            {
                Console.Clear();
                searchByFieldScreen.PrintLine();
                logo.PrintMenu((int)Constants.ColumnMenu.LOGO_LEFT, (int)Constants.ColumnMenu.LOGO_TOP, "[학수번호/분반 검색]");

                logo.PrintMenu((int)Constants.RowMenu.FIRST, (int)Constants.ColumnMenu.FIRST, "☞학수번호(6자리 숫자):");
                string classNumber = InputClassNumber((int)Constants.RowMenu.FIRST + 24, @"^[0-9]{6}");   //학수번호
                if (classNumber.Equals(Constants.ESC)) return;

                logo.PrintMenu((int)Constants.RowMenu.THIRD, (int)Constants.ColumnMenu.FIRST, "☞분반(3자리 숫자):");
                string distribution = InputClassNumber((int)Constants.RowMenu.THIRD + 20, @"^[0-9]{3}");  //분반
                if (distribution.Equals(Constants.ESC)) return;

                courseVO.ClassNumber = classNumber;
                courseVO.Distribution = distribution;
                courseIndex = FindCourseIndexInList(classNumber, distribution);
                searchByFieldScreen.PrintResult(appliedCredit, lectureSchedule, courseVO);   //검색결과 출력

                InputCourseNumber(classNumber, distribution);  //순번입력
                courseVO.ClassNumber = null;
                courseVO.Distribution = null;
            }

        }
    }
}
