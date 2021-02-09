using System;
using System.Collections.Generic;
using System.Text;

namespace SpellingGame2
{
    public class Engine
    {
        public static Random rng = new Random();
        public EventHandler<DayEndEventArgs> dayEnd;

        public class DayEndEventArgs : EventArgs
        {

        }
    }
}
