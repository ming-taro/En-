using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class ViewingLectureSchedule
    {
        private List<CourseVO> lectureSchedule;
        private CourseVO courseVO;
        public ViewingLectureSchedule(List<CourseVO> lectureSchedule)
        {
            this.lectureSchedule = lectureSchedule;
            courseVO = new CourseVO();
        }
        private void InitSearchWord()
        {
            courseVO.Department = "";
            courseVO.CompletionType = "";
            courseVO.Grade = "";
            courseVO.CourseTitle = "";
            courseVO.Instructor = "";
        }
        private void SelectMenu(int menu)
        {
            switch (menu)
            {
                case (int)Constants.ColumnMenu.FIRST:       //학과전공
                    Department departmentMenu = new Department();
                    departmentMenu.SelectMenu(courseVO);
                    break;
                case (int)Constants.ColumnMenu.SECOND:     //이수구분
                    CompletionType completionTypeMenu = new CompletionType();
                    completionTypeMenu.SelectMenu(courseVO);
                    break;
                case (int)Constants.ColumnMenu.THIRD:      //학년
                    SelectingGrade gradeMenu = new SelectingGrade();
                    gradeMenu.SelectMenu(courseVO);
                    break;
                case (int)Constants.ColumnMenu.FOURTH:     //교과목명
                    CourseTitle courseTitleMenu = new CourseTitle();
                    courseTitleMenu.InputCourseTitle(courseVO, (int)Constants.ColumnMenu.FOURTH);
                    break;
                case (int)Constants.ColumnMenu.FIFTH:       //교수명
                    CourseTitle instructorMenu = new CourseTitle();
                    instructorMenu.InputCourseTitle(courseVO, (int)Constants.ColumnMenu.FIFTH);
                    break;
                case (int)Constants.ColumnMenu.SIXTH:       //조회
                    Searching searching = new Searching();
                    searching.ShowLectureSchedule(lectureSchedule, courseVO);
                    InitSearchWord();   //조회 후 검색어 초기화
                    break;
            }
        }
        public void ViewLectureSchedule()
        {
            ViewingScheduleScreen viewingScheduleScreen = new ViewingScheduleScreen();
            viewingScheduleScreen.PrintMenu();

            Keyboard keyboard = new Keyboard((int)Constants.ColumnMenu.LEFT, (int)Constants.ColumnMenu.FIRST);
            int menu;

            while (Constants.KEYBOARD_OPERATION)
            {
                menu = keyboard.SelectTop((int)Constants.ColumnMenu.FIRST, (int)Constants.ColumnMenu.SIXTH, (int)Constants.ColumnMenu.STEP);
                if (menu == (int)Constants.Keyboard.ESCAPE) return;  //메뉴선택 중 esc -> 뒤로가기(강의조회 및 수강신청 화면으로)
                menu = keyboard.Top;
                SelectMenu(menu);  //1.학과전공  2.이수구분  3.학년  4.교과목명  5.교수명  6.조회
            }
        }

    }
}
