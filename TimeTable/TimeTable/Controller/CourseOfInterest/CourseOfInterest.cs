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
        public int DeleteCourseApplication(int appliedCredit, List<CourseVO> courseOfInterest)   //관심과목에 담았던 내역 삭제
        {
            SearchByFieldScreen searchByFieldScreen = new SearchByFieldScreen();
            EnteringText text = new EnteringText();
            Keyboard keyboard = new Keyboard();
            string number;
            int courseIndex;

            while (Constants.INPUT_VALUE)
            {
                searchByFieldScreen.PrintDeletionResult(appliedCredit, courseOfInterest);  //현재 관심과목 리스트
                number = text.EnterText((int)Constants.Credit.THIRD + 20, (int)Constants.ColumnMenu.LOGO_TOP + 4, "");  //삭제할 과목 순번 입력
                if (number.Equals(Constants.ESC)) break;     //과목번호 입력 도중 esc -> 뒤로가기
                courseIndex = FindCourseIndex(number, courseOfInterest);   //입력받은 과목번호가 관심과목에 있는지 확인

                if (courseIndex == Constants.COURSE_NOT_ON_LIST)  //입력받은 번호를 담은적이 없는 경우
                {
                    searchByFieldScreen.PrintFailureMessage((int)Constants.ColumnMenu.LOGO_TOP + 2, ">신청내역에 없는 과목입니다<");
                }
                else
                {
                    searchByFieldScreen.PrintSuccessMessage((int)Constants.ColumnMenu.LOGO_TOP + 2, ">>>해당 과목의 삭제가 완료되었습니다<<<");
                    appliedCredit -= Int32.Parse(courseOfInterest[courseIndex].Credit);  //관심과목 학점 --
                    courseOfInterest.RemoveAt(courseIndex);  //관심과목 목록에서 삭제
                }

                if (keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break;   //esc -> 과목삭제 종료
            }

            return appliedCredit;
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
