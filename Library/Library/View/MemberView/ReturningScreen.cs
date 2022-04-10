using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class ReturningScreen
    {
        public void PrintReturning(string memberId)
        {
            Console.Clear();
            Console.WriteLine("\n☞반납할 도서 번호: ");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>뒤로가기:[ESC]<<<<<<<<<<<<<<<<<<<<<<<<\n");

            BorrowListVO borrowListVO = BorrowListVO.GetBorrowListVO();

            for(int i=0; i<borrowListVO.borrowList.Count; i++)
            {
                if (borrowListVO.borrowList[i].MemberId.Equals(memberId))
                {
                    Console.WriteLine(borrowListVO.borrowList[i].BookVO);  //회원의 대여 도서 목록 출력
                    Console.WriteLine("\n=============================================================\n");
                }
            }

        }
    }
}
