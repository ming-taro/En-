using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable.View.CourseOfInterest
{
    class CourseHistoryScreen
    {
        public void PrintCourseHistory(List<CourseVO> courseHistory)
        {
            int[] leftSize = new int[] { 1, 6, 29, 38, 43, 65, 74, 83, 99, 104, 112, 130 };

            for (int row = 0; row < courseHistory.Count; row++)
            {
                Console.SetCursorPosition(leftSize[0], Console.CursorTop);
                Console.Write(courseHistory[row].Number);
                Console.SetCursorPosition(leftSize[1], Console.CursorTop);
                Console.Write(courseHistory[row].Department);
                Console.SetCursorPosition(leftSize[2], Console.CursorTop);
                Console.Write(courseHistory[row].ClassNumber);
                Console.SetCursorPosition(leftSize[3], Console.CursorTop);
                Console.Write(courseHistory[row].Distribution);
                Console.SetCursorPosition(leftSize[4], Console.CursorTop);
                Console.Write(courseHistory[row].CourseTitle);
                Console.SetCursorPosition(leftSize[5], Console.CursorTop);
                Console.Write(courseHistory[row].Language);
                Console.SetCursorPosition(leftSize[6], Console.CursorTop);
                Console.Write(courseHistory[row].CompletionType);
                Console.SetCursorPosition(leftSize[7], Console.CursorTop);
                Console.Write(courseHistory[row].Credit);
                Console.SetCursorPosition(leftSize[8], Console.CursorTop);
                Console.Write(courseHistory[row].Grade);
                Console.SetCursorPosition(leftSize[9], Console.CursorTop);
                Console.Write(courseHistory[row].Instructor);
                Console.SetCursorPosition(leftSize[10], Console.CursorTop);
                Console.Write(courseHistory[row].ClassDay);
                Console.SetCursorPosition(leftSize[11], Console.CursorTop);
                Console.Write(courseHistory[row].LectureRoom);
                if (row == 0) Console.WriteLine();
                Console.WriteLine();
            }

        }
    }
}
