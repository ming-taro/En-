using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace TimeTable
{
    class LectureScheduleScreen
    {
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
        public void PrintLectureSchedule(int top, List<CourseVO> lectureSchedule, CourseVO courseVO)
        {
            Logo logo = new Logo();

            int[] leftSize = new int[] { 1, 6, 25, 34, 39, 65, 74, 87, 92, 97, 108, 130};

            logo.PrintLongLine(0, top);
            for (int row = 0; row < lectureSchedule.Count; row++)
            {
                if (row != 0 && IsValueMeetCondition(lectureSchedule[row], courseVO) == Constants.IS_NOT_MEETING_CONDITION)
                {
                    continue;  //조건에 맞지 않다면 출력X
                }
                Console.SetCursorPosition(leftSize[0], Console.CursorTop);
                Console.Write(lectureSchedule[row].Number);  
                Console.SetCursorPosition(leftSize[1], Console.CursorTop);
                Console.Write(lectureSchedule[row].Department);
                Console.SetCursorPosition(leftSize[2], Console.CursorTop);
                Console.Write(lectureSchedule[row].ClassNumber);
                Console.SetCursorPosition(leftSize[3], Console.CursorTop);
                Console.Write(lectureSchedule[row].Distribution);
                Console.SetCursorPosition(leftSize[4], Console.CursorTop);
                Console.Write(lectureSchedule[row].CourseTitle);
                Console.SetCursorPosition(leftSize[5], Console.CursorTop);
                Console.Write(lectureSchedule[row].Language);
                Console.SetCursorPosition(leftSize[6], Console.CursorTop);
                Console.Write(lectureSchedule[row].CompletionType);
                Console.SetCursorPosition(leftSize[7], Console.CursorTop);
                Console.Write(lectureSchedule[row].Credit);
                Console.SetCursorPosition(leftSize[8], Console.CursorTop);
                Console.Write(lectureSchedule[row].Grade);
                Console.SetCursorPosition(leftSize[9], Console.CursorTop);
                Console.Write(lectureSchedule[row].Instructor);
                Console.SetCursorPosition(leftSize[10], Console.CursorTop);
                Console.Write(lectureSchedule[row].ClassDay);
                Console.SetCursorPosition(leftSize[11], Console.CursorTop);
                Console.Write(lectureSchedule[row].LectureRoom);
                if (row == 0) Console.WriteLine();
                Console.WriteLine();
            }
            logo.PrintLongLine(0, Console.CursorTop);
        }
    }
}
