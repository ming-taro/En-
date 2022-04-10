using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MemberListVO
    {
        private static MemberListVO memberListVO = null;
        public List<MemberVO> memberList;
        public static MemberListVO getMemberListVO()
        {
            if (memberListVO == null)
            {
                memberListVO = new MemberListVO();
            }
            return memberListVO;
        }
        public MemberListVO()
        {
            memberList = new List<MemberVO>();

            memberList.Add(new MemberVO("aaabbb", "000111", "김남준", "29", "010-1111-1111", "서울특별시 광진구 강변북로"));
            memberList.Add(new MemberVO("aaaccc", "000222", "김우빈", "31", "010-2222-2222", "서울특별시 광진구 강변북로"));
            memberList.Add(new MemberVO("aaaddd", "000333", "정해인", "30", "010-3333-3333", "서울특별시 광진구 강변북로"));
            memberList.Add(new MemberVO("aaaeee", "000444", "정호석", "29", "010-4444-4444", "서울특별시 광진구 강변북로"));
            memberList.Add(new MemberVO("aaafff", "000555", "박해진", "28", "010-5555-5555", "서울특별시 광진구 뚝섬로"));
            memberList.Add(new MemberVO("aaaggg", "000666", "김석진", "28", "010-6666-6666", "서울특별시 광진구 광나루로"));
            memberList.Add(new MemberVO("aaahhh", "000777", "박지민", "26", "010-7777-7777", "서울특별시 광진구 광나루로"));

        }
    }
}
