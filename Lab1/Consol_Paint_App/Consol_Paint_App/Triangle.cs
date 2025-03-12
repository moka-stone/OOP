using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consol_Paint_App
{
    public class Triangle : IFigure
    {
        private int x, y,side1,side2,side3;
        private ConsoleColor backgroundColor = ConsoleColor.Black;
        public Triangle(int x, int y,int side1,int side2,int side3)
        {
            this.x = x;
            this.y = y;
            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;
        }
        public void Draw()
        {
            Console.BackgroundColor = backgroundColor; // Устанавливаем цвет фона

            // Вычисляем координаты вершин
            double s = (side1 + side2 + side3) / 2.0; // Полупериметр
            double area = Math.Sqrt(s * (s - side1) * (s - side2) * (s - side3)); // Площадь по формуле Герона

            // Высота от основания (side1) до вершины (x3, y3)
            int height = (int)((2 * area) / side1);

            int x1 = x; // первая вершина (x, y)
            int y1 = y;
            int x2 = x + side1; // вторая вершина
            int y2 = y;
            
            int x3 = x + side1 / 2; // третья вершина
            int y3 = (int)(y - height); // высота

            // Отрисовка треугольника
            // Условия для рисования треугольника
            for (int i = 0; i <= height; i++)
            {
                // Вычисляем положение для левых и правых точек
                if (i < height)
                {
                    Console.SetCursorPosition(x1 + i, y1 - i); // Левый край
                    Console.Write('█');
                    Console.SetCursorPosition(x2 - i, y2 - i); // Правый край
                    Console.Write('█');
                }
                if (i < height) // Заполняем только если это не последний уровень
                {
                    for (int fillX = x1+i+1; fillX < x2-i; fillX++) // Заполнение между краями
                    {
                        Console.SetCursorPosition(fillX, y1 - i);
                        Console.Write('█');
                    }
                }

                if (i == height)
                {
                    Console.SetCursorPosition(x3, y3); // Верхняя вершина
                    Console.Write('█');
                   
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
            Console.WriteLine($"Erasing Triangle at ({x}, {y})");
        }

        public void AddBackground(ConsoleColor color)
        {
            backgroundColor = color;
        }
        public string GetInfo()
        {
            return $"Triangle at ({x}, {y}), side1: {side1}, side2: {side2}, side3: {side3}, background: {backgroundColor}";
        }
    }
}
