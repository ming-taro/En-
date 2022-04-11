using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class ListScreen
    {
        public void PrintBookList(List<BookVO> bookList)
        {
            Console.SetCursorPosition(0, 5);
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<");

            for (int i = 0; i < bookList.Count; i++)
            {
                Console.WriteLine(bookList[i]);
                Console.WriteLine("\n=============================================================\n");
            }
        }
        public void PrintMemberList(List<MemberVO> memberList)
        {
            Console.Clear();
            Console.WriteLine("\n☞삭제할 회원 아이디: ");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<");

            for (int i = 0; i < memberList.Count; i++)
            {
                Console.WriteLine(memberList[i]);
                Console.WriteLine("\n=============================================================\n");
            }
        }
    }

}
