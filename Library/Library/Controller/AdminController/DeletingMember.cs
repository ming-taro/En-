using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class DeletingMember
    {
        private int memberIndex;
        public void PrintInputBox(int left, int top, string message)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }
        public bool IsMemberInList(string memberId)  //입력받은 회원아이디가 회원목록에 있는지 확인
        {
            MemberListVO memberListVO = MemberListVO.GetMemberListVO();  //회원목록

            for(int i=0; i<memberListVO.memberList.Count; i++)
            {
                if(memberListVO.memberList[i].Id.Equals(memberId))
                {
                    memberIndex = i;     //삭제할 회원아이디 인덱스 저장
                    return Constants.MEMBER_IN_LIST;
                }
            }
            return !Constants.MEMBER_IN_LIST;
        }
        public bool IsBorrowingBook(string memberId)  //회원이 도서를 대여중인지 확인
        {
            BorrowListVO borrowListVO = BorrowListVO.GetBorrowListVO();  //대여 도서 목록

            for(int i=0; i<borrowListVO.borrowList.Count; i++)
            {
                if (borrowListVO.borrowList[i].MemberId.Equals(memberId)) return Constants.BOOK_I_BORROWED;   //도서를 대여중 -> 회원삭제 불가
            }
            return !Constants.BOOK_I_BORROWED;
        }
        public string InputMemberId()
        {
            string memberId;

            while (Constants.INPUT_VALUE)
            {
                Console.SetCursorPosition(22, 1);
                memberId = Console.ReadLine();     //삭제할 회원 아이디를 입력받음
                if (string.IsNullOrEmpty(memberId) || !IsMemberInList(memberId))   //존재하지 않는 회원을 삭제하려고 할 때
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
        public void DeleteMember()
        {
            MemberListVO memberListVO = MemberListVO.GetMemberListVO();  //회원목록
            memberListVO.memberList.RemoveAt(memberIndex);  //회원삭제
        }
        public int ControlDeletingMember()
        {
            DeletingScreen deletingScreen = new DeletingScreen();
            deletingScreen.PrintDeleting();     //삭제입력화면 출력

            Keyboard keyboard = new Keyboard();
            int menu;

            keyboard.SetPosition(0, 1);
            menu = keyboard.SelectMenu(1, 1, 1);

            if (menu == Constants.ESCAPE) return Constants.ESCAPE;  //뒤로가기
            string memberId = InputMemberId();   //삭제할 회원 아이디를 입력받음

            deletingScreen.PrintSuccessMessage(memberIndex);   //삭제 성공 메세지 출력
            DeleteMember();//회원삭제

            return Constants.COMPLETE_FUNCTION;
        }
    }
}
