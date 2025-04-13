using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentEditor
{
    public class TextEditor
    {
        private string? content;
        private int cursorPosition;
        private int cursorPositionTop;
        private int cursorPositionLeft;
        public TextEditor(string initialContent)
        {            
            this.content = initialContent;
            this.cursorPosition = 0;
            this.cursorPositionTop = 0;
            this.cursorPositionLeft = 0;

            if (initialContent == null)
            {
                this.content = "empty";
            }

        }
        public void Run() 
        {
   
            while (true) 
            {
                Console.Clear();
                Console.WriteLine(content);
                Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop);
                var key = Console.ReadKey(intercept:true);
                switch (key.Key) 
                {
                    // Controlers
                    case ConsoleKey.Backspace:
                        if (cursorPosition > 0) 
                        {
                            content.Remove(cursorPosition - 1, 1);
                            cursorPosition--;
                            cursorPositionLeft--;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (cursorPositionLeft > 0)
                        {
                            cursorPosition--;
                            cursorPositionLeft--;
                        }
                        else if (cursorPositionLeft == 0 && cursorPositionTop != 0)
                        {
                            cursorPositionTop--;
                            string[] linesl = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                            // Устанавливаем курсор в конец предыдущей строки
                            cursorPosition -=Environment.NewLine.Length;
                            cursorPositionLeft = linesl[cursorPositionTop].Length; // Длина предыдущей строки

                        }
                        break;
                    case ConsoleKey.RightArrow:
                        string[] linesr = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                        if (cursorPositionLeft < linesr[cursorPositionTop].Length)
                        {
                            cursorPosition++;
                            cursorPositionLeft++;
                        }
                        else if (cursorPositionLeft == linesr[cursorPositionTop].Length && cursorPositionTop < linesr.Length - 1) 
                        {
                            cursorPosition+= Environment.NewLine.Length;
                            cursorPositionLeft = 0;
                            cursorPositionTop++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        content = content.Insert(cursorPosition, Environment.NewLine);                                            
                        cursorPosition += Environment.NewLine.Length;
                        cursorPositionTop++;
                        cursorPositionLeft = 0;
                        break;
                    // with ctrl control
                    case ConsoleKey.C when (key.Modifiers & ConsoleModifiers.Control) != 0:
                        break;
                    case ConsoleKey.V when (key.Modifiers & ConsoleModifiers.Control) != 0:
                        break;
                    case ConsoleKey.X when (key.Modifiers & ConsoleModifiers.Control) != 0:
                        break;
                    //Formatting
                    case ConsoleKey.B when (key.Modifiers & ConsoleModifiers.Control) != 0:
                       // content = ApplyFormatting("**", cursorPosition);
                        cursorPosition += 2; // Сдвигаем курсор после вставки форматирования
                        break;

                    case ConsoleKey.I when (key.Modifiers & ConsoleModifiers.Control) != 0:
                        //content = ApplyFormatting("_", cursorPosition);
                        cursorPosition += 1; // Сдвигаем курсор после вставки форматирования
                        break;

                    case ConsoleKey.U when (key.Modifiers & ConsoleModifiers.Control) != 0:
                        //content = ApplyFormatting("~", cursorPosition);
                        cursorPosition += 1; // Сдвигаем курсор после вставки форматирования
                        break;
                    case ConsoleKey.Escape:
                        return; // Выход из редактора
                    default:
                        if (key.KeyChar != 0)
                        {
                            content = content.Insert(cursorPosition, key.KeyChar.ToString());
                            cursorPosition++;
                            cursorPositionLeft++;
                        }
                        break;
                }
            }
        }
        public string TransportContent() 
        {
            return this.content;
        }
    }
}
