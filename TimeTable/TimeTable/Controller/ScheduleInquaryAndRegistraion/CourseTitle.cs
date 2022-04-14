using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TimeTable
{
    class CourseTitle
    {
        public void InputCourseTitle(ref string courseTitle)
        {
            Logo logo = new Logo();
            logo.PrintMenu((int)Constants.Grade.ALL, (int)Constants.LectureSchedule.COURSE_TITLE, "☞2글자 이상 입력:");
            logo.RemoveLine(42, (int)Constants.LectureSchedule.COURSE_TITLE + 1);

            EnteringText text = new EnteringText();
            string title;

            while (Constants.INPUT_VALUE)
            {
                logo.RemoveLine(42, (int)Constants.LectureSchedule.COURSE_TITLE);
                title = text.EnterText(42, (int)Constants.LectureSchedule.COURSE_TITLE);

                if (title.Equals(Constants.ESC))
                {
                    logo.RemoveLine(42, (int)Constants.LectureSchedule.COURSE_TITLE + 1);   //경고메세지 지움
                    courseTitle = "";
                    return;
                }
                else if (string.IsNullOrEmpty(title) == Constants.IS_NOT_NULL_OR_EXCEPTION && Regex.IsMatch(title, @"^[a-zA-Z가-힣\#\+]{2,20}"))
                {
                    courseTitle = title;
                    return;
                }
                logo.FailureMessage(42, (int)Constants.LectureSchedule.COURSE_TITLE + 1, "두 글자 이상 입력해주세요.");
            }
        }
    }
}
