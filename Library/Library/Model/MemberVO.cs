using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MemberVO
    {
        private string id;
        private string password;
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
            set { id = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Age
        {
            get { return age; }
            set { age = value; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string PrintMember()
        {
            return "('" + id + "', '" + password + "', '" + name + "', '" + age + "', '" + phoneNumber + "', '" + address + "')";
        }
        public override string ToString()
        {
            return "아이디: " + id + "\n비밀번호: " + password + "\n이름: " + name + 
                "\n나이: " + age + "\n휴대전화: " + phoneNumber + "\n도로명 주소: " + address;
        }
    }
}
