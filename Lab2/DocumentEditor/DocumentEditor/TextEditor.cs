using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentEditor
{
    public class TextEditor
    {
        private string content;
        public void Run() 
        {
            int cursorPosition = 0;
            while (true) 
            {
                Console.Clear();
                Console.WriteLine(content);
                Console.SetCursorPosition(cursorPosition, Console.CursorTop);
                var key = Console.ReadKey();
                switch (key.Key) 
                {
                    case ConsoleKey.Backspace:
                        if (cursorPosition > 0) 
                        {
                            content.Remove(cursorPosition - 1, 1);
                            cursorPosition--;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (cursorPosition > 0)
                            cursorPosition--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (cursorPosition < content.Length)
                            cursorPosition++;
                        break;

                }
            }
        }
    }
}
