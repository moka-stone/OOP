using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consol_Paint_App
{
    public class Ellipse : IFigure
    {
        private int x, y, width, height;
        private ConsoleColor backgroundColor ;
        public Ellipse(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.backgroundColor = ConsoleColor.Black;
        }
        public Ellipse() { }
        public void Draw()
        {
            Console.BackgroundColor = backgroundColor; // Устанавливаем цвет фона

            // Отрисовка эллипса
            for (int i = -height; i <= height; i++)
            {
                for (int j = -width; j <= width; j++)
                {
                    // Проверяем, попадает ли точка в эллипс
                    if ((j * j) / (double)(width * width) + (i * i) / (double)(height * height) <= 1)
                    {
                        // Устанавливаем позицию на консоли, добавляя смещение x и y
                        Console.SetCursorPosition(x + j, y + i);
                        Console.Write('#'); // Рисуем точку эллипса
                    }
                }
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
            Console.WriteLine($"Erasing Ellipse at ({x}, {y})");
        }

        public void AddBackground(ConsoleColor color)
        {
            backgroundColor = color;
        }
        public string GetInfo()
        {
            return $"Ellipse at ({x}, {y}), width: {width}, height: {height}, background: {backgroundColor}";
        }
    }
}
