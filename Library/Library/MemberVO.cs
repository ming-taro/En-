using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MemberVO : AdminVO
    {
        private string name;
        private string age;
        private string phoneNumber;
        private string address;
        public MemberVO(string id, string password, string name, string age, string phoneNumber, string address)
        {
            this.id = id;
            this.password = password;
            this.name = name;
            this.age = age;
            this.phoneNumber = phoneNumber;
            this.address = address;
        }
        public string Id
        {
            get { return id; }
        }
        public string Password
        {
            get { return password; }
        }
        public override string ToString()
        {
            return "아이디: " + id + "\n비밀번호: " + password + "\n이름: " + name + 
                "\n나이: " + age + "\n휴대전화: " + phoneNumber + "\n도로명 주소: " + address;
        }
    }
}
