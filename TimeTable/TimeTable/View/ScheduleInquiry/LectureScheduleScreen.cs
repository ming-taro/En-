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
        private bool IsContainingWord(string cellData, string searchWord)
        {
            if (searchWord == null || cellData.Contains(searchWord)) return Constants.IS_MEETING_CONDITION;

            return Constants.IS_NOT_MEETING_CONDITION;
        }
        private bool IsValueMeetCondition(Array data, int row, string department, string completionType, string grade, string courseTitle, string instructor)
        {
            if (IsContainingWord(data.GetValue(row, 2).ToString(), department) == Constants.IS_NOT_MEETING_CONDITION) return Constants.IS_NOT_MEETING_CONDITION;
            if (IsContainingWord(data.GetValue(row, 7).ToString(), completionType) == Constants.IS_NOT_MEETING_CONDITION) return Constants.IS_NOT_MEETING_CONDITION;
            if (IsContainingWord(data.GetValue(row, 9).ToString(), grade) == Constants.IS_NOT_MEETING_CONDITION) return Constants.IS_NOT_MEETING_CONDITION;
            if (IsContainingWord(data.GetValue(row, 5).ToString(), courseTitle) == Constants.IS_NOT_MEETING_CONDITION) return Constants.IS_NOT_MEETING_CONDITION;
            if (IsContainingWord(data.GetValue(row, 10).ToString(), instructor) == Constants.IS_NOT_MEETING_CONDITION) return Constants.IS_NOT_MEETING_CONDITION;

            return Constants.IS_MEETING_CONDITION;
        }
        public void PrintLectureSchedule(int top, string department, string completionType, string grade, string courseTitle, string instructor)
        {
            Logo logo = new Logo();

            try
            {
                Excel.Application application = new Excel.Application(); 
                Excel.Workbook workbook = application.Workbooks.Open(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\excelStudy.xlsx");
                Excel.Sheets sheets = workbook.Sheets;
                Excel.Worksheet worksheet = sheets["ensharp"] as Excel.Worksheet;
                Excel.Range cellRange = worksheet.get_Range("A1", "L164") as Excel.Range;
                Array data = cellRange.Cells.Value2;
                
                int[] leftSize = new int[] { 1, 6, 29, 38, 43, 65, 74, 83, 99, 104, 112, 130};

                logo.PrintLongLine(0, top);
                for (int row = 1; row <= cellRange.Rows.Count; row++)
                {
                    if (row != 1 && IsValueMeetCondition(data, row, department, completionType, grade, courseTitle, instructor) == Constants.IS_NOT_MEETING_CONDITION)
                    {
                        continue;  //조건에 맞지 않다면 출력X
                    }

                    for (int column = 1; column <= cellRange.Columns.Count; column++)
                    {
                        Console.SetCursorPosition(leftSize[column - 1], Console.CursorTop);
                        Console.Write(data.GetValue(row, column));
                    }
                    if (row == 1) Console.WriteLine();
                    Console.WriteLine();
                }
                logo.PrintLongLine(0, Console.CursorTop);

                application.Workbooks.Close();
                application.Quit();
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
