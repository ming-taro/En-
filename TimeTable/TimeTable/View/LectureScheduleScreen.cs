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
        public void PrintLectureSchedule(int left, int top)
        {
            Logo logo = new Logo();

            try
            {
                // Excel Application 객체 생성
                Excel.Application application = new Excel.Application();

                // Workbook 객체 생성 및 파일 오픈 (바탕화면에 있는 excelStudy.xlsx 가져옴)
                Excel.Workbook workbook = application.Workbooks.Open(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\excelStudy.xlsx");

                // sheets에 읽어온 엑셀값을 넣기 (한 workbook 내의 모든 sheet 가져옴)
                Excel.Sheets sheets = workbook.Sheets;

                // 특정 sheet의 값 가져오기
                Excel.Worksheet worksheet = sheets["ensharp"] as Excel.Worksheet;

                // 범위 설정 (좌측 상단, 우측 하단)
                Excel.Range cellRange = worksheet.get_Range("A1", "L164") as Excel.Range;

                // 설정한 범위만큼 데이터 담기 (Value2 -셀의 기본 값 제공)
                Array data = cellRange.Cells.Value2;
 
                // 데이터 출력
                int[] leftSize = new int[] { 1, 6, 29, 38, 43, 65, 74, 83, 99, 104, 112, 130};

                logo.PrintLine(left, top);
                Console.WriteLine();

                for (int row = 1; row <= cellRange.Rows.Count; row++)
                {
                    for(int column = 1; column <= cellRange.Columns.Count; column++)
                    {
                        Console.SetCursorPosition(leftSize[column - 1], Console.CursorTop);
                        Console.Write(data.GetValue(row, column));
                    }
                    Console.WriteLine();
                }

                logo.PrintLine(Console.CursorLeft, Console.CursorTop);

                // 모든 워크북 닫기
                application.Workbooks.Close();

                // application 종료
                application.Quit();
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
