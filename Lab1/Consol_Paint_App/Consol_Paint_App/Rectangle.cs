using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consol_Paint_App
{
    public class Rectangle : IFigure
    {
        private int x, y, width, height;
        private ConsoleColor backgroundColor;
        public int X => x;
        public int Y => y;
        public int Width => width;
        public int Height => height;
        public ConsoleColor BackgroundColor => backgroundColor;

        public Rectangle(int x, int y, int width, int height)
        {
            SetDimensions(x, y, width, height);
            this.backgroundColor = ConsoleColor.Black;
        }
        public void SetDimensions(int x, int y, params int[] dimensions)
        {
            this.x = x;
            this.y = y;
            this.width = dimensions[0];
            this.height = dimensions[1];
        }
        public Rectangle() { }

        public void Draw()
        {
            Console.BackgroundColor = backgroundColor; // Устанавливаем цвет фона
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(x + 1, y + i + 1); // Устанавливаем позицию курсора
                Console.Write(new string('#', width)); // Рисуем строку из символов
            }
            Console.ResetColor(); // Сбрасываем цвет консоли
        }

        public void Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Erase()
        {
            Console.WriteLine($"Erasing Rectangle at ({x}, {y})");
        }

        public void AddBackground(ConsoleColor color)
        {
            backgroundColor = color;

        }
        public string GetInfo()
        {
            return $"Rectangle: X={X}, Y={Y}, Width={Width}, Height={Height}, BackgroundColor={BackgroundColor}";
        }

    }
}

