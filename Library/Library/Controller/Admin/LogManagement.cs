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
        private Logo logo;
        private AdminView adminView;
        private Exception exception;
        List<LogVO> logList;

        public LogManagement(Logo logo, AdminView adminView, Exception exception)
        {
            this.logo = logo;
            this.adminView = adminView;
            this.exception = exception;
            logList = new List<LogVO>();
        }
        private void ManageLogList(Keyboard keyboard)
        {
            if (logList.Count == 0) adminView.PrintNoLogRecord();
            else adminView.PrintLogManagemnet(logList);    //로그리스트 출력

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
            exception.PrintSaveFile();    //파일 저장 메세지 출력
        }
        private void DeleteFile()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Constants.FILE_NAME;

            if (File.Exists(desktopPath) == Constants.IS_EXISTING_FILE) //파일 존재여부 확인
            {
                exception.PrintFileDeletion();  //파일 삭제 메세지
                File.Delete(desktopPath);       //바탕화면에서 파일 삭제
            }
            else
            {
                exception.PrintNoLogFile();     //파일이 존재하지 않음
            }
        }
        private void InitializeLogRecord()
        {
            logDAO.InitializeLogTable();        //로그 테이블 초기화
            exception.PrintLogInitialization(); //로그 초기화 메세지
            logList.Clear();
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
