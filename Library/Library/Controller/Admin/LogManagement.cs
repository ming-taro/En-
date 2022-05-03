using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LogManagement
    {
        private LogDAO logDAO = LogDAO.GetInstance();
        private AdminView adminView;
        private Logo logo;
        List<LogVO> logList;

        public LogManagement(AdminView adminView, Logo logo)
        {
            this.adminView = adminView;
            this.logo = logo;
            logList = new List<LogVO>();
        }
        public void ManageLogList(Keyboard keyboard)
        {
            adminView.PrintLogManagemnet(logList);  //로그리스트 출력
            keyboard.PressESC();
            logo.PrintLogManagement();                //로그 관리 화면 출력
        }
        public void SaveFile()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/로그 기록.txt";
            StreamWriter writer = new StreamWriter(desktopPath);

            for(int i= logList.Count - 1; i >= 0; i--)
            {
                writer.WriteLine("=====================================================================");
                writer.WriteLine(logList[i]);
            }
            writer.WriteLine("=====================================================================");
            writer.Close();

            logo.PrintLogManagement();    //로그 관리 화면 출력
            logo.PrintMessage((int)Constants.Menu.LOG_MESSAGE_LEFT, (int)Constants.Menu.SIXTH, "(바탕화면에 '로그 기록' 파일이 저장되었습니다)", ConsoleColor.Green);
        }
        public void DeleteFile()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/로그 기록.txt";
            if (File.Exists(desktopPath) == Constants.IS_EXISTING_FILE)
            {
                logo.PrintMessage((int)Constants.Menu.LOG_MESSAGE_LEFT, (int)Constants.Menu.SIXTH, "     ('로그 기록' 파일이 삭제되었습니다)               ", ConsoleColor.Red);
                File.Delete(desktopPath);
            }
            else
            {
                logo.PrintMessage((int)Constants.Menu.LOG_MESSAGE_LEFT, (int)Constants.Menu.SIXTH, "   ('로그 기록' 파일이 존재하지 않습니다)               ", ConsoleColor.Red);

            }
        }
        public void SelectMenu(int menu, Keyboard keyboard)
        {
            switch (menu)
            {
                case (int)Constants.Menu.FIRST:   //로그 기록
                    ManageLogList(keyboard);
                    break;
                case (int)Constants.Menu.SECOND:  //파일 저장
                    SaveFile();
                    break;
                case (int)Constants.Menu.THIRD:   //파일 삭제
                    DeleteFile();
                    break;
                case (int)Constants.Menu.FOURTH:  //초기화

                    break;
            }
        }
        public void ManageLog(Keyboard keyboard)
        {
            int menu;
            logList = logDAO.GetLogList();                //로그리스트
            logo.PrintLogManagement();                    //로그 관리 화면 출력

            while (Constants.KEYBOARD_OPERATION)          //로그인 성공
            {
                keyboard.InitCursorPosition();            //커서 위치 조정

                menu = keyboard.SelectMenu((int)Constants.Menu.FIRST, (int)Constants.Menu.FOURTH, (int)Constants.Menu.STEP); //메뉴선택 완료
                if (menu == (int)Constants.Keyboard.ESCAPE) break;  //관리자 메뉴 선택중 esc -> 관리자 모드 종료(메인으로 돌아감)

                menu = keyboard.Top;                      //Enter를 눌렀을 때의 커서값 == 선택한 메뉴 
                SelectMenu(menu, keyboard);               //해당 메뉴 기능 실행
            }
        }
    }
}
