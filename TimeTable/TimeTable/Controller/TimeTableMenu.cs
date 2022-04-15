using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace TimeTable
{
    class TimeTableMenu
    {
        private List<CourseVO> lectureSchedule;
        private List<CourseVO> courseOfInterest;  //관심과목 리스트
        public TimeTableMenu()
        {
            lectureSchedule = new List<CourseVO>();
            courseOfInterest = new List<CourseVO>();
        }
        public void MakeCourseList()
        {
            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Open(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\excelStudy.xlsx");
            Excel.Sheets sheets = workbook.Sheets;
            Excel.Worksheet worksheet = sheets["ensharp"] as Excel.Worksheet;
            Excel.Range cellRange = worksheet.get_Range("A1", "L164") as Excel.Range;
            Array data = cellRange.Cells.Value2;

            for (int row = 1; row <= cellRange.Rows.Count; row++)
            {
                CourseVO courseVO = new CourseVO();

                courseVO.Number = data.GetValue(row, 1).ToString();         //순번
                courseVO.Department = data.GetValue(row, 2).ToString();     //학과
                courseVO.ClassNumber = data.GetValue(row, 3).ToString();    //학수번호
                courseVO.Distribution = data.GetValue(row, 4).ToString();  //분반
                courseVO.CourseTitle = data.GetValue(row, 5).ToString();    //교과목명
                if (data.GetValue(row, 6) != null) courseVO.Language = data.GetValue(row, 6).ToString(); //강의언어
                else courseVO.Language = "";
                courseVO.CompletionType = data.GetValue(row, 7).ToString(); //이수구분
                courseVO.Credit = data.GetValue(row, 8).ToString();         //학점
                courseVO.Grade = data.GetValue(row, 9).ToString();          //학년
                courseVO.Instructor = data.GetValue(row, 10).ToString();     //교수명
                if (data.GetValue(row, 11) != null) courseVO.ClassDay = data.GetValue(row, 11).ToString();
                else courseVO.ClassDay = "";  //요일
                if (data.GetValue(row, 12) != null) courseVO.LectureRoom = data.GetValue(row, 12).ToString();    //강의실
                else courseVO.LectureRoom = "";
                lectureSchedule.Add(courseVO);
            }
            application.Workbooks.Close();
            application.Quit();
        }

        private void SelectMenu()
        {
            TimeTableMenuScreen timeTableMenuScreen = new TimeTableMenuScreen();
            Keyboard keyboard = new Keyboard((int)Constants.MainMenu.LEFT, (int)Constants.MainMenu.FIRST);
            int menu;

            while (Constants.KEYBOARD_OPERATION)
            {
                timeTableMenuScreen.PrintMenu();                //메뉴선택화면 출력
                menu = keyboard.SelectTop((int)Constants.MainMenu.FIRST, (int)Constants.MainMenu.FOURTH, (int)Constants.MainMenu.STEP);            //강좌조회 및 수강신청 : 메뉴선택
                if (menu == (int)Constants.Keyboard.ESCAPE) return;  //esc클릭 -> 로그인 화면으로 돌아감
                menu = keyboard.Top;

                switch (menu)
                {
                    case (int)Constants.MainMenu.FIRST:   //강의 시간표 조회
                        ViewingLectureSchedule viewingLectureSchedule = new ViewingLectureSchedule(lectureSchedule);
                        viewingLectureSchedule.ViewLectureSchedule();
                        break;
                    case (int)Constants.MainMenu.SECOND:  //관심과목 담기
                        AddingCourseOfInterest addingCourseOfInterest = new AddingCourseOfInterest(lectureSchedule, courseOfInterest);
                        addingCourseOfInterest.ShowCourseOfInterestMenu();      
                        break;
                    case (int)Constants.MainMenu.THIRD:  //수강신청

                        break;
                    case (int)Constants.MainMenu.FOURTH:  //수강내역 조회

                        break;

                }
            }
            
        }

        public void ShowTimeTableMenu()
        {
            MakeCourseList();
            courseOfInterest.Add(lectureSchedule[0]);  //관심과목담기리스트에 먼저 목록이름 저장
            Console.ForegroundColor = ConsoleColor.White;
            Logo logo = new Logo();
            logo.PrintMain();           //메인화면 출력
            

            LogIn logIn = new LogIn();

            while (Constants.LOGIN_IN)
            {
                if (logIn.LogInToWebsite() == Constants.LOGIN_IN)
                {
                    SelectMenu();
                    logo.PrintMain();           //메인화면 출력
                }
                else
                {
                    logo.FailureMessage(80, 14, "(학번이나 비밀번호가 일치하지 않습니다.)");  //로그인 실패 메세지 출력
                    logo.RemoveLine(100, 9);
                    logo.RemoveLine(112, 12);
                }
            }
        }
    }
}
