using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class EditingProfile
    {
        public void SelectMenu()
        {

        }
        public int ControlEditingProfile(string memberId)
        {
            Keyboard keyboard = new Keyboard(0, 16);
            EditingScreen editingScreen = new EditingScreen();
            editingScreen.PrintEditing(memberId);                //회원정보수정 화면
            int menu;

            while (Constants.INPUT_VALUE)
            {
                menu = keyboard.SelectMenu(16, 34, 3);
                if (menu == Constants.ESCAPE) return Constants.ESCAPE;      //메뉴선택 중 뒤로가기를 누르면 종료
            }

            return Constants.COMPLETE_FUNCTION;
        }
    }
}
