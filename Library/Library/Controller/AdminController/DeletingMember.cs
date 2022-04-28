using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class DeletingMember
    {
        private BookDAO bookDatabaseManager;
        private EnteringText text;
        private AdminView adminView;

        private LibraryVO library;
        private int memberIndex;
        public DeletingMember(BookDAO bookDatabaseManager)
        {
            this.bookDatabaseManager = bookDatabaseManager;
            text = new EnteringText();
            adminView = new AdminView();
        }
        public void PrintInputBox(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }
        public bool IsMemberInList(string memberId)  //입력받은 회원아이디가 회원목록에 있는지 확인
        {
            for(int i=0; i<library.memberList.Count; i++)
            {
                if(library.memberList[i].Id.Equals(memberId))
                {
                    memberIndex = i;     //삭제할 회원아이디 인덱스 저장
                    return Constants.MEMBER_IN_LIST;
                }
            }
            return !Constants.MEMBER_IN_LIST;
        }
        public bool IsBorrowingBook(string memberId)  //회원이 도서를 대여중인지 확인
        {
            for(int i=0; i<library.borrowList.Count; i++)
            {
                if (library.borrowList[i].MemberId.Equals(memberId)) return Constants.BOOK_I_BORROWED;   //도서를 대여중 -> 회원삭제 불가
            }
            return !Constants.BOOK_I_BORROWED;
        }
        public string InputMemberId()
        {
            string memberId;

            while (Constants.INPUT_VALUE)
            {
                memberId = text.EnterText(15, 1, "");     //삭제할 회원 아이디를 입력받음

                if (text.Equals(Constants.ESC))
                {
                    return Constants.ESC;
                }
                else if (!IsMemberInList(memberId))   //존재하지 않는 회원을 삭제하려고 할 때
                {
                    PrintInputBox(0, 2, "(존재하지 않는 회원입니다.)                     ");
                }
                else if (IsBorrowingBook(memberId))  //해당 회원은 도서를 대여중 -> 삭제불가
                {
                    PrintInputBox(0, 2, "(도서를 대여중인 회원은 삭제가 불가능합니다.)   ");
                }
                else
                {
                    break;
                }
                PrintInputBox(22, 1, Constants.REMOVE_LINE);
            }
            return memberId;
        }
        public void ControlDeletingMember()
        {
            DeletingMemberScreen deletingScreen = new DeletingMemberScreen();

            List<MemberVO> memberList = bookDatabaseManager.MakeMemberList();    //회원 리스트
            adminView.PrintMemberIdInputScreen(memberList);                      //회원 아이디 검색창 + 회원 리스트 출력

            string memberId = InputMemberId();            //검색할 회원 아이디를 입력받음
            if (memberId.Equals(Constants.ESC)) return;

            deletingScreen.PrintSuccessMessage(memberIndex);   //삭제 성공 메세지 출력
            library.memberList.RemoveAt(memberIndex);  //회원삭제

        }
    }
}
