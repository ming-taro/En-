using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AdminController
    {
        Value value = new Value();
        private List<BookVO> bookList;   //책목록
        public AdminController()
        {
            InintBookList();
        }
        public void InintBookList()
        {
            bookList = new List<BookVO>(); 

            string path = "./text/BookList.txt";
            StreamReader reader = new StreamReader(path);
            while (reader.Peek() >= 0)
            {
                String[] text = reader.ReadLine().ToString().Split(",");     //책 정보를 읽어옴
                bookList.Add(new BookVO(text[0], text[1], text[2], text[3], text[4], text[5])); //초기 책 목록을 리스트에 저장
            }
            reader.Close();
        }
        public void ControlAdminSignIn()  //관리자 로그인 관리
        {
            string id = "";
            string password = "";
            SignIn admin = new SignIn();  //2. 관리자모드(->관리자 로그인 화면으로 이동)
            bool input = value.WRONG_VALUE;

            while (input == value.WRONG_VALUE)
            {
                admin.SignInAdmin(ref id, ref password);   //로그인
                if (id == "123456789" && password == "00000")
                {
                    ControlAdminMode();  //관리자 모드로
                    input = value.RIGHT_VALUE;
                }
                else
                {
                    admin.PrintScreen();//로그인 실패시 다시 입력(+오류 메세지)
                    Console.WriteLine("\n\n"+value.SIGN_IN_ERROR);
                }
            }
        }
        public void ControlAdminMode()     //관리자 모드 관리
        {
            Screen screen = new Screen();
            TestingLibrary testingLibrary = new TestingLibrary();
            int menu;

            while (value.ADMIN_MODE)
            {

                screen.PrintAdminMode();       //관리자 모드 출력
                testingLibrary.InitCursorPosition();
                menu = testingLibrary.SelectMenu(13, 17); //메뉴선택 완료
                menu = testingLibrary.GetTop();//메뉴의 해당 커서값
                SelectMenu(menu);              //해당 메뉴 기능 실행
            }
        }
        
        public void SelectMenu(int menu) //관리자 메뉴에서 선택
        {
            int function = value.COMPLETE_FUNCTION;

            switch (menu)
            {
                case 13:  //도서 이름 검색
                    SearchingBook searchingBook = new SearchingBook(bookList);
                    function = searchingBook.ControlSearchingBook(bookList);
                    break;
                case 14:  //도서 등록
                    break;
                case 15:  //도서 수량 관리
                    break;
                case 16:  //도서 삭제
                    break;
                case 17:  //회원관리
                    break;

            }
            
            if(function == value.COMPLETE_FUNCTION)GoningBack();//뒤로가기 -> 관리자메뉴로 돌아감
        }
        public void GoningBack()  //뒤로가기
        {
            while (value.INPUT_VALUE)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape) break;
            }   //escape키 입력 -> 뒤로가기
        }
    }
}
