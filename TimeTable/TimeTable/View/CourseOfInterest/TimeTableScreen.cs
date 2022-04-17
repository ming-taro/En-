using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace TimeTable
{
    class TimeTableScreen
    {
        private void PrintTime()
        {
            Logo logo = new Logo();
            Console.Clear();
            logo.PrintLongLine(0, 2);
            Console.SetCursorPosition((int)Constants.Day.MONDAY, 3);
            Console.Write("월");
            Console.SetCursorPosition((int)Constants.Day.TUESDAY, 3);
            Console.Write("화");
            Console.SetCursorPosition((int)Constants.Day.WEDNESDAY, 3);
            Console.Write("수");
            Console.SetCursorPosition((int)Constants.Day.THURSDAY, 3);
            Console.Write("목");
            Console.SetCursorPosition((int)Constants.Day.FRIDAY, 3);
            Console.Write("금");

            Console.SetCursorPosition(0, 5);
            Console.WriteLine(" 09:00 ~ 09:30\n");
            Console.WriteLine(" 09:30 ~ 10:00\n");

            int hour = 10;

            for (int i = 0; i < 22; i++)
            {
                if (i % 2 == 0) Console.WriteLine(" " + hour + ":00" + " ~ " + hour + ":30");
                else Console.WriteLine(" " + hour + ":30" + " ~ " + ++hour + ":00");
                Console.WriteLine();
            }
        }
        private int GetLeft(char day)
        {
            switch (day)
            {
                case '월':
                    return (int)Constants.Day.MONDAY;
                case '화':
                    return (int)Constants.Day.TUESDAY;
                case '수':
                    return (int)Constants.Day.WEDNESDAY;
                case '목':
                    return (int)Constants.Day.THURSDAY;
                case '금':
                    return (int)Constants.Day.FRIDAY;
            }

            return (int)Constants.Day.FRIDAY;
        }
        private int GetTop(string time)
        {
            switch (time)
            {
                case "0900":
                    return (int)Constants.Time.NINE;
                case "0930":
                    return (int)Constants.Time.NINE_THIRTY;
                case "1000":
                    return (int)Constants.Time.TEN;
                case "1030":
                    return (int)Constants.Time.TEN_THIRTY;
                case "1100":
                    return (int)Constants.Time.ELEVEN;
                case "1130":
                    return (int)Constants.Time.ELEVEN_THIRTY;
                case "1200":
                    return (int)Constants.Time.TWELVE;
                case "1230":
                    return (int)Constants.Time.TWELVE_THIRTY;
                case "1300":
                    return (int)Constants.Time.ONE;
                case "1330":
                    return (int)Constants.Time.ONE_THIRTY;
                case "1400":
                    return (int)Constants.Time.TWO;
                case "1430":
                    return (int)Constants.Time.TWO_THIRTY;
                case "1500":
                    return (int)Constants.Time.THREE;
                case "1530":
                    return (int)Constants.Time.THREE_THIRTY;
                case "1600":
                    return (int)Constants.Time.FOUR;
                case "1630":
                    return (int)Constants.Time.FOUR_THIRTY;
                case "1700":
                    return (int)Constants.Time.FIVE;
                case "1730":
                    return (int)Constants.Time.FIVE_THIRTY;
                case "1800":
                    return (int)Constants.Time.SIX;
                case "1830":
                    return (int)Constants.Time.SIX_THIRTY;
                case "1900":
                    return (int)Constants.Time.SEVEN;
                case "1930":
                    return (int)Constants.Time.SEVEN_THIRTY;
                case "2000":
                    return (int)Constants.Time.EIGHT;
                case "2030":
                    return (int)Constants.Time.EIGHT_THIRTY;
            }

            return (int)Constants.Time.EIGHT_THIRTY;
        }
        private int CalculateLine(string time)   //시작, 끝나는 시간의 시간차를 계산해 시간표에 몇 줄을 출력할지 계산 
        {
            int endTime = Int32.Parse(time) % 10000;       //time이 "10301130"(10:30~11:30) -> endTime은 1130, startTime은 1030
            endTime = endTime % 100 + (endTime / 100) * 60;  //10:30의 총 분 수는 30 + 10*60 = 630

            int startTime = Int32.Parse(time) / 10000;
            startTime = startTime % 100 + (startTime / 100) * 60;   //11:30의 총 분 수는 30 + 11*60 = 690

            int line = (endTime - startTime) / 30;   //총 출력할 줄 수는 (690 - 630)/30 = 2줄, 즉 10:30~11:00, 11:00~11:30 라인 2개

            return line;
        }
        public void PrintCourse(int left, int top, int line, string courseTitle, string LectureRoom)
        {
            for(int row = 0; row < line; row++)
            {
                Console.SetCursorPosition(left, top++);
                Console.Write(courseTitle);             //과목명
                Console.SetCursorPosition(left, top++);
                Console.Write(LectureRoom);               //강의실
            }
        }
        public void PrintTimeTable(List<CourseVO> lectureSchedule)
        {
            PrintTime();

            int dayLeft;
            int dayTop;
            string classDay;
            string day;
            int line;

            string time;

            for (int row = 1; row < lectureSchedule.Count; row++)
            {
                classDay = lectureSchedule[row].ClassDay;
                day = Regex.Replace(classDay, @"[^월|화|수|목|금]", "");  //요일
                time = Regex.Replace(classDay, @"[^0-9]", "");            //시간

                                                             //요일이 하루, 시간이 한 타임인 강의(ex: 화 09:00~10:00)
                dayLeft = GetLeft(day[0]);                   //첫번째 요일에 해당하는 세로 라인의 left값
                dayTop = GetTop(time.Substring(0, 4));       //수업의 시작시간에 해당하는 가로 라인의 top값
                line = CalculateLine(time.Substring(0,8));                  //시작점으로부터 출력할 횟수
                PrintCourse(dayLeft, dayTop, line, lectureSchedule[row].CourseTitle, lectureSchedule[row].LectureRoom);

                if (day.Length ==  2 && time.Length == 8)    //요일이 이틀, 시간이 같은 강의(ex: 화 목 09:00~10:00)
                {
                    dayLeft = GetLeft(day[1]);                   //두번째 요일에 해당하는 세로 라인의 left값
                    PrintCourse(dayLeft, dayTop, line, lectureSchedule[row].CourseTitle, lectureSchedule[row].LectureRoom);
                }
                else if(day.Length == 2 && time.Length == 16)//요일이 이틀, 시간대가 다른 강의(ex: 화 09:00~10:00 목 10:00~11:00)
                {
                    dayLeft = GetLeft(day[1]);                   //두번째 요일에 해당하는 세로 라인의 left값
                    dayTop = GetTop(time.Substring(8, 4));       //수업의 시작시간에 해당하는 가로 라인의 top값
                    line = CalculateLine(time.Substring(8, 8));  //시작점으로부터 출력할 횟수
                    PrintCourse(dayLeft, dayTop, line, lectureSchedule[row].CourseTitle, lectureSchedule[row].LectureRoom);
                }
            }
        }



        //-------엑셀 저장
        public void WirteCourse(Excel.Worksheet workSheet, int column, int row, int line, string courseTitle, string LectureRoom)
        {
            for (int count = 0; count < line; count++)
            {
                workSheet.Cells[row + count * 2, column] = courseTitle;
                workSheet.Cells[row + count * 2 + 1, column] = LectureRoom;
            }
        }
        public int GetColumn(char day)  //엑셀의 행값
        {
            switch (day)
            {
                case '월':
                    return 2;
                case '화':
                    return 3;
                case '수':
                    return 4;
                case '목':
                    return 5;
                case '금':
                    return 6;
            }

            return 6;
        }

        public void WriteToExcel(Excel.Worksheet workSheet, List<CourseVO> lectureSchedule)
        {
            workSheet.Cells[15, 2] = "월";
            workSheet.Cells[15, 3] = "화";
            workSheet.Cells[15, 4] = "수";
            workSheet.Cells[15, 5] = "목";
            workSheet.Cells[15, 6] = "금";

            workSheet.Cells[16, 1] = "09:00 ~ 09:30";
            workSheet.Cells[18, 1] = "09:30 ~ 10:00";

            int hour = 10;
            int row = 20;

            for (int i = 0; i < 22; i++)
            {
                if (i % 2 == 0) workSheet.Cells[row, 1] = hour + ":00" + " ~ " + hour + ":30";
                else workSheet.Cells[row, 1] = hour + ":30" + " ~ " + ++hour + ":00";
                row += 2;
            }

            int dayColumn;
            int dayRow;
            int line;
            string classDay;
            string day;
            string time;

            for (row = 1; row < lectureSchedule.Count; row++)
            {
                classDay = lectureSchedule[row].ClassDay;
                day = Regex.Replace(classDay, @"[^월|화|수|목|금]", "");  //요일
                time = Regex.Replace(classDay, @"[^0-9]", "");            //시간

                //요일이 하루, 시간이 한 타임인 강의(ex: 화 09:00~10:00)
                dayColumn = GetColumn(day[0]);                 //첫번째 요일에 해당하는 행값
                dayRow = GetTop(time.Substring(0, 4)) + 11;       //수업의 시작시간에 해당하는 가로 라인의 top값
                line = CalculateLine(time.Substring(0, 8));                  //시작점으로부터 출력할 횟수
                WirteCourse(workSheet, dayColumn, dayRow, line, lectureSchedule[row].CourseTitle, lectureSchedule[row].LectureRoom);

                if (day.Length == 2 && time.Length == 8)    //요일이 이틀, 시간이 같은 강의(ex: 화 목 09:00~10:00)
                {
                    dayColumn = GetColumn(day[1]);                   //두번째 요일에 해당하는 세로 라인의 left값
                    WirteCourse(workSheet, dayColumn, dayRow, line, lectureSchedule[row].CourseTitle, lectureSchedule[row].LectureRoom);
                }
                else if (day.Length == 2 && time.Length == 16)//요일이 이틀, 시간대가 다른 강의(ex: 화 09:00~10:00 목 10:00~11:00)
                {
                    dayColumn = GetColumn(day[1]);                   //두번째 요일에 해당하는 세로 라인의 left값
                    dayRow = GetTop(time.Substring(8, 4)) + 11;       //수업의 시작시간에 해당하는 가로 라인의 top값
                    line = CalculateLine(time.Substring(8, 8));  //시작점으로부터 출력할 횟수
                    WirteCourse(workSheet, dayColumn, dayRow, line, lectureSchedule[row].CourseTitle, lectureSchedule[row].LectureRoom);
                }
            }

        }
    }
}
