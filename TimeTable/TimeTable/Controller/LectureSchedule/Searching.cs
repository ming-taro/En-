using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Searching
    {
        private List<CourseVO> lectureSchedule;    //강의목록
        private List<CourseVO> courseOfInterest;   //관심과목목록
        private int appliedCredit;
        public Searching(List<CourseVO> lectureSchedule)
        {
            this.lectureSchedule = lectureSchedule;
        }
        public Searching(List<CourseVO> lectureSchedule, List<CourseVO> courseOfInterest, int appliedCredit)
        {
            this.courseOfInterest = courseOfInterest;
            this.lectureSchedule = lectureSchedule;
            this.appliedCredit = appliedCredit;
        }
        public void ShowLectureSchedule(CourseVO courseVO)   //강의조회
        {
            LectureScheduleScreen lectureScheduleScreen = new LectureScheduleScreen();
            lectureScheduleScreen.PrintLectureSchedule((int)Constants.Console.HEIGHT + 3, lectureSchedule, courseVO);

            Logo logo = new Logo();
            logo.PrintMenu((int)Constants.GO_BACK_BUTTON_LEFT, Console.CursorTop, "☜[ESC]를 누르면 뒤로갑니다");
            Keyboard keyboard = new Keyboard((int)Constants.GO_BACK_BUTTON_LEFT, Console.CursorTop);
            Console.SetCursorPosition((int)Constants.GO_BACK_BUTTON_LEFT, Console.CursorTop);
            keyboard.PressESC();   //뒤로가기(esc) -> 조회 종료
        }
        private bool IsSameWord(string cellData, string searchWord)
        {
            if (searchWord == null || searchWord == "" || cellData.Equals(searchWord)) return Constants.IS_MEETING_CONDITION;

            return Constants.IS_NOT_MEETING_CONDITION;
        }
        private bool IsContainingWord(string cellData, string searchWord)
        {
            if (searchWord == null || searchWord == "" || cellData.Contains(searchWord)) return Constants.IS_MEETING_CONDITION;

            return Constants.IS_NOT_MEETING_CONDITION;
        }
        private bool IsValueMeetCondition(CourseVO LectureSchedule, CourseVO courseVO)
        {
            if (IsSameWord(LectureSchedule.Department, courseVO.Department) == Constants.IS_NOT_MEETING_CONDITION) return Constants.IS_NOT_MEETING_CONDITION;
            if (IsSameWord(LectureSchedule.CompletionType, courseVO.CompletionType) == Constants.IS_NOT_MEETING_CONDITION) return Constants.IS_NOT_MEETING_CONDITION;
            if (IsSameWord(LectureSchedule.Grade, courseVO.Grade) == Constants.IS_NOT_MEETING_CONDITION) return Constants.IS_NOT_MEETING_CONDITION;
            if (IsContainingWord(LectureSchedule.CourseTitle, courseVO.CourseTitle) == Constants.IS_NOT_MEETING_CONDITION) return Constants.IS_NOT_MEETING_CONDITION;
            if (IsContainingWord(LectureSchedule.Instructor, courseVO.Instructor) == Constants.IS_NOT_MEETING_CONDITION) return Constants.IS_NOT_MEETING_CONDITION;
            if (IsSameWord(LectureSchedule.ClassNumber, courseVO.ClassNumber) == Constants.IS_NOT_MEETING_CONDITION || IsSameWord(LectureSchedule.Distribution, courseVO.Distribution) == Constants.IS_NOT_MEETING_CONDITION) return Constants.IS_NOT_MEETING_CONDITION;

            return Constants.IS_MEETING_CONDITION;
        }

        public int FindCourseIndexInList(string number, CourseVO courseVO)//입력받은 강의의 강의목록 내 인덱스값 찾기
        {
            for (int row = 1; row < lectureSchedule.Count; row++)  //lectureSchedule[0]은 목록이름
            {
                if (lectureSchedule[row].Number.Equals(number) && IsValueMeetCondition(lectureSchedule[row], courseVO))
                {
                    Console.WriteLine("["+row+"]");
                    Console.ReadLine();
                    return row;
                }
            }
            return Constants.COURSE_NOT_ON_LIST;  //강의목록에 없는 순번을 입력한 경우
        }
        public bool IsAppyingWithinCredit(int courseIndex)  //24학점 범위 내에서 신청하는지
        {
            if (24 >= appliedCredit + (Int32.Parse(lectureSchedule[courseIndex].Credit)))
            {
                return Constants.IS_APPLYING_WITHIN_CREDIT;
            }
            return Constants.IS_NOT_APPLYING_WITHIN_CREDIT;
        }
        public bool IsDuplicateCourse(int courseIndex) //추가하려는 강의가 이미 관심과목리스트에 있는지 확인(같은 과목X)
        {
            for (int row = 1; row < courseOfInterest.Count; row++) //과목명이 겹치는가로 확인
            {
                if (courseOfInterest[row].CourseTitle.Equals(lectureSchedule[courseIndex].CourseTitle)) return Constants.IS_DUPLICATE_COURSE;
            }

            return Constants.IS_FIRST_APPLICATION;    //처음 담는 강의
        }
        public void ShowMessage(int courseIndex)
        {
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();

            if (courseIndex == Constants.COURSE_NOT_ON_LIST)
            {
                searchByFieldScreen.PrintFailureMessage((int)Constants.Credit.TOP - 2, ">입력하신 번호에 해당하는 강의가 없습니다<");     //관심과목 담기 실패 메세지
            }
            else if (IsAppyingWithinCredit(courseIndex) == Constants.IS_NOT_APPLYING_WITHIN_CREDIT)
            {
                searchByFieldScreen.PrintFailureMessage((int)Constants.Credit.TOP - 2, ">관심과목은 최대 24학점까지 담을 수 있습니다<");     //관심과목 담기 실패 메세지
            }
            else if (IsDuplicateCourse(courseIndex))
            {
                searchByFieldScreen.PrintFailureMessage((int)Constants.Credit.TOP - 2, ">같은 강의는 중복해서 담을 수 없습니다<");     //관심과목 담기 실패 메세지
            }
            else
            {
                searchByFieldScreen.PrintSuccessMessage((int)Constants.Credit.TOP - 2, ">>>관심과목 담기에 성공했습니다!<<<");    //관심과목 담기 성공 메세지
                courseOfInterest.Add(lectureSchedule[courseIndex]);     //관심과목 리스트에 등록
                appliedCredit += (Int32.Parse(lectureSchedule[courseIndex].Credit)); //담은 학점에 성공한 과목의 학점 추가
            }
        }
        public int InputCourseNumber(CourseVO courseVO)  //관심과목담기
        {
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            searchByFieldScreen.PrintResult(appliedCredit, lectureSchedule, courseVO);   //검색 결과 강의목록 출력

            EnteringText text = new EnteringText();
            Keyboard keyboard = new Keyboard();
            string number;
            int courseIndex;

            while (Constants.INPUT_VALUE)
            {
                searchByFieldScreen.PrintInputBox(appliedCredit);  //현재 신청한 학점 표시
                number = text.EnterText((int)Constants.Credit.THIRD + 18, (int)Constants.Credit.TOP, "");   //담을 순번 입력
                if (number.Equals(Constants.ESC)) break;           //입력도중 esc -> 관심과목 입력 종료

                courseIndex = FindCourseIndexInList(number, courseVO);  //입력받은 과목의 목록 내 인덱스 찾기
                ShowMessage(courseIndex);

                if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break;   //순번입력 후 esc -> 학과검색으로 돌아가기
            }

            return appliedCredit;   //esc클릭 후 종료할 때 담은 관심과목 학점을 리턴
        }
    }
}
