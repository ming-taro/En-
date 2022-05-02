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
        public void PrintBookIBorrowed(int left, int top)
        {
            PrintMessage(left, top, "(이미 대여중인 도서입니다. 다른 도서를 선택해주세요.)                  ");
        }
        public void PrintQuantityZero(int left, int top)
        {
            PrintMessage(left, top, "(대여가능한 도서가 0권입니다. 다른 도서를 선택해주세요.)              ");
        }
        public void PrintBookINeverBorrowed(int left, int top)
        {
            PrintMessage(left,top, "(대여목록에 없는 도서번호입니다. 다시 입력해주세요.)             ");
        }
        public void PrintBookOnLoan(int left, int top)
        {
            PrintMessage(left, top, "(회원이 대여중인 도서는 삭제가 불가능합니다.)                 ");
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
    }
}
