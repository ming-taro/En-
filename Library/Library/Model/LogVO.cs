using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LogVO
    {
        private string id;
        private string user;
        private string menu;
        private string content;
        private string date;
        public string Id
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
    }
}
