using System;
using System.Collections.Generic;
using System.Text;

namespace rockpaperscissors.Core.Data.Objects
{
    public class GameObject
    {
        private ulong _PlayerOneID;
        private ulong _PlayerTwoID;
        private string _Input;
        public ulong PlayerOneID
        {
            get { return _PlayerOneID; }
            set { _PlayerOneID = value; }
        }
        public ulong PlayerTwoID
        {
            get { return _PlayerTwoID; }
            set { _PlayerTwoID = value; }
        }
        public string Input
        {
            get { return _Input; }
            set { _Input = value; }
        }
        public GameObject(ulong p1, ulong p2, string input)
        {
            _PlayerOneID = p1;
            _PlayerTwoID = p2;
            _Input = input;
        }
    }
}
