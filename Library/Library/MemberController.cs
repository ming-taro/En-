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
        public List<MemberVO> memberList;

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
        public void ControlMemberMode(int maxTop)    //1.회원가입  2.로그인  3.종료(미완)
        {
            Screen screen = new Screen();
            TestingLibrary testingLibrary = new TestingLibrary();
            testingLibrary.InitCursorPosition();
            int menu;

            while (value.MEMBER_MODE)
            {
                if (maxTop == 15) screen.PrintMemberMenu();//1.로그인  2.회원가입  3.종료
                else screen.PrintMemberMode();    //1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료
                menu = testingLibrary.SelectMenu(13, maxTop);  //메뉴선택
                if (menu == value.ESCAPE) break;  //메뉴선택 중 뒤로가기 -> 메인화면으로
                menu = testingLibrary.GetTop();   //메뉴의 해당 커서값
                if (maxTop == 15) SelectMenu1(menu); //1.로그인  2.회원가입  3.종료
                else SelectMenu2(menu);              //1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료
            }
            //회원 모드 종료 -> 메인화면으로 돌아감
        }
        public void SelectMenu1(int menu)
        {
            switch (menu)
            {
                case 13:  //로그인
                    ControlSignIn();
                    break;
                case 14:  //회원가입(미완)
                    SignUp signUp = new SignUp();
                    break;
                case 15:   //종료(미완)
                    break;
            }
        }
        public void SelectMenu2(int menu)
        {
            switch (menu)
            {
                case 13:  //도서검색(미완)
                    AdminController adminController = new AdminController();
                    adminController.SelectMenu(menu);
                    break;
                case 14:  //도서대여(미완)

                    break;
                case 15:   //도서반납(미완)
                    break;
                case 16:   //개인정보수정(미완)
                    break;
                case 17:   //종료(미완)
                    break;
            }
        }
        public bool IsCorrectMemberShip(string id, string password)
        {
            for(int i=0; i<memberList.Count; i++)
            {
                if(memberList[i].Id.Equals(id) && memberList[i].Password.Equals(password))
                {
                    return value.CORRECT_MEMBERSHIP;
                }
            }
            return value.WRONG_MEMBERSHIP;
        }
        public void ControlSignIn()
        {
            string id = "";
            string password = "";
            SignIn member = new SignIn();  //회원 로그인 화면으로 이동)

            bool input = value.WRONG_VALUE;

            while (input == value.WRONG_VALUE)
            {
                member.SignInAdmin(ref id, ref password);   //로그인
                if (IsCorrectMemberShip(id, password))
                {
                    ControlMemberMode(17);  //회원 모드로(1.도서검색  2.도서대여  3.도서반납  4.개인정보수정  5.종료)
                    input = value.RIGHT_VALUE;
                }
                else
                {
                    member.PrintScreen();//로그인 실패시 다시 입력(+오류 메세지)
                    Console.WriteLine("\n\n" + value.SIGN_IN_ERROR);
                }
            }
        }
    }
}
