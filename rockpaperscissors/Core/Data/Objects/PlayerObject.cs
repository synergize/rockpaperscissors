using System;
using System.Collections.Generic;
using System.Text;

namespace rockpaperscissors.Core.Data.Objects
{
    public class PlayerObject
    {
        private ulong _PlayerID;
        private int _PlayerWins;
        private int _PlayerLosses;
        public ulong PlayerID
        {
            get { return _PlayerID; }
            set { _PlayerID = value; }
        }
        public int PlayerWins
        {
            get { return _PlayerWins; }
            set { _PlayerWins = value; }
        }
        public int PlayerLosses
        {
            get { return _PlayerLosses; }
            set { _PlayerLosses = value; }
        }
        public PlayerObject(ulong id, int win, int loss)
        {
            _PlayerID = id;
            _PlayerWins = win;
            _PlayerLosses = loss;
        }
    }
}
