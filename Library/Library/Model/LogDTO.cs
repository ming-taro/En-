using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LogDTO
    {
        private int id;
        private string user;
        private string menu;
        private string content;
        private string date;
        public LogDTO(int id, string user, string menu, string content, string date)
        {
            this.id = id;
            this.user = user;
            this.menu = menu;
            this.content = content;
            this.date = date;
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string User
        {
            get { return user; }
            set { user = value; }
        }
        public string Menu
        {
            get { return menu; }
            set { menu = value; }
        }
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public override String ToString()
        {
            return "로그 번호: " + id + "\n   사용자: " + user + "\n실행 기능: " + menu +
                "\n실행 내역: " + content + "\n날짜/시간: " + date;
        }
    }
}
