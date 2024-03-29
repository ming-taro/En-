﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MemberDeletion
    {
        private BookDAO bookDAO = BookDAO.GetInstance();
        private MemberDAO memberDAO = MemberDAO.GetInstance();
        private LogDAO logDAO = LogDAO.GetInstance();
        private EnteringText text;
        private AdminView adminView;
        private Logo logo;

        public MemberDeletion(EnteringText text, AdminView adminView, Logo logo)
        {
            this.text = text;
            this.adminView = adminView;
            this.logo = logo;
        }
        private bool IsMemberInList(string memberId, List<MemberVO> memberList)  //입력받은 회원아이디가 회원목록에 있는지 확인
        {
            for(int i=0; i<memberList.Count; i++)
            {
                if(memberList[i].Id.Equals(memberId)) return Constants.IS_MEMBER_IN_LIST;
            }

            return Constants.IS_MEMBER_NOT_IN_LIST;
        }
        private string InputMemberId(List<MemberVO> memberList)
        {
            int top = (int)Constants.SearchMenu.FIRST;
            int left = (int)Constants.InputField.MEMBER_DELETION;
            int exceptionLeft = (int)Constants.SearchMenu.LEFT;
            int exceptionTop = (int)Constants.Exception.TOP;
            string memberId;

            while (Constants.INPUT_VALUE)
            {
                memberId = text.EnterText(left, top, "");     //삭제할 회원 아이디를 입력받음

                if (memberId.Equals(Constants.ESC) == Constants.IS_NOT_MATCH && IsMemberInList(memberId, memberList) == Constants.IS_MEMBER_NOT_IN_LIST)   //존재하지 않는 회원을 삭제하려고 할 때
                {
                    logo.PrintMessage(exceptionLeft, exceptionTop, "(존재하지 않는 회원입니다.)                     ", ConsoleColor.Red);
                }
                else if (memberId.Equals(Constants.ESC) == Constants.IS_NOT_MATCH && bookDAO.IsMemberWhoBorrowedBook(memberId))  //해당 회원은 도서를 대여중 -> 삭제불가
                {
                    logo.PrintMessage(exceptionLeft, exceptionTop, "(도서를 대여중인 회원은 삭제가 불가능합니다.)", ConsoleColor.Red);
                }
                else break;

                logo.RemoveLine(left, top);
            }
            return memberId;
        }
        public void SaveChanges(string memberId)
        {
            adminView.PrintDeletedMember(memberDAO.GetMemberAccount(memberId));  //삭제 성공 메세지 출력
            memberDAO.DeleteFromMemberList(memberId);                            //회원리스트에서 회원 정보 삭제
            logDAO.DeleteFromMemberList(memberId);                               //로그에 회원 삭제 기록 저장
        }
        public void DeleteMember(Keyboard keyboard)
        {
            string memberId;
            List<MemberVO> memberList;

            while (Constants.INPUT_VALUE)
            {
                memberList = memberDAO.MakeMemberList();    //회원 리스트
                adminView.PrintMemberIdInputScreen(memberList);       //회원 아이디 검색창 + 회원 리스트 출력

                memberId = InputMemberId(memberList);                 //삭제할 회원 아이디를 입력받음
                if (memberId.Equals(Constants.ESC)) break;

                SaveChanges(memberId);  //변경사항 저장
                
                if(keyboard.PressEnterOrESC() == (int)Constants.Keyboard.ESCAPE) break;
                Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
            }
            Console.CursorVisible = Constants.IS_VISIBLE_CURSOR;
        }
    }
}
