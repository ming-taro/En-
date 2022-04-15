using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace TimeTable
{
    class DepartmentMajor
    {
        private List<CourseVO> courseOfInterest;
        public DepartmentMajor(List<CourseVO> courseOfInterest)
        {
            this.courseOfInterest = courseOfInterest;
        }
        public void AddCourseOfInterest(Array data, int row)
        {
            CourseVO courseVO = new CourseVO();

            courseVO.Number = data.GetValue(row, 1).ToString();         //순번
            courseVO.Department = data.GetValue(row, 2).ToString();     //학과
            courseVO.ClassNumber = data.GetValue(row, 3).ToString();    //학수번호
            courseVO.Distribution = data.GetValue(row, 4).ToString();  //분반
            courseVO.CourseTitle = data.GetValue(row, 5).ToString();    //교과목명
            if(data.GetValue(row, 6) != null) courseVO.Language = data.GetValue(row, 6).ToString(); //강의언어
            courseVO.CompletionType = data.GetValue(row, 7).ToString(); //이수구분
            courseVO.Credit = data.GetValue(row, 8).ToString();         //학점
            courseVO.Grade = data.GetValue(row, 9).ToString();          //학년
            courseVO.Instructor = data.GetValue(row, 10).ToString();     //교수명
            if (data.GetValue(row, 11) != null) courseVO.ClassDay = data.GetValue(row, 11).ToString();       //요일
            if (data.GetValue(row, 12) != null) courseVO.LectureRoom = data.GetValue(row, 12).ToString();    //강의실

            courseOfInterest.Add(courseVO);   //관심과목 리스트에 저장
        }
        public bool IsCourseInList(string courseNumber, string department)
        {
            try
            {
                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Open(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\excelStudy.xlsx");
                Excel.Sheets sheets = workbook.Sheets;
                Excel.Worksheet worksheet = sheets["ensharp"] as Excel.Worksheet;
                Excel.Range cellRange = worksheet.get_Range("A1", "L164") as Excel.Range;
                Array data = cellRange.Cells.Value2;

                for (int row = 2; row <= cellRange.Rows.Count; row++)
                {
                    if (data.GetValue(row, 1).ToString().Equals(courseNumber) && data.GetValue(row, 2).ToString().Equals(department))  //선택한 학과와 입력한 순번이 일치하는 과목을 찾음
                    {
                        AddCourseOfInterest(data, row);     //관심과목 리스트에 등록
                        application.Workbooks.Close();
                        application.Quit();
                        return Constants.IS_COURSE_IN_LIST;  
                    }
                }

                application.Workbooks.Close();
                application.Quit();
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }

            return Constants.IS_NOT_COURSE_IN_LIST;      //강의목록에 없는 순번을 입력한 경우
        }
        public void InputCourseNumber(string department)
        {
            EnteringText text = new EnteringText();
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();

            while (Constants.INPUT_VALUE)
            {
                string CourseNumber = text.EnterText((int)Constants.Credit.THIRD + 18, (int)Constants.Credit.TOP);   //담을 순번 입력
                if (CourseNumber.Equals(Constants.ESC)) break;    //입력도중 esc -> 관심과목 입력 종료
                
                if (IsCourseInList(CourseNumber, department))      //선택한 학과에 입력한 순번이 있는 경우 
                {
                    searchByFieldScreen.PrintSuccessMessage();    //관심과목 담기 성공 메세지
                    Console.Read();
                }
                else
                {

                }
            }
        }

        public void SearchMajor(ref int appliedCredit)
        {
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            searchByFieldScreen.PrintLine();

            CourseVO courseVO = new CourseVO();
            Department departmentMenu = new Department();
            departmentMenu.SelectMenu(courseVO);
            if (courseVO.Department == null) return;  //학과선택 중 esc -> 과목조회를 종료하고 분야별검색 메뉴로 돌아감

            searchByFieldScreen.PrintInputBox(appliedCredit, courseVO);   //학과선택시 -> 학과검색결과를 보여줌
            InputCourseNumber(courseVO.Department);   //순번 입력
        }
    }
}
