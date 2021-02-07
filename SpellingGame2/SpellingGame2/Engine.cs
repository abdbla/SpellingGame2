using System;
using System.Collections.Generic;
using System.Text;

namespace SpellingGame2
{
    class Engine
    {
        public EventHandler<DayEndEventArgs> dayEnd;

        public class DayEndEventArgs : EventArgs
        {

        }
    }
}
