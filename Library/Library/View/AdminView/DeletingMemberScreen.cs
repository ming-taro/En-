using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class DeletingMemberScreen
    {
        private LibraryVO library;
        public DeletingMemberScreen()
        {
            library = LibraryVO.GetLibraryVO();
        }

        public void PrintDeleting()
        {
            Console.Clear();
            Console.WriteLine("\n☞삭제할 회원 아이디: ");

            ListScreen listScreen = new ListScreen();
            listScreen.PrintMemberList(library.memberList);   //회원목록 출력    
        }
        public void PrintSuccessMessage(int memberIndex)
        {
            Logo logoScreen = new Logo();
            logoScreen.PrintMenu("회원 삭제 완료");
            Console.WriteLine("=======================뒤로가기:[ESC]========================\n");
            Console.WriteLine("======================[삭제한 회원 정보]=====================\n");
            Console.WriteLine(library.memberList[memberIndex]);
            Console.Write("\n=============================================================");
        }
    }
}
