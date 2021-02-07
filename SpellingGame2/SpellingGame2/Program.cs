using System;
using System.Collections.Generic;

namespace SpellingGame2
{
    class Program
    {
        static void Main(string[] args) { //start of game setup
            IUserInterface userInterface = new UI();
            Engine engine = new Engine();
            Player player = new Player();
            Data data = new Data();
            engine.dayEnd += player.RestoreStats;
        }

        void TowerMenu() {
            while (true) {

            }
        }
    }
}
