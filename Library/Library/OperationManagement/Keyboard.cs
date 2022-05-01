using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Keyboard
    {
        private int left;
        private int top;
        public Keyboard()
        {
            left = (int)Constants.Menu.LEFT;
            top = (int)Constants.Menu.FIRST;
        }
        public int Top
        {
            get { return top; }
            set { top = value; }
        }
        public void InitCursorPosition()
        {
            left = (int)Constants.Menu.LEFT;
            top = (int)Constants.Menu.FIRST;
        }
        public void SetPosition(int left, int top)
        {
            this.left = left;
            this.top = top;
        }
        public int SelectMenu(int minTop, int maxTop, int move)//메뉴 고르기 -> 고른 메뉴의 커서값을 left,top에 반영
        {
            int key;
            
            while (Constants.KEYBOARD_OPERATION)            //키보드 방향키로 움직이는 동안
            {
                Console.SetCursorPosition(left, top);
                Console.WindowTop = 0;
                key = ControlKeyboard(move);                //키보드를 입력받음
                if (IsOutOfMenu(minTop, maxTop)) continue;  //메뉴를 벗어나는 이동은 X

                if (key == (int)Constants.Keyboard.ENTER || key == (int)Constants.Keyboard.ESCAPE) break;//메뉴입력 -> 해당 메뉴로 이동(1.회원모드   2.관리자모드)
            }

            return key;
        }
        private int ControlKeyboard(int move)       //현재 키보드 입력값 반환
        {
            ConsoleKeyInfo keyInfo;

            while (Constants.KEYBOARD_OPERATION)   //다른 키를 입력하면 올바른 키를 입력할때까지 무한루프
            {
                keyInfo = Console.ReadKey(true);   //키를 입력받음 

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        top += move;
                        Console.SetCursorPosition(left, top);   //↓방향키 입력
                        return (int)Constants.Keyboard.MOVING_CURSOR;
                    case ConsoleKey.UpArrow:                      //↑방향키 입력
                        top -= move;
                        Console.SetCursorPosition(left, top);
                        return (int)Constants.Keyboard.MOVING_CURSOR;
                    case ConsoleKey.Enter:                        //enter 입력
                        return (int)Constants.Keyboard.ENTER;
                    case ConsoleKey.Escape:                       //escape 입력
                        return (int)Constants.Keyboard.ESCAPE;
                }
            }
        }
        private bool IsOutOfMenu(int minTop, int maxTop)   //메뉴 선택 범위를 벗어나는지 확인
        {
            if (top < minTop) top = minTop;
            else if (top > maxTop) top = maxTop;
            else return Constants.MOVEMENT_WITHIN_MENU;

            return Constants.OUT_OF_MENU;
        }
        public void PressESC()
        {
            ConsoleKeyInfo keyInfo;

            while (Constants.KEYBOARD_OPERATION)   //다른 키를 입력하면 올바른 키를 입력할때까지 무한루프
            {
                keyInfo = Console.ReadKey(true);   //키를 입력받음 
                if (keyInfo.Key == ConsoleKey.Escape) return;
            }
        }
        public int PressEnterOrESC()
        {
            ConsoleKeyInfo keyInfo;

            while (Constants.KEYBOARD_OPERATION)   //다른 키를 입력하면 올바른 키를 입력할때까지 무한루프
            {
                keyInfo = Console.ReadKey(true);   //키를 입력받음 
                if (keyInfo.Key == ConsoleKey.Escape) return (int)Constants.Keyboard.ESCAPE;
                else if (keyInfo.Key == ConsoleKey.Enter) return (int)Constants.Keyboard.ENTER;
            }
        }
    }
}
