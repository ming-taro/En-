using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable
{
    class Keyboard
    {
        private int left;
        private int top;
        public Keyboard(int left, int top)
        {
            this.left = left;
            this.top = top;
        }
        public int Left
        {
            get { return left; }
            set { left = value; }
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
        public int SelectTop(int minTop, int maxTop, int move)//메뉴 고르기 -> 고른 메뉴의 커서값을 left,top에 반영
        {
            int key;

            while (Constants.KEYBOARD_OPERATION)    //키보드 방향키로 움직이는 동안
            {
                Console.SetCursorPosition(left, top);
                key = ControlKeyboard(move);            //키보드를 입력받음
                if (OutOfTop(minTop, maxTop) == (int)Constants.Keyboard.OUT_OF_MENU) continue;  //메뉴를 벗어나는 이동은 X

                //메뉴입력 -> 해당 메뉴로 이동
                if (key == (int)Constants.Keyboard.ENTERING_MENU || key == (int)Constants.Keyboard.ESCAPE) break;
            }

            return key;
        }
        public int SelectLeft(int minLeft, int maxLeft, int move)
        {
            int key;

            while (Constants.KEYBOARD_OPERATION)
            {
                Console.SetCursorPosition(left, top);
                key = ControlKeyboard(move);            //키보드를 입력받음
                if (OutOfLeft(minLeft, maxLeft) == (int)Constants.Keyboard.OUT_OF_MENU) continue;  //메뉴를 벗어나는 이동은 X

                //메뉴입력 -> 해당 메뉴로 이동
                if (key == (int)Constants.Keyboard.ENTERING_MENU || key == (int)Constants.Keyboard.ESCAPE) break;
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
                        return (int)Constants.Keyboard.MOVING_CURSOR;
                    case ConsoleKey.UpArrow:                      //↑방향키 입력
                        top -= move;
                        Console.SetCursorPosition(left, top);
                        return (int)Constants.Keyboard.MOVING_CURSOR;
                    case ConsoleKey.Enter:                        //enter 입력
                        return (int)Constants.Keyboard.ENTERING_MENU;
                    case ConsoleKey.Escape:                       //escape 입력
                        return (int)Constants.Keyboard.ESCAPE;
                }
            }
        }
        public int OutOfTop(int minTop, int maxTop)
        {
            if (top < minTop) top = minTop;
            else if (top > maxTop) top = maxTop;
            else return (int)Constants.Keyboard.WITHIN_THE_MENU;  //입력범위 내에서 키보드를 움직임

            return (int)Constants.Keyboard.OUT_OF_MENU;   //입력 범위를 벗어남
        }
        public int OutOfLeft(int minLeft, int maxLeft)
        {
            if (Left < minLeft) top = minLeft;
            else if (Left > maxLeft) top = maxLeft;
            else return (int)Constants.Keyboard.WITHIN_THE_MENU;  //입력범위 내에서 키보드를 움직임

            return (int)Constants.Keyboard.OUT_OF_MENU;   //입력 범위를 벗어남
        }
    }
}
