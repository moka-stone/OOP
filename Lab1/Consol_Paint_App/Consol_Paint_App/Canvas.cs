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
            //if (currentFigureIndex >= 0) 
                //figures[currentFigureIndex].AddBackground(ConsoleColor.);
        }
        public string GetCurrentFigureInfo()
        {
            if (currentFigureIndex >= 0)
                return figures[currentFigureIndex].GetInfo();
            return "No figure selected.";
        }


    }
}
