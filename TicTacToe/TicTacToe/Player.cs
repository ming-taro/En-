using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Player
    {
        private char drawType;
        private string name;
        public List<int> mySpaceNumber = new List<int>();

        public char DrawType
        {
            get { return drawType; }
            set { drawType = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public void SetPlayer(char drawType, string name)
        {
            this.drawType = drawType;
            this.name = name;
        }
        public void AddSpaceNumber(int spaceNumber)
        {
            mySpaceNumber.Add(spaceNumber);
        }
        public void InitMySpaceNumber()
        {
            mySpaceNumber.Clear();
        }
    }
}
