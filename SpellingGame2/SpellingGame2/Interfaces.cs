using System;
using System.Collections.Generic;
using System.Text;

namespace SpellingGame2
{
    interface IUserInterface
    {
        event EventHandler<InterfaceEventArgs> OptionSelected;

        public void ChangeTitle(string title);
        public void ChangeTitle(string title, ConsoleColor foreground, ConsoleColor background);
        public void WriteIntoDescription(string description, bool newLine);
        public void WriteIntoDescription(string description, ConsoleColor foreground, ConsoleColor background, bool newLine);
        public void ClearDescription();
        public void SetOptions(string id, List<string> options);
        public void SetOptions(string id, List<(string, ConsoleColor, ConsoleColor)> options);
        public void SetStatus(List<string> vars);
        public void SetStatus(List<(string, ConsoleColor, ConsoleColor)> vars);
        public string GetInput();
    }

    class InterfaceEventArgs : EventArgs
    {
        string choiceId;
        string option;

        public InterfaceEventArgs(string _choiceId, string _option) {
            choiceId = _choiceId;
            option = _option;
        }
    }
}
