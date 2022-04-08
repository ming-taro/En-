using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Value
    {
        public int CLOSE_PROGRAM = -3;
        public int INVALID_INPUT = -2;
        public int ESCAPE = -1;
        public int MOVING_CURSOR = 0;
        public int ENTERING_MENU = 1;

        public int SEARCHING_BOOK = 1;
        public int REGISTRATION_BOOK = 2;
        public int MANAGING_BOOK = 3;
        public int REMOVING_BOOK = 4;
        public int MANAGING_MEMBER = 5;

        public int COMPLETE_FUNCTION = 7;

        public bool OUT_OF_MENU = true;
        public bool ADMIN_MODE = true;

        public bool GOING_NEXT = true;
        public bool GOING_BACK = true;
        public bool INPUT_VALUE = true;
        public bool RIGHT_VALUE = true;
        public bool WRONG_VALUE = false;
        public string SIGN_IN_ERROR = "아이디 또는 비밀번호를 잘못 입력하셨습니다.\n다시 입력해주세요.\n";
    }
}
