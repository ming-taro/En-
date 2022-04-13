using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class EditingBookScreen
    {
        public void PrintSearchingBookId()
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintSearchBox("\n☞정보를 수정할 도서번호:");
            BookListVO bookListVO = BookListVO.GetBookListVO();
            ListScreen listScreen = new ListScreen();
            listScreen.PrintBookList(bookListVO.bookList);
        }
        public void PrintBook(BookVO bookVO)
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintMenu("도서정보 수정");
            Console.WriteLine("=======================뒤로가기:[ESC]========================\n");
            BookListVO bookListVO = BookListVO.GetBookListVO();
            Console.WriteLine(bookVO);  //해당 도서정보 출력
            Console.WriteLine("\n=============================================================\n");
        }
        public void PrintQuantityManagement()
        {
            Console.WriteLine("☞도서번호: \n(0~999사이의 숫자만 입력 가능합니다.(ex: 123))\n");
            Console.WriteLine("☞도서명: \n(50자 이내로 입력해주세요.(ex: 이것이 C#이다))\n");
            Console.WriteLine("☞출판사: \n(50자 이내로 입력해주세요.(ex: 한빛미디어)\n");
            Console.WriteLine("☞저자: \n(50자 이내의 영어, 한글만 입력 가능합니다.(ex: 박상현)\n");
            Console.WriteLine("☞가격: \n(숫자만 입력 가능합니다.(ex: 34000)\n");
            Console.WriteLine("☞수량: \n(1~99사이의 숫자만 입력 가능합니다.(ex: 5))");
        }

        public void PrintSuccessMessage(BookVO bookVO)
        {
            PrintBook(bookVO);
            Console.SetCursorPosition(0, 14);
            Console.WriteLine("==================도서정보가 변경되었습니다===================\n");  //변경완료문구 출력
            PrintQuantityManagement();
        }
    }
}
