using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TimeTable
{
    class SelectingCourseTitle
    {
        public void InputCourseTitle(CourseVO courseVO, int top)
        {
            Logo logo = new Logo();
            logo.PrintMenu((int)Constants.RowMenu.FIRST, top, "☞2글자 이상 입력:");
            
            EnteringText text = new EnteringText();
            string title;

            while (Constants.INPUT_VALUE)
            {
                logo.RemoveLine(Constants.LEFT_VALUE_OF_COURSE_INPUT, top); //이전 입력기록 지움
                title = text.EnterText(Constants.LEFT_VALUE_OF_COURSE_INPUT, top, ""); //검색어 입력

                if (title.Equals(Constants.ESC))  //esc -> 입력중 뒤로가기
                {
                    logo.RemoveInput(Constants.LEFT_VALUE_OF_COURSE_INPUT, top);   //입력값, 경고메세지 지움
                    return;
                }
                else if (string.IsNullOrEmpty(title) == Constants.IS_NOT_NULL_OR_EXCEPTION && Regex.IsMatch(title, @"^[a-zA-Z가-힣\#\+]{2,20}"))
                {
                    logo.RemoveLine(Constants.LEFT_VALUE_OF_COURSE_INPUT, top + 1);//경고메세지 지움
                    break;
                }
                logo.FailureMessage(Constants.LEFT_VALUE_OF_COURSE_INPUT, top + 1, "영어,한글,+,#만 가능합니다.");  //경고메세지 출력
            }

            if (top == (int)Constants.ColumnMenu.FOURTH) courseVO.CourseTitle = title;  //입력값 저장
            else courseVO.Instructor = title;
        }
    }
}
