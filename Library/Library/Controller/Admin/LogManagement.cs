using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    class LogManagement
    {
        private LogDAO logDAO = LogDAO.GetInstance();
        private EnteringText text;
        private AdminView adminView;
        private Logo logo;
        List<LogVO> logList;

        public LogManagement(EnteringText text, AdminView adminView, Logo logo)
        {
            this.text = text;
            this.adminView = adminView;
            this.logo = logo;
            logList = new List<LogVO>();
        }
        public string InputLogId()
        {
            string logId;

            while (Constants.INPUT_VALUE)
            {
                logId = text.EnterText((int)Constants.InputField.LEFT, (int)Constants.SearchMenu.FIRST, "");

                if (logId.Equals(Constants.ESC) == Constants.IS_NOT_MATCH && (Regex.IsMatch(logId, Constants.BOOK_ID_REGEX) == Constants.IS_NOT_MATCH || Int32.Parse(logId) < 1 || Int32.Parse(logId) > logList.Count))
                {
                    logo.PrintMessage((int)Constants.SearchMenu.LEFT, (int)Constants.Exception.TOP, "(현재 조회목록에 없는 로그 아이디입니다.)", ConsoleColor.Red);
                    logo.RemoveLine((int)Constants.InputField.LEFT, (int)Constants.SearchMenu.FIRST);
                }
                else break;
            }
            return logId;
        }
        
        private void RemoveLogRecord(Keyboard keyboard)
        {
            string logId;

            if (logList.Count == 0)             //로그기록이 없는 경우 기록 없음 문구만 출력
            {
                adminView.PrintNoLogRecord();
                return;
            }
            
            adminView.PrintLogManagemnet(logList);    //로그리스트 출력

            logId = InputLogId();
            if (logId.Equals(Constants.ESC)) return;  //로그번호 입력 중 Esc -> 종료

            keyboard.PressESC();
            Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
            logo.PrintLogManagement();                     //로그 관리 화면 출력
        }
        private void SaveFile()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Constants.FILE_NAME;
            StreamWriter writer = new StreamWriter(desktopPath);

            for(int i= logList.Count - 1; i >= 0; i--)
            {
                writer.WriteLine("=====================================================================");
                writer.WriteLine(logList[i]);
            }
            writer.WriteLine("=====================================================================");
            writer.Close();

            logo.PrintLogManagement();    //로그 관리 화면 출력
            logo.PrintMessage((int)Constants.Menu.LOG_MESSAGE_LEFT, (int)Constants.Menu.SIXTH, "(바탕화면에 '로그 기록' 파일이 저장되었습니다)", ConsoleColor.Green);    //파일 저장 메세지 출력
        }
        private void DeleteFile()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Constants.FILE_NAME;

            if (File.Exists(desktopPath) == Constants.IS_EXISTING_FILE) //파일 존재여부 확인
            {
                logo.PrintMessage((int)Constants.Menu.LOG_MESSAGE_LEFT, (int)Constants.Menu.SIXTH, "     ('로그 기록' 파일이 삭제되었습니다)               ", ConsoleColor.Green);  //파일 삭제 메세지
                File.Delete(desktopPath);       //바탕화면에서 파일 삭제
            }
            else
            {
                logo.PrintMessage((int)Constants.Menu.LOG_MESSAGE_LEFT, (int)Constants.Menu.SIXTH, "   ('로그 기록' 파일이 존재하지 않습니다)               ", ConsoleColor.Red);     //파일이 존재하지 않음
            }
        }
        private void InitializeLogRecord()
        {
            logDAO.InitializeLogTable();        //로그 테이블 초기화
            logo.PrintMessage((int)Constants.Menu.LOG_MESSAGE_LEFT, (int)Constants.Menu.SIXTH, "       (로그 기록이 초기화되었습니다.)               ", ConsoleColor.Green); //로그 초기화 메세지
            logList.Clear();
        }
        public void SelectMenu(int menu, Keyboard keyboard)
        {
            switch (menu)
            {
                case (int)Constants.Menu.FIRST:   //로그 기록
                    RemoveLogRecord(keyboard);
                    logo.PrintLogManagement();
                    break;
                case (int)Constants.Menu.SECOND:  //파일 저장
                    SaveFile();
                    break;
                case (int)Constants.Menu.THIRD:   //파일 삭제
                    DeleteFile();
                    break;
                case (int)Constants.Menu.FOURTH:  //초기화
                    InitializeLogRecord();
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
