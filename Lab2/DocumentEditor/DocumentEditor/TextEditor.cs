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
        private string? content; // Content
        
        private int cursorPosition; // Navigation
        private int cursorPositionTop;
        private int cursorPositionLeft;

        string clipboard = string.Empty; // Selection
        int selectionStart = -1; 
        int selectionEnd = -1;   

        private Stack<string> undoStack = new Stack<string>(); // Undo/redo
        private Stack<string> redoStack = new Stack<string>();
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
        public string Run() 
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
                            UpdateUR();
                            if (cursorPositionLeft > 0)  // Default
                            {
                                content = content.Remove(cursorPosition - 1, 1);
                                cursorPosition--;
                                cursorPositionLeft--;
                            }
                            else if (cursorPositionLeft == 0 && cursorPositionTop > 0) // End of Line
                            {
                                string[] linesb = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                                cursorPositionTop--;
                                cursorPositionLeft = linesb[cursorPositionTop].Length;
                                content = content.Remove(cursorPosition - Environment.NewLine.Length, Environment.NewLine.Length);
                                cursorPosition -= Environment.NewLine.Length;                             
                            }
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (key.Modifiers.HasFlag(ConsoleModifiers.Shift)) // Selection
                        {
                            selectionEnd = cursorPosition;
                            if (selectionStart == -1) selectionStart = cursorPosition;
                        }
                        if (cursorPositionLeft > 0)                        // Default
                        {
                            cursorPosition--;
                            cursorPositionLeft--;
                        }
                        else if (cursorPositionLeft == 0 && cursorPositionTop != 0)     // End of line
                        {
                            cursorPositionTop--;
                            string[] linesl = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                            cursorPosition -=Environment.NewLine.Length;
                            cursorPositionLeft = linesl[cursorPositionTop].Length; 

                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (key.Modifiers.HasFlag(ConsoleModifiers.Shift))  // Selection
                        {
                            selectionEnd = cursorPosition;
                            if (selectionStart == -1) selectionStart = cursorPosition; 
                        }
                        string[] linesr = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                        if (cursorPositionLeft < linesr[cursorPositionTop].Length)  // Default
                        {
                            cursorPosition++;
                            cursorPositionLeft++;
                        }
                        else if (cursorPositionLeft == linesr[cursorPositionTop].Length && cursorPositionTop < linesr.Length - 1) // End of line
                        {
                            cursorPosition+= Environment.NewLine.Length;
                            cursorPositionLeft = 0;
                            cursorPositionTop++;
                        }
                        break;

                    case ConsoleKey.Enter:                                                  //Enter
                        UpdateUR();
                        content = content.Insert(cursorPosition, Environment.NewLine);                                            
                        cursorPosition += Environment.NewLine.Length;
                        cursorPositionTop++;
                        cursorPositionLeft = 0;
                        break;
                        

                    // With ctrl control
                    case ConsoleKey.C when (key.Modifiers & ConsoleModifiers.Control) != 0: //Ctrl+C
                        if (selectionStart != -1 && selectionEnd != -1 && selectionStart != selectionEnd)
                        {
                            clipboard = content.Substring(Math.Min(selectionStart, selectionEnd), Math.Abs(selectionEnd - selectionStart)); ;
                        }
                        break;
                    case ConsoleKey.V when (key.Modifiers & ConsoleModifiers.Control) != 0: //Ctlr+V
                        if (!string.IsNullOrEmpty(clipboard))
                        {
                            UpdateUR();
                            content = content.Insert(cursorPosition, clipboard);
                            cursorPosition += clipboard.Length; 
                            cursorPositionLeft += clipboard.Length; 
                        }
                        break;
                    case ConsoleKey.X when (key.Modifiers & ConsoleModifiers.Control) != 0:
                        if (selectionStart != -1 && selectionEnd != -1 && selectionStart != selectionEnd) 
                        {
                            clipboard = content.Substring(Math.Min(selectionStart, selectionEnd), Math.Abs(selectionEnd - selectionStart));
                            content = content.Remove(Math.Min(selectionStart, selectionEnd), Math.Abs(selectionEnd - selectionStart));
                            
                        }
                        break;

                    //Formatting
                    case ConsoleKey.B when (key.Modifiers & ConsoleModifiers.Control) != 0: // Ctrl+B
                        content = ApplyFormatting("**", cursorPosition);
                        cursorPosition += 2; // Сдвигаем курсор после вставки форматирования
                        cursorPositionLeft += 2;
                        break;

                    case ConsoleKey.I when (key.Modifiers & ConsoleModifiers.Control) != 0: // Ctrl+I
                        content = ApplyFormatting("_", cursorPosition);
                        cursorPosition += 1; // Сдвигаем курсор после вставки форматирования
                        cursorPositionLeft += 1;
                        break;

                    case ConsoleKey.U when (key.Modifiers & ConsoleModifiers.Control) != 0: // Ctrl+U
                        content = ApplyFormatting("~", cursorPosition);
                        cursorPosition += 1; // Сдвигаем курсор после вставки форматирования
                        cursorPositionLeft += 1;
                        break;

                    //Undo/Redo
                    case ConsoleKey.Z when (key.Modifiers & ConsoleModifiers.Control) != 0:
                        if (undoStack.Count > 0)
                        {
                            redoStack.Push(content);
                            content = undoStack.Pop();
                        }
                        break;
                    case ConsoleKey.N when (key.Modifiers & ConsoleModifiers.Control) != 0:
                        if (redoStack.Count > 0)
                        {
                            undoStack.Push(content);
                            content = redoStack.Pop();
                        }
                        break;

                    case ConsoleKey.Escape:
                        return content; // Exit
                    default:
                        if (key.KeyChar != 0)
                        {
                            UpdateUR();
                            content = content.Insert(cursorPosition, key.KeyChar.ToString());
                            cursorPosition++;
                            cursorPositionLeft++;
                        }
                        break;
                }
            }
        }
        private string ApplyFormatting(string formatting, int cursorPosition)
        {          
                return content.Insert(cursorPosition, formatting + formatting);     
        }
        private void UpdateUR() 
        {
            undoStack.Push(content);
        }
    }
}
