using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Screen
    {
        public void PrintSearchingMember(List<MemberVO> memberList)//이사완
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
        public void PrintSearchingMember(string id, List<MemberVO> memberList)
        {
            Console.Clear();
            Console.WriteLine("\n=============================================================\n");
            for(int i=0; i<memberList.Count; i++)
            {
                if (memberList[i].Id.Equals(id))
                {
                    Console.WriteLine(memberList[i]);
                    Console.WriteLine("\n=============================================================\n");
                }
            }
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<");
        }
    }
}
