﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace TimeTable
{
    class TimeTableMenu
    {
        private List<CourseVO> lectureSchedule;
        private List<CourseVO> courseOfInterest;    //관심과목 리스트
        private List<CourseVO> courseRegistration;  //수강신청 리스트
        public TimeTableMenu()
        {
            lectureSchedule = new List<CourseVO>();
            courseOfInterest = new List<CourseVO>();
            courseRegistration = new List<CourseVO>();
        }
        private void MakeCourseList()
        {
            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Open(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\2022년도 1학기 강의시간표.xlsx");
            Excel.Sheets sheets = workbook.Sheets;
            Excel.Worksheet worksheet = sheets["sheet1"] as Excel.Worksheet;
            Excel.Range cellRange = worksheet.get_Range("A1", "L185") as Excel.Range;
            Array data = cellRange.Cells.Value2;

            for (int row = 1; row <= cellRange.Rows.Count; row++)
            {
                CourseVO courseVO = new CourseVO();

                if (data.GetValue(row, 1) != null) courseVO.Number = data.GetValue(row, 1).ToString();         //순번
                if (data.GetValue(row, 2) != null) courseVO.Department = data.GetValue(row, 2).ToString();     //학과
                if (data.GetValue(row, 3) != null) courseVO.ClassNumber = data.GetValue(row, 3).ToString();    //학수번호
                if (data.GetValue(row, 4) != null) courseVO.Distribution = data.GetValue(row, 4).ToString();  //분반
                if (data.GetValue(row, 5) != null) courseVO.CourseTitle = data.GetValue(row, 5).ToString();    //교과목명
                if (data.GetValue(row, 6) != null) courseVO.CompletionType = data.GetValue(row, 6).ToString(); //이수구분
                if (data.GetValue(row, 7) != null) courseVO.Grade = data.GetValue(row, 7).ToString();          //학년
                if (data.GetValue(row, 8) != null) courseVO.Credit = data.GetValue(row, 8).ToString();         //학점
                if (data.GetValue(row, 9) != null) courseVO.ClassDay = data.GetValue(row, 9).ToString();//요일
                if (data.GetValue(row, 10) != null) courseVO.LectureRoom = data.GetValue(row, 10).ToString();    //강의실
                if (data.GetValue(row, 11) != null) courseVO.Instructor = data.GetValue(row, 11).ToString();     //교수명
                if (data.GetValue(row, 12) != null) courseVO.Language = data.GetValue(row, 12).ToString(); //강의언어

                lectureSchedule.Add(courseVO);
            }
            application.Workbooks.Close();
            application.Quit();
        }
        public void WirteToExcel()
        {
            TimeTableScreen timeTableScreen = new TimeTableScreen();
            timeTableScreen.PrintTimeTable(courseRegistration);   //콘솔에 시간표 출력
            Console.SetCursorPosition(45, 1);
            Console.WriteLine("☞시간표가 바탕화면에 Excel파일로 저장되었습니다.([ESC]:뒤로가기)");

            //엑셀저장
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // 바탕화면 경로
            string path = Path.Combine(desktopPath, "LectureTimeTable.xlsx");

            Excel.Application application = new Excel.Application();
            Excel.Workbook workBook = application.Workbooks.Add();
            Excel.Sheets sheets = workBook.Sheets;
            Excel.Worksheet workSheet = sheets["sheet1"] as Excel.Worksheet;

            for (int row = 0; row < courseRegistration.Count; row++)  //현재 신청한 강의 리스트 엑셀에 저장
            {
                workSheet.Cells[row + 1, 1] = courseRegistration[row].Number;
                workSheet.Cells[row + 1, 2] = courseRegistration[row].Department;
                workSheet.Cells[row + 1, 3] = courseRegistration[row].ClassNumber;
                workSheet.Cells[row + 1, 4] = courseRegistration[row].Distribution;
                workSheet.Cells[row + 1, 5] = courseRegistration[row].CourseTitle;
                workSheet.Cells[row + 1, 6] = courseRegistration[row].CompletionType;
                workSheet.Cells[row + 1, 7] = courseRegistration[row].Grade;
                workSheet.Cells[row + 1, 8] = courseRegistration[row].Credit;
                workSheet.Cells[row + 1, 9] = courseRegistration[row].ClassDay;
                workSheet.Cells[row + 1, 10] = courseRegistration[row].LectureRoom;
                workSheet.Cells[row + 1, 11] = courseRegistration[row].Instructor;
                workSheet.Cells[row + 1, 12] = courseRegistration[row].Language;
            }

            timeTableScreen.WriteToExcel(workSheet, courseRegistration);  //시간표 저장

            workSheet.Columns.AutoFit();
            workBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookDefault);
            application.Workbooks.Close();
            application.Quit();

            Keyboard keyboard = new Keyboard();
            keyboard.PressESC();
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
                        addingCourseOfInterest.ShowCourseOfInterestMenu(24); //최대 24학점까지 담을 수 있음     
                        break;
                    case (int)Constants.MainMenu.THIRD:  //수강신청
                        CourseRegistraton courseRegistrationMenu = new CourseRegistraton(lectureSchedule, courseOfInterest, courseRegistration);
                        courseRegistrationMenu.ShowCourseRegistrationMenu(21);  //수강신청은 최대 21학점까지 담을 수 있음
                        break;
                    case (int)Constants.MainMenu.FOURTH:  //수강내역 조회
                        WirteToExcel();
                        break;

                }
            }
            
        }


        public void ShowTimeTableMenu()
        {
            MakeCourseList();
            courseOfInterest.Add(lectureSchedule[0]);  //관심과목담기리스트에 먼저 목록이름 저장
            courseRegistration.Add(lectureSchedule[0]);//수강목록 리스트에 먼저 목록이름 저장
            Console.ForegroundColor = ConsoleColor.White;
            Logo logo = new Logo();
            logo.PrintMain();           //메인화면 출력

            LogIn logIn = new LogIn();
            int logInResult;

            while (Constants.INPUT_VALUE)
            {
                logInResult = logIn.LogInToWebsite();

                if (logInResult == (int)Constants.Login.LOGIN_SUCCESS)
                {
                    SelectMenu();
                    logo.PrintMain();           //메인화면 출력
                }
                else if(logInResult == (int)Constants.Login.COLSE)  //프로그램 종료
                {
                    break;
                }
                else
                {
                    logo.FailureMessage(93, 14, "학번이나 비밀번호가 일치하지 않습니다.");  //로그인 실패 메세지 출력
                    logo.RemoveLine(93, 9);
                    logo.RemoveLine(93, 12);
                }
            }
        }
    }
}
