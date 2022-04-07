using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AdminVO
    {
        protected string id;
        protected string password;
        public AdminVO()
        {
            id = "123456789";
            password = "0000";
        }

        public override string ToString()
        {
            return "아이디: " + id + "\n비밀번호: " + password;
        }

    }
}
