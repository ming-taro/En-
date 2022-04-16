using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class CourseOfInterest
    {
        public int FindCourseIndex(string number, List<CourseVO> courseOfInterest)  //현재 관심과목에 해당 순번이 존재하는지 확인
        {
            for(int row = 1; row < courseOfInterest.Count; row++)
            {
                if (courseOfInterest[row].Number.Equals(number)) return row;  //해당 과목의 관심과목 내 인덱스값 리턴
            }
            return Constants.COURSE_NOT_ON_LIST;
        }
        public void DeleteCourseApplication(int appliedCredit, List<CourseVO> courseOfInterest)   //관심과목에 담았던 내역 삭제
        {
            CourseHistoryScreen courseHistoryScreen = new CourseHistoryScreen();
            courseHistoryScreen.PrintMyCourseHistory(courseOfInterest);        //과목리스트 출력

            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            searchByFieldScreen.PrintDeletionInputBox(appliedCredit);

            Console.SetCursorPosition((int)Constants.Credit.THIRD + 20, (int)Constants.ColumnMenu.LOGO_TOP + 6);
            EnteringText text = new EnteringText();
            string number = text.EnterText((int)Constants.Credit.THIRD + 20, (int)Constants.ColumnMenu.LOGO_TOP + 6, "");  //삭제할 과목 순번 입력
            if (number.Equals(Constants.ESC)) return;     //과목번호 입력 도중 esc -> 뒤로가기

            int courseIndex = FindCourseIndex(number, courseOfInterest);
            if (courseIndex == Constants.COURSE_NOT_ON_LIST)  //입력받은 번호를 담은적이 없는 경우
            {

            }
            else
            {
                courseOfInterest.RemoveAt(courseIndex);
            }
        }

        public void ShowCourseHistory(List<CourseVO> courseOfInterest)
        {
            CourseHistoryScreen courseHistoryScreen = new CourseHistoryScreen();
            courseHistoryScreen.PrintMyCourseHistory(courseOfInterest);

            Keyboard keyboard = new Keyboard();
            keyboard.PressESC();               //과목리스트 출력 후 esc -> 뒤로가기
        }
    }
}
