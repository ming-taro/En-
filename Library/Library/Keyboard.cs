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
        private int left = 25, top = 13;
        public Keyboard()
        {

        }
        public Keyboard(int left, int top)
        {
            this.left = left;
            this.top = top;
        }
        public int Top
        {
            get { return top; }
            set { top = value; }
        }
        public void InitCursorPosition()
        {
            left = 25;
            top = 13;
        }
        public void SetPosition(int left, int top)
        {
            this.left = left;
            this.top = top;
        }
        public int SelectMenu(int minTop, int maxTop, int move)//메뉴 고르기 -> 고른 메뉴의 커서값을 left,top에 반영
        {
            int key;
            
            while (Constants.KEYBOARD_OPERATION)    //키보드 방향키로 움직이는 동안
            {
                Console.SetCursorPosition(left, top);
                key = ControlKeyboard(move);            //키보드를 입력받음
                if (IsOutOfMenu(minTop, maxTop)) continue;  //메뉴를 벗어나는 이동은 X

                if (key == Constants.ENTERING_MENU || key == Constants.ESCAPE) break;//메뉴입력 -> 해당 메뉴로 이동(1.회원모드   2.관리자모드)
            }

            return key;
        }
        public int ControlKeyboard(int move) //현재 키보드 입력값 반환
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
                        return Constants.MOVING_CURSOR;
                    case ConsoleKey.UpArrow:                      //↑방향키 입력
                        top -= move;
                        Console.SetCursorPosition(left, top);
                        return Constants.MOVING_CURSOR;
                    case ConsoleKey.Enter:                        //enter 입력
                        return Constants.ENTERING_MENU;
                    case ConsoleKey.Escape:                       //escape 입력
                        return Constants.ESCAPE;
                }
            }
        }
        public bool IsOutOfMenu(int minTop, int maxTop)
        {
            if (top < minTop) top = minTop;
            else if (top > maxTop) top = maxTop;
            else return !Constants.OUT_OF_MENU;

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
    }
}
