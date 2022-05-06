using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Exception
    {
        public void PrintMessage(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void PrintNodificationMessage(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void RemoveLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("                                                                           ");
        }

        public void PrintBookIdRegex(int left, int top)
        {
            PrintMessage(left, top, "(0~999사이의 숫자가 아닙니다.다시 입력해주세요.)               ");
        }
        public void PrintBookNameRegex(int left, int top)
        {
            PrintMessage(left, top, "(1~50자 이내의 문자를 입력해주세요.)                         ");
        }
        public void PrintPublisherRegex(int left, int top)
        {
            PrintMessage(left, top, "(1~50자 이내의 문자를 입력해주세요.)                         ");
        }
        public void PrintAuthorRegex(int left, int top)
        {
            PrintMessage(left, top , "(50자 이내의 영어, 한글만 입력해주세요.)                            ");
        }
        public void PrintPriceRegex(int left, int top)
        {
            PrintMessage(left, top, "(0이상의 숫자를 10자 이내로 다시 입력해주세요.)                    )");
        }
        public void PrintQuantityRegex(int left, int top)
        {
            PrintMessage(left, top, "(1~99사이의 숫자만 가능합니다. 다시 입력해주세요.)                            ");
        }
        public void PrintBookNotInList(int left, int top)
        {
            PrintMessage(left, top, "(현재 조회 목록에 없는 도서입니다. 다시 입력해주세요.)           ");
        }
        public void PrintMemberNotInList(int left, int top)
        {
            PrintMessage(left, top, "(존재하지 않는 회원입니다.)                     ");
        }
        public void PrintMemberBorrowingBook(int left, int top)
        {
            PrintMessage(left, top, "(도서를 대여중인 회원은 삭제가 불가능합니다.)   ");
        }
        public void PrintDuplicateBookId(int left, int top)
        {
            PrintMessage(left, top, "(이미 사용중인 도서번호입니다. 다시 입력해주세요.)    ");
        }
        public void PrintNumberOfBook(int left, int top)
        {
            PrintMessage(left, top, "(1~100 사이의 숫자를 입력해주세요.)                    ");
        }
        public void PrintNoLogFile()
        {
            PrintMessage((int)Constants.Menu.LOG_MESSAGE_LEFT, (int)Constants.Menu.SIXTH, "   ('로그 기록' 파일이 존재하지 않습니다)               ");
        }
        public void PrintSaveFile()
        {
            PrintNodificationMessage((int)Constants.Menu.LOG_MESSAGE_LEFT, (int)Constants.Menu.SIXTH, "(바탕화면에 '로그 기록' 파일이 저장되었습니다)");
        }
        public void PrintFileDeletion()
        {
            PrintMessage((int)Constants.Menu.LOG_MESSAGE_LEFT, (int)Constants.Menu.SIXTH, "     ('로그 기록' 파일이 삭제되었습니다)               ");
        }
        public void PrintLogInitialization()
        {
            PrintMessage((int)Constants.Menu.LOG_MESSAGE_LEFT, (int)Constants.Menu.SIXTH, "       (로그 기록이 초기화되었습니다.)               ");
        }
    }
}
