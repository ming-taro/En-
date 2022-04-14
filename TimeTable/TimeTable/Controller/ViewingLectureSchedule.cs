using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class ViewingLectureSchedule
    {

        public void ViewLectueSchedule()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Logo logo = new Logo();
            logo.PrintMain();           //메인화면 출력

            LogIn logIn = new LogIn();

            while (Constants.LOGIN_IN)
            {
                if (logIn.LogInToWebsite() == Constants.LOGIN_IN) break;
                else
                {
                    logo.FailureMessage(60, 14, "(학번이나 비밀번호가 일치하지 않습니다.)");  //로그인 실패 메세지 출력
                    logo.RemoveLine(80, 9);
                    logo.RemoveLine(92, 12);
                }
            }
        }
    }
}
