using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consol_Paint_App
{
    public class Canvas
    {
        private List<IFigure> figures = new List<IFigure>();
        private int currentFigureIndex = -1;

        public void AddFigure(IFigure figure)
        {
            figures.Add(figure);
            currentFigureIndex = figures.Count - 1;

        }
        public void DrawFigureArea()
        {
            for (int j = 0; j <= 42; j++)
            {

                if (j == 0 || j == 42)
                {

                    for (int i = 0; i <= 202; i++)
                    {
                        Console.Write('#');
                    }
                }
                else
                {

                    for (int i = 0; i <= 202; i++)
                    {
                        if (i == 0 || i == 202)
                        {
                            Console.Write('#');
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                }
                Console.Write("\n");
            }
        }
        public void DrawAll()
        {
            Console.SetCursorPosition(0, 0);
            DrawFigureArea();
            foreach (var figure in figures)
            {
                figure.Draw();
            }
        }
        public void SelectNextFigure()
        {
            if (currentFigureIndex < figures.Count - 1)
                currentFigureIndex++;
        }
        public void SelectPreviousFigure()
        {
            if (currentFigureIndex > 0)
                currentFigureIndex--;
        }
        public void MoveCurrentFigure(int x, int y)
        {
            if (currentFigureIndex >= 0)
                figures[currentFigureIndex].Move(x, y);
        }
        public void EraseCurrentFigure()
        {
            if (currentFigureIndex >= 0)
            {
                figures[currentFigureIndex].Erase();
                figures.RemoveAt(currentFigureIndex);
                currentFigureIndex = -1;
            }
        }
        public void ChangeBackgroundCurrentFigure(string color)
        {
            if (currentFigureIndex >= 0) {
               
                if (color == "Red")
                {
                    figures[currentFigureIndex].AddBackground(ConsoleColor.Red);
                    
                }
                else if (color == "Blue")
                {
                    figures[currentFigureIndex].AddBackground(ConsoleColor.Blue);

                }
                else if (color == "Green")
                {
                    figures[currentFigureIndex].AddBackground(ConsoleColor.Green);

                }
                else
                {
                    Console.WriteLine("Неверный цвет.");
                }                        
            }
        }
        public string GetCurrentFigureInfo()
        {
            if (currentFigureIndex >= 0)
                return figures[currentFigureIndex].GetInfo();
            return "No figure selected.";
        }
        public void SaveToFile(string fileName)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            var json = JsonConvert.SerializeObject(figures, settings);
            File.WriteAllText(fileName, json);
            Console.WriteLine("Figures saved to " + fileName);
        }
        public void LoadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore
                };
                var json = File.ReadAllText(fileName);
                var loadedFigures = JsonConvert.DeserializeObject<List<IFigure>>(json, settings);
                figures.Clear();
                figures.AddRange(loadedFigures);
                /*foreach(var figure in loadedFigures)
                {
                    if (figure is Rectangle rectangle)
                    {
                        Console.WriteLine($"Rect{rectangle.X} ******* {rectangle.Y} ********* {rectangle.Width} ******* {rectangle.Height}");
                        Thread.Sleep(2000);
                        figures.Add(new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
                    }
                    if (figure is Triangle triangle)
                    {
                        Console.WriteLine($"Triangl{triangle.X} ******* {triangle.Y} ********* {triangle.Side1} ******* {triangle.Side2}");
                        Thread.Sleep(2000);
                        figures.Add(new Triangle(triangle.X, triangle.Y, triangle.Side1, triangle.Side2, triangle.Side1));

                    }
                    if (figure is Ellipse ellipse)
                    {
                        Console.WriteLine($"Ellips{ellipse.X} ******* {ellipse.Y} ********* {ellipse.Width} ******* {ellipse.Height}");
                        Thread.Sleep(2000);
                        figures.Add(new Ellipse(ellipse.X, ellipse.Y, ellipse.Width, ellipse.Height));

                    }
                }
                */
                    currentFigureIndex = figures.Count - 1; 
                    Console.WriteLine("Figures loaded from " + fileName);
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
    }
    
}
