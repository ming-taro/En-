﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class AddingCourseOfInterest
    {
        private List<CourseVO> lectureSchedule;   //강의목록
        private List<CourseVO> courseOfInterest;  //관심과목 리스트
        private int appliedCredit;                //현재 관심과목 리스트에 있는 강좌의 총 학점
        public int AppliedCredit
        {
            get { return appliedCredit; }
            set { appliedCredit = value; }
        }
        public AddingCourseOfInterest(List<CourseVO> lectureSchedule, List<CourseVO> courseOfInterest)
        {
            this.lectureSchedule = lectureSchedule;
            this.courseOfInterest = courseOfInterest;
            appliedCredit = 0;
        }
        public void CalculateCredit()  //현재 관심과목에 담겨있는 신청학점 계산
        {
            for(int row = 1; row<courseOfInterest.Count; row++)
            {
                appliedCredit += Int32.Parse(courseOfInterest[row].Credit);
            }
        }
        private void InitSearchWord(CourseVO courseVO)
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();
            searchCategoryScreen.PrintMenu("☞학수번호/분반");

            courseVO.Department = "";
            courseVO.CompletionType = "";
            courseVO.Grade = "";
            courseVO.CourseTitle = "";
            courseVO.Instructor = "";
            courseVO.ClassNumber = "";
            courseVO.Distribution = "";
        }

        public void SelectMenu(int menu, int maxCredit, CourseVO courseVO)
        {
            switch (menu)
            {
                case (int)Constants.ColumnMenu.FIRST:       //학과전공
                    Department departmentMenu = new Department();
                    departmentMenu.SelectMenu(courseVO);
                    break;
                case (int)Constants.ColumnMenu.SECOND:     //학수번호
                    ClassNumber classNumber = new ClassNumber(lectureSchedule, courseOfInterest, appliedCredit);
                    classNumber.SearchClassNumber(courseVO);
                    break;
                case (int)Constants.ColumnMenu.THIRD:      //학년
                    SelectingGrade gradeMenu = new SelectingGrade();
                    gradeMenu.SelectMenu(courseVO, (int)Constants.ColumnMenu.THIRD);
                    break;
                case (int)Constants.ColumnMenu.FOURTH:     //교과목명
                    SelectingCourseTitle courseTitleMenu = new SelectingCourseTitle();
                    courseTitleMenu.InputCourseTitle(courseVO, (int)Constants.ColumnMenu.FOURTH);
                    break;
                case (int)Constants.ColumnMenu.FIFTH:       //교수명
                    SelectingCourseTitle instructorMenu = new SelectingCourseTitle();
                    instructorMenu.InputCourseTitle(courseVO, (int)Constants.ColumnMenu.FIFTH);
                    break;
            }
        }
        public void SearchResult(CourseVO courseVO, int maxCredit)//조회
        {
            Searching searching = new Searching(lectureSchedule, courseOfInterest, appliedCredit);
            appliedCredit = searching.InputCourseNumber(courseVO, maxCredit);
            InitSearchWord(courseVO);   //조회 후 검색어 초기화
        }
        public void SearchCourse(int maxCredit)
        {
            SearchCategoryScreen searchCategoryScreen = new SearchCategoryScreen();
            Keyboard keyboard = new Keyboard((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.FIRST);
            CourseVO courseVO = new CourseVO();
            int menu;
            searchCategoryScreen.PrintMenu("☞학수번호/분반");  //관심분야 검색 화면 출력

            while (Constants.KEYBOARD_OPERATION)
            {
                menu = keyboard.SelectTop((int)Constants.ColumnMenu.FIRST, (int)Constants.ColumnMenu.SIXTH, (int)Constants.ColumnMenu.STEP);
                if (menu == (int)Constants.Keyboard.ESCAPE) return;  //메뉴선택 중 esc -> 뒤로가기(강의조회 및 수강신청 화면으로)
                menu = keyboard.Top;

                if (menu == (int)Constants.ColumnMenu.SIXTH) SearchResult(courseVO, maxCredit);  //조회
                else SelectMenu(menu, maxCredit, courseVO);    //검색할 분야 선택
            }
        }
        public void ShowCourseOfInterestMenu(int maxCredit)
        {
            CalculateCredit();  //현재 관심과목에 담은 학점 계산

            CourseOfInterestScreen courseOfInterestScreen = new CourseOfInterestScreen();
            Keyboard keyboard = new Keyboard((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FIRST);
            int menu;

            while (Constants.KEYBOARD_OPERATION)
            {
                courseOfInterestScreen.PrintMenu(); 
                menu = keyboard.SelectTop((int)Constants.MainMenu.FIRST, (int)Constants.MainMenu.FOURTH, (int)Constants.MainMenu.STEP);
                if (menu == (int)Constants.Keyboard.ESCAPE) return;  //esc클릭 -> 로그인 화면으로 돌아감
                menu = keyboard.Top;

                switch (menu)
                {
                    case (int)Constants.MainMenu.FIRST:   //분야별 과목 검색
                        SearchCourse(maxCredit);
                        break;
                    case (int)Constants.MainMenu.SECOND:  //담은 강의 내역
                        CourseOfInterest checkMenu = new CourseOfInterest();
                        checkMenu.ShowCourseHistory(courseOfInterest);
                        break;
                    case (int)Constants.MainMenu.THIRD:   //관심과목 시간표
                        TimeTableScreen timeTableScreen = new TimeTableScreen();
                        timeTableScreen.PrintTimeTable(courseOfInterest);
                        keyboard.PressESC();    //esc -> 뒤로가기
                        break;
                    case (int)Constants.MainMenu.FOURTH:  //관심과목 삭제
                        CourseOfInterest deletionMenu = new CourseOfInterest();
                        appliedCredit = deletionMenu.DeleteCourseApplication(appliedCredit, maxCredit, courseOfInterest);
                        break;

                }
            }
            

        }
    }
}
