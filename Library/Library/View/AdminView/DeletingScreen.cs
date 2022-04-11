using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class DeletingScreen
    {
        public void PrintDeleting()
        {
            Console.Clear();
            Console.WriteLine("\n☞삭제할 회원 아이디: ");

            ListScreen listScreen = new ListScreen();
            MemberListVO memberListVO = MemberListVO.GetMemberListVO();
            listScreen.PrintMemberList(memberListVO.memberList);   //회원목록 출력    
        }
        public void PrintSuccessMessage(int memberIndex)
        {
            LogoScreen logoScreen = new LogoScreen();
            logoScreen.PrintMenu("회원 삭제 완료");
            Console.WriteLine("=======================뒤로가기:[ESC]========================\n");
            Console.WriteLine("======================[삭제한 회원 정보]=====================\n");
            MemberListVO memberListVO = MemberListVO.GetMemberListVO();
            Console.WriteLine(memberListVO.memberList[memberIndex]);
            Console.Write("\n=============================================================");
        }
    }
}
