﻿using System;
using Newtonsoft.Json;
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
        public int X => x;
        public int Y => y;
        public int Width => width;
        public int Height => height;
        public ConsoleColor BackgroundColor => backgroundColor;
        [JsonConstructor]
        public Ellipse(int x, int y, int width, int height,ConsoleColor backgroundColor)
        {
            //SetDimensions(x, y, width, height);
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.backgroundColor = backgroundColor;
        }
        /*public void SetDimensions(int x, int y, params int[] dimensions)
        {
            this.x = x;
            this.y = y;
            this.width = dimensions[0];
            this.height = dimensions[1];
        }*/
        public void Draw()
        {
            Console.BackgroundColor = backgroundColor; 

            
            for (int i = -height; i <= height; i++)
            {
                for (int j = -width; j <= width; j++)
                {
                    // Проверяем, попадает ли точка в эллипс
                    if ((j * j) / (double)(width * width) + (i * i) / (double)(height * height) <= 1)
                    {
                        // Устанавливаем позицию на консоли, добавляя смещение x и y
                        Console.SetCursorPosition(x + j, y + i);
                        Console.Write('#'); 
                    }
                }
            }

            Console.ResetColor(); 
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
            return $"Ellipse: X={X}, Y={Y}, Width={Width}, Height={Height}, BackgroundColor={BackgroundColor}";
        }
    }
}
