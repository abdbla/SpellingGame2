using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SpellingGame2
{
    public class UI : IUserInterface
    {
        int selectedOption = 0;
        int selectedStat = 0;
        string currentChoice = "";

        int titleWidth = (Console.LargestWindowWidth * 3) / 5;
        int statusWidth = 80;
        int optionsWidth = 40;
        int descriptionHeight = Console.LargestWindowHeight - 14;
        (int, int) currentDescPos = (5, 14);

        const int DESC_SPEED = 1;

        string title = "Default";
        (ConsoleColor, ConsoleColor) titleColor = (ConsoleColor.White, ConsoleColor.Black);
        string description = "lorem ipsum";
        List<(string, ConsoleColor, ConsoleColor)> options = new List<(string, ConsoleColor, ConsoleColor)>() { ("Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ConsoleColor.White, ConsoleColor.Black), ("ipsum", ConsoleColor.White, ConsoleColor.Black), ("dolor", ConsoleColor.White, ConsoleColor.Black), ("sit", ConsoleColor.White, ConsoleColor.Black), ("amet", ConsoleColor.White, ConsoleColor.Black) }; 
        List<(string, ConsoleColor, ConsoleColor)> statistics = new List<(string, ConsoleColor, ConsoleColor)>() { ("Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ConsoleColor.White, ConsoleColor.Black), ("ipsum", ConsoleColor.White, ConsoleColor.Black), ("dolor", ConsoleColor.White, ConsoleColor.Black), ("sit", ConsoleColor.White, ConsoleColor.Black), ("amet", ConsoleColor.White, ConsoleColor.Black) };

        event EventHandler<InterfaceEventArgs> optionSelected;

        event EventHandler<InterfaceEventArgs> IUserInterface.OptionSelected {
            add {
                optionSelected += value;
            }
            remove {
                optionSelected -= value;
            }
        }

        string IUserInterface.GetInput() {
            while (true) {
                switch (Console.ReadKey(true).Key) {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if (selectedOption > 0) {
                            selectedOption--;
                        } else {
                            selectedOption = options.Count - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        selectedOption = (selectedOption + 1) % options.Count;
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        if (selectedStat > 0) {
                            selectedStat--;
                        } else {
                            selectedStat = ((statistics.Count - 1) / 4);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        selectedStat = (selectedStat + 1) % ((statistics.Count + 3) / 4);
                        break;
                    case ConsoleKey.Enter:
                        optionSelected(this, new InterfaceEventArgs(currentChoice, options[selectedOption].Item1));
                        return options[selectedOption].Item1;
                    default: 
                        break;
                }
                DrawOptions();
            }
        }

        void IUserInterface.ChangeTitle(string _title) { //Note to self, needs to move description if enlargened title.
            title = _title;
            DrawTitle();
        }

        void IUserInterface.ChangeTitle(string _title, ConsoleColor foreground, ConsoleColor background) {
            title = _title;
            titleColor = (foreground, background);
            DrawTitle();
        }

        void IUserInterface.ClearDescription() {
            ClearArea(12 + GetSplit(title, titleWidth - 4).Length, 5, Console.BufferWidth - 8, descriptionHeight - (12 + GetSplit(title, titleWidth - 4).Length));
            currentDescPos = (5, 12 + GetSplit(title, titleWidth - 4).Length);
        }

        void IUserInterface.SetOptions(string id, List<string> _options) {
            selectedOption = 0;
            currentChoice = id;
            List<(string, ConsoleColor, ConsoleColor)> tmp = new List<(string, ConsoleColor, ConsoleColor)>();
            foreach (var item in _options) {
                tmp.Add((item, ConsoleColor.White, ConsoleColor.Black));
            }
            options = tmp;
            DrawOptions();
        }

        void IUserInterface.SetOptions(string id, List<(string, ConsoleColor, ConsoleColor)> _options) {
            selectedOption = 0;
            currentChoice = id;
            options = _options;
            DrawOptions();
        }

        void IUserInterface.WriteIntoDescription(string description, int newLine) {
            writeDescription(description, ConsoleColor.White, ConsoleColor.Black, newLine);
        }

        void IUserInterface.WriteIntoDescription(string description, ConsoleColor foreground, ConsoleColor background, int newLine) {
            writeDescription(description, foreground, background, newLine);
        }

        private void writeDescription(string description, ConsoleColor foreground, ConsoleColor background, int newLine) {
            Console.SetCursorPosition(currentDescPos.Item1, currentDescPos.Item2);
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            if (Console.BufferWidth - currentDescPos.Item1 - 5 - description.Length == 0) {
                WriteSlowly(description, DESC_SPEED);
                currentDescPos.Item2++; ;
            } else if (Console.BufferWidth - currentDescPos.Item1 - 5 - description.Length > 0) {
                WriteSlowly(description, DESC_SPEED);
            } else { 
                var item = GetSplit(description, Console.BufferWidth - currentDescPos.Item1 - 5)[0];
                WriteSlowly(item, DESC_SPEED);
                currentDescPos.Item2++;
                if (GetSplit(description, Console.BufferWidth - currentDescPos.Item1 - 5).Length == 0) throw new Exception("Error in UI");
                currentDescPos.Item1 = 5;
                description = description.Substring(item.Length);
            }

            for (int i = 0; i < GetSplit(description, Console.BufferWidth - 10).Length; i++) {
                if (Console.BufferWidth - currentDescPos.Item1 - 5 == 0) { currentDescPos.Item2++; currentDescPos.Item1 = 5; }
                Console.SetCursorPosition(currentDescPos.Item1, currentDescPos.Item2);
                var item = GetSplit(description, Console.BufferWidth - 5 - currentDescPos.Item1)[i];
                WriteSlowly(item, DESC_SPEED);
                if (i != GetSplit(description, Console.BufferWidth - 5 - currentDescPos.Item1).Length - 1) {
                    currentDescPos.Item2++;
                    if (Console.CursorTop >= descriptionHeight - 2) {
                        Console.CursorTop = descriptionHeight - 3;
                        Console.MoveBufferArea(5, 13 + GetSplit(title, titleWidth - 4).Length, Console.BufferWidth - 8, (descriptionHeight - 3) - 13 + GetSplit(title, titleWidth - 4).Length, 5, 12 + GetSplit(title, titleWidth - 4).Length);
                    }
                }
            }
            for (int i = 0; i < newLine; i++) {
                Console.Write("\n");
                if (Console.CursorTop >= descriptionHeight - 2) {
                    Console.CursorTop = descriptionHeight - 3;
                    Console.MoveBufferArea(5, 13 + GetSplit(title, titleWidth - 4).Length, Console.BufferWidth - 8, (descriptionHeight - 3) - 13 + GetSplit(title, titleWidth - 4).Length, 5, 12 + GetSplit(title, titleWidth - 4).Length);
                }
            }
            currentDescPos = (Console.CursorLeft, Console.CursorTop);
            if (newLine > 0) { currentDescPos.Item2 = Console.CursorTop; currentDescPos.Item1 = 5; }
            if (currentDescPos.Item2 >= descriptionHeight - 2) {
                currentDescPos.Item2 = descriptionHeight - 3;
            }
        }

        void IUserInterface.SetStatus(List<string> _statistics) {
            List<(string, ConsoleColor, ConsoleColor)> tmp = new List<(string, ConsoleColor, ConsoleColor)>();
            foreach (var item in _statistics) {
                tmp.Add((item, ConsoleColor.White, ConsoleColor.Black));
            }
            statistics = tmp;
            selectedStat = 0;
            DrawStatistics();
        }

        void IUserInterface.SetStatus(List<(string, ConsoleColor, ConsoleColor)> _statistics) {
            statistics = _statistics;
            selectedStat = 0;
            DrawStatistics();
        }

        public UI() {
            Console.BufferHeight = Console.LargestWindowHeight;
            if (Console.BufferWidth != 240) {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Set the console to fullscreen!");
            }
            while (Console.BufferWidth != 240) { }
            Console.ResetColor();
            Console.Clear();
            DrawLines();
            DrawOptions();
            DrawStatistics();
            DrawTitle();
        }

        private void DrawLines() {
            Console.ResetColor();
            Console.SetCursorPosition(0, descriptionHeight);
            WriteChars('-', Console.BufferWidth);
            Console.SetCursorPosition(((Console.BufferWidth - titleWidth) / 2) - 1, 3);
            WriteChars('-', titleWidth);
            Console.SetCursorPosition(((Console.BufferWidth - titleWidth) / 2) - 1, 6 + GetSplit(title, titleWidth - 4).Length);
            WriteChars('-', titleWidth);
            for (int i = 0; i < GetSplit(title, titleWidth - 4).Length + 2; i++) {
                Console.SetCursorPosition(((Console.BufferWidth - titleWidth) / 2) - 1, 4 + i);
                Console.Write('|');
                Console.SetCursorPosition(Console.BufferWidth - ((Console.BufferWidth - titleWidth) / 2) - 2, 4 + i);
                Console.Write('|');
            }
            for (int i = 0; i < Console.WindowHeight - descriptionHeight - 1; i++) {
                Console.SetCursorPosition(Console.BufferWidth - statusWidth, descriptionHeight + i + 1);
                Console.Write('|');
            }
            Console.SetCursorPosition(0, 10 + GetSplit(title, titleWidth - 4).Length);
            WriteChars('-', Console.BufferWidth);
            Console.CursorVisible = false;
        }

        private void DrawOptions() {
            ClearArea(descriptionHeight + 1, Console.BufferWidth - statusWidth + 1, statusWidth - 2, Console.WindowHeight - descriptionHeight - 1);
            for (int i = 0; i < Math.Min(options.Count, 4); i++) {
                Console.SetCursorPosition(Console.BufferWidth - statusWidth + 4, descriptionHeight + 2 + (3 * i));
                Console.ForegroundColor = options[i + Math.Max(selectedOption - 3, 0)].Item2;
                Console.BackgroundColor = options[i + Math.Max(selectedOption - 3, 0)].Item3;
                if (selectedOption != i + Math.Max(selectedOption - 3, 0)) {
                    Console.Write(GetSplit(options[i + Math.Max(selectedOption - 3, 0)].Item1, statusWidth - 7)[0]);
                } else {
                    Console.Write('[');
                    Console.Write(GetSplit(options[i + Math.Max(selectedOption - 3, 0)].Item1, statusWidth - 9)[0]);
                    Console.Write(']');
                }
                Console.SetCursorPosition(Console.BufferWidth - statusWidth + 4, descriptionHeight + 3 + (3 * i));
                if (GetSplit(options[i + Math.Max(selectedOption - 3, 0)].Item1, statusWidth - 7).Length == 1) {
                    continue;
                } else if (GetSplit(options[i + Math.Max(selectedOption - 3, 0)].Item1, statusWidth - 7).Length == 2) {
                    if (selectedOption != i + Math.Max(selectedOption - 3, 0)) {
                        Console.Write(GetSplit(options[i + Math.Max(selectedOption - 3, 0)].Item1, statusWidth - 7)[1]);
                    } else {
                        Console.Write('[');
                        Console.Write(GetSplit(options[i + Math.Max(selectedOption - 3, 0)].Item1, statusWidth - 9)[1]);
                        Console.Write(']');
                    }
                } else {
                    if (selectedOption != i + Math.Max(selectedOption - 3, 0)) {
                        Console.Write(GetSplit(options[i + Math.Max(selectedOption - 3, 0)].Item1, statusWidth - 7)[1].Substring(0, Math.Min(statusWidth - 10, GetSplit(options[i + Math.Max(selectedOption - 3, 0)].Item1, statusWidth - 7)[1].Length)));
                        Console.Write("...");
                    } else {
                        Console.Write('[');
                        Console.Write(GetSplit(options[i + Math.Max(selectedOption - 3, 0)].Item1, statusWidth - 9)[1].Substring(0, Math.Min(statusWidth - 10, GetSplit(options[i + Math.Max(selectedOption - 3, 0)].Item1, statusWidth - 7)[1].Length)));
                        Console.Write("...");
                        Console.Write(']');
                    }
                }
            }
            Console.ResetColor();
            if (options.Count > 3 && selectedOption < options.Count - 1) {
                Console.SetCursorPosition(Console.BufferWidth - 2, Console.WindowHeight - 1);
                Console.Write('V');
                Console.SetCursorPosition(Console.BufferWidth - 2, Console.WindowHeight - 2);
                Console.Write('|');
                Console.SetCursorPosition(Console.BufferWidth - 2, Console.WindowHeight - 3);
                Console.Write('|');
            }
            if (selectedOption > 3) {
                Console.SetCursorPosition(Console.BufferWidth - 2, descriptionHeight + 2);
                Console.Write('A');
                Console.SetCursorPosition(Console.BufferWidth - 2, descriptionHeight + 3);
                Console.Write('|');
                Console.SetCursorPosition(Console.BufferWidth - 2, descriptionHeight + 4);
                Console.Write('|');
            }
            Console.CursorVisible = false;
        }

        private void ClearArea(int top, int left, int width, int height) {
            for (int i = 0; i < height; i++) {
                Console.SetCursorPosition(left, Math.Min(top + i, Console.BufferHeight - 1));
                WriteChars(' ', width);
            }
            Console.SetCursorPosition(left, top);
        }

        private void DrawTitle() {
            Console.ForegroundColor = titleColor.Item1;
            Console.BackgroundColor = titleColor.Item2;
            for (int i = 0; i < GetSplit(title, titleWidth - 4).Length; i++) {
                Console.SetCursorPosition((Console.BufferWidth - title.Length) / 2, 5 + i);
                Console.Write(GetSplit(title, titleWidth - 4)[i]);
            }
            Console.ResetColor();
        }

        private void DrawStatistics() {
            ClearArea(descriptionHeight + 1, 0, Console.BufferWidth - statusWidth, Console.WindowHeight - descriptionHeight - 1);
            for (int i = 0; i < Math.Min(statistics.Count - (4 * selectedStat), 4); i++) {
                Console.SetCursorPosition(4, descriptionHeight + 4 + (2 * i));
                Console.ForegroundColor = statistics[i + 4 * selectedStat].Item2;
                Console.BackgroundColor = statistics[i + 4 * selectedStat].Item3;
                if (GetSplit(statistics[i + 4 * selectedStat].Item1, Console.BufferWidth - statusWidth - 4).Length == 1) {
                    Console.Write(GetSplit(statistics[i + 4 * selectedStat].Item1, Console.BufferWidth - statusWidth - 6)[0]);
                } else {
                    Console.Write(GetSplit(statistics[i + 4 * selectedStat].Item1, Console.BufferWidth - statusWidth - 9)[0]);
                    Console.Write("...");
                }
            }
            if (statistics.Count - 4 * selectedStat >= 4) {
                Console.SetCursorPosition(Console.BufferWidth - statusWidth - 4, descriptionHeight + 2);
                Console.Write("-->");
            }
            if (selectedStat > 0) {
                Console.SetCursorPosition(1, descriptionHeight + 2);
                Console.Write("<--");
            }
        }

        private string[] GetSplit(string text, int maxWidth) {
            if (text.Length <= maxWidth) return new string[] { text };
            string[] whitespaceSplit = text.Split(new string[] { " ", "\n" }, StringSplitOptions.None);
            bool whitespaceConditional = true;
            foreach (var item in whitespaceSplit) {
                if (item.Length > maxWidth) {
                    whitespaceConditional = false;
                    break;
                }
                item.Trim();
            }
            if (whitespaceConditional) {
                List<StringBuilder> assembledSplit = new List<StringBuilder>();
                int j = 0;
                assembledSplit.Add(new StringBuilder());
                for (int i = 0; i < whitespaceSplit.Length; i++) {
                    if (assembledSplit[j].Length + whitespaceSplit[i].Length + 1 <= maxWidth) {
                        assembledSplit[j].Append(" ");
                        assembledSplit[j].Append(whitespaceSplit[i]);
                    } else {
                        j++;
                        assembledSplit.Add(new StringBuilder());
                        assembledSplit[j].Append(" ");
                        assembledSplit[j].Append(whitespaceSplit[i]);
                    }
                }
                List<string> splitFinal = new List<string>();
                foreach (var item in assembledSplit) {
                    splitFinal.Add(item.ToString().Trim());
                }
                return splitFinal.ToArray();
            }
            //List<string> hyphenSplit = new List<string>();
            //foreach (var item in whitespaceSplit) {
            //    foreach (var i in item.Split('-')) {
            //        hyphenSplit.Add(i);
            //    }
            //}
            //bool hyphenConditional = true;
            //foreach (var item in hyphenSplit) {       Non functioning hyphen-splitting code.
            //    if (item.Length <= maxWidth) {
            //        hyphenConditional = false;
            //        break;
            //    }
            //}
            //if (hyphenConditional) {
            //    return hyphenSplit.ToArray();
            //}
            List<string> nonspaceSplit = new List<string>();
            for (int i = 0; i < (text.Length + (maxWidth / 2)) / maxWidth; i++) {
                nonspaceSplit.Add(text.Substring(i * maxWidth, Math.Min(maxWidth, text.Length - (i * maxWidth))));
            }
            return nonspaceSplit.ToArray();
        }

        private void WriteChars(char letter, int amount) {
            for (int i = 0; i < amount; i++) {
                Console.Write(letter);
            }
        }

        private void WriteSlowly(string text, int delay) {
            for (int i = 0; i < text.Length; i++) {
                Console.Write(text[i]);
                Thread.Sleep(TimeSpan.FromMilliseconds(delay));
            }
        }
    }
}
