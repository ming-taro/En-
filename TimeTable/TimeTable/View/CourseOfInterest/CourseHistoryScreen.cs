using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class CourseHistoryScreen
    {
        public void PrintMyCourseHistory(List<CourseVO> courseOfInterest)
        {
            Logo logo = new Logo();
            Console.Clear();
            logo.PrintMenu((int)Constants.ColumnMenu.LOGO_LEFT, (int)Constants.ColumnMenu.LOGO_TOP, "[담은 강의 내역]");

            CourseHistoryScreen courseHistoryScreen = new CourseHistoryScreen();
            courseHistoryScreen.PrintCourseHistory((int)Constants.ColumnMenu.LOGO_TOP + 6, courseOfInterest);           //현재 담겨진 관심과목 내역

            logo.PrintMenu((int)Constants.RowMenu.FIFTH, (int)Constants.ColumnMenu.LOGO_TOP + 4, "[ESC]: 뒤로가기");
        }

        public void PrintCourseHistory(int top, List<CourseVO> courseHistory)
        {
            int[] leftSize = new int[] { 1, 6, 29, 38, 43, 65, 74, 83, 99, 104, 112, 130 };

            Logo logo = new Logo();
            logo.PrintLongLine(0, top);

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
            logo.PrintLongLine(0, Console.CursorTop);
        }
    }
}
