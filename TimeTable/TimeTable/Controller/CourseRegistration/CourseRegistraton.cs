using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class CourseRegistraton
    {
        private List<CourseVO> lectureSchedule;   //강의목록
        private List<CourseVO> courseOfInterest;
        private List<CourseVO> courseRegistration;//수강신청 리스트
        private int appliedCredit;                //수강신청 리스트에 있는 강좌의 총 학점
        public CourseRegistraton(List<CourseVO> lectureSchedule, List<CourseVO> courseOfInterest, List<CourseVO> courseRegistration)
        {
            this.lectureSchedule = lectureSchedule;
            this.courseOfInterest = courseOfInterest;
            this.courseRegistration = courseRegistration;
            appliedCredit = 0;
        }
        public int FindCourseIndex(string number, List<CourseVO> courseOfInterest)  //현재 관심과목에 해당 순번이 존재하는지 확인
        {
            for (int row = 1; row < courseOfInterest.Count; row++)
            {
                if (courseOfInterest[row].Number.Equals(number)) return row;  //해당 과목의 관심과목 내 인덱스값 리턴
            }
            return Constants.COURSE_NOT_ON_LIST;
        }
        public bool IsDuplicateCourse(int courseIndex)   //이미 신청한 과목인가
        {
            for(int row = 1; row < courseRegistration.Count; row++)
            {
                if (courseRegistration[row].Number == courseOfInterest[row].Number) return Constants.IS_DUPLICATE_COURSE;
            }
            return Constants.IS_FIRST_APPLICATION;
        }
        public void SelectCourseOfInterest(int maxCredit)
        {
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            EnteringText text = new EnteringText();
            Keyboard keyboard = new Keyboard();
            string number;
            int courseIndex;

            while (Constants.INPUT_VALUE)
            {
                searchByFieldScreen.PrintCourserOfInterest(appliedCredit, maxCredit, courseOfInterest);  //현재 관심과목 리스트
                number = text.EnterText((int)Constants.Credit.THIRD + 20, (int)Constants.ColumnMenu.LOGO_TOP + 4, "");  //삭제할 과목 순번 입력
                if (number.Equals(Constants.ESC)) break;     //과목번호 입력 도중 esc -> 뒤로가기
                courseIndex = FindCourseIndex(number, courseOfInterest);   //입력받은 과목번호가 관심과목에 있는지 확인

                if (courseIndex == Constants.COURSE_NOT_ON_LIST)  //입력받은 번호를 담은적이 없는 경우
                {
                    searchByFieldScreen.PrintFailureMessage((int)Constants.ColumnMenu.LOGO_TOP + 2, ">신청내역에 없는 과목입니다<");
                }
                else if (IsDuplicateCourse(courseIndex))
                {
                    searchByFieldScreen.PrintFailureMessage((int)Constants.ColumnMenu.LOGO_TOP + 2, ">이미 신청한 과목입니다<");
                }
                else
                {
                    searchByFieldScreen.PrintSuccessMessage((int)Constants.ColumnMenu.LOGO_TOP + 2, ">>>과목 신청이 완료되었습니다<<<");
                    appliedCredit += Int32.Parse(courseOfInterest[courseIndex].Credit);  //수강신청한 과목 학점 ++
                    courseRegistration.Add(courseOfInterest[courseIndex]);  //수강신청목록에 추가
                }

                if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break;   //esc -> 관심과목 조회 종료
            }
        }
        public void ShowCourseRegistrationMenu(int maxCredit)
        {
            CourseOfInterestScreen courseOfInterestScreen = new CourseOfInterestScreen();
            Keyboard keyboard = new Keyboard((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FIRST);
            AddingCourseOfInterest addingCourseOfInterest = new AddingCourseOfInterest(lectureSchedule, courseRegistration);
            
            
            int menu;

            while (Constants.KEYBOARD_OPERATION)
            {
                courseOfInterestScreen.PrintRegistrationMenu();   //수강신청 메뉴 출력
                menu = keyboard.SelectTop((int)Constants.MainMenu.FIRST, (int)Constants.MainMenu.FIFTH, (int)Constants.MainMenu.STEP);
                if (menu == (int)Constants.Keyboard.ESCAPE) return;  //esc클릭 -> 로그인 화면으로 돌아감
                menu = keyboard.Top;

                switch (menu)
                {
                    case (int)Constants.MainMenu.FIRST:   //분야별 과목 검색
                        addingCourseOfInterest.CalculateCredit();
                        addingCourseOfInterest.SearchCourse(maxCredit);        //수강신청은 최대 21학점까지
                        appliedCredit = addingCourseOfInterest.AppliedCredit;  //과목검색 후 담은 학점 저장
                        break;
                    case (int)Constants.MainMenu.SECOND:  //관심과목 리스트
                        SelectCourseOfInterest(maxCredit);
                        break;
                    case (int)Constants.MainMenu.THIRD:  //담은 강의 내역
                        CourseOfInterest checkMenu = new CourseOfInterest();
                        checkMenu.ShowCourseHistory(courseRegistration);
                        break;
                    case (int)Constants.MainMenu.FOURTH:   //수강신청 시간표
                        TimeTableScreen timeTableScreen = new TimeTableScreen();
                        timeTableScreen.PrintTimeTable(courseRegistration);
                        keyboard.PressESC();    //esc -> 뒤로가기
                        break;
                    case (int)Constants.MainMenu.FIFTH:  //관심과목 삭제
                        CourseOfInterest deletionMenu = new CourseOfInterest();
                        appliedCredit = deletionMenu.DeleteCourseApplication(appliedCredit, maxCredit, courseRegistration);
                        break;

                }
            }


        }

    }
}
