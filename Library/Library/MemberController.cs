using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MemberController
    {
        Value value = new Value();
        private List<MemberVO> memberList;

        public MemberController()
        {
            InitMemberList();
        }
        public void InitMemberList()   //회원목록 불러오기
        {
            memberList = new List<MemberVO>(); //회원목록

            string path = "./text/MemberList.txt";
            StreamReader reader = new StreamReader(path);
            while (reader.Peek() >= 0)
            {
                String[] text = reader.ReadLine().ToString().Split(",");     //회원 정보를 읽어옴
                memberList.Add(new MemberVO(text[0], text[1], text[2], text[3], text[4], text[5])); //초기 회원 목록을 리스트에 저장
            }
            reader.Close();
        }
        public void ControlMemberMenu()    //1.회원가입  2.로그인  3.종료(미완)
        {
            Screen screen = new Screen();
            TestingLibrary testingLibrary = new TestingLibrary();
            testingLibrary.InitCursorPosition();
            int menu;

            while (value.MEMBER_MODE)
            {
                menu = testingLibrary.SelectMenu(13, 15);  //메뉴선택
                if (menu == value.ESCAPE) break;  //메뉴선택 중 뒤로가기 -> 메인화면으로
                menu = testingLibrary.SelectMenu(13, 15); //메뉴선택 완료
                SelectMenu(menu);                         //해당 메뉴 기능 실행
            }
            //회원 모드 종료 -> 메인화면으로 돌아감
        }
        public void SelectMenu(int menu)
        {
            switch (menu)
            {
                case 13:  //회원가입(미완)
                    SignUp signUp = new SignUp();
                    break;
                case 14:  //로그인
                    SignIn member = new SignIn();
                    break;
                case 15:   //종료(미완)
                    break;
            }
        }
    }
}
