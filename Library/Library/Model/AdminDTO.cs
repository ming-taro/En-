﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class AdminDTO
    {
        private string id;
        private string password;

        public AdminDTO(string id, string password)
        {
            this.id = id;
            this.password = password;
        }
        public string Id
        {
            get { return id; }
        }
        public string Password
        {
            get { return password; }
        }
    }
}
