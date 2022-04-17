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
        public void SelectCourseOfInterest()
        {



        }
        public void ShowCourseRegistrationMenu(int maxCredit)
        {
            CourseOfInterestScreen courseOfInterestScreen = new CourseOfInterestScreen();
            Keyboard keyboard = new Keyboard((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FIRST);
            AddingCourseOfInterest addingCourseOfInterest = new AddingCourseOfInterest(lectureSchedule, courseRegistration);
            
            addingCourseOfInterest.CalculateCredit();
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
                        addingCourseOfInterest.SearchCourse(maxCredit);   //수강신청은 최대 21학점까지
                        appliedCredit = addingCourseOfInterest.AppliedCredit;  //과목검색 후 담은 학점 저장
                        break;
                    case (int)Constants.MainMenu.SECOND:  //관심과목 리스트

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
