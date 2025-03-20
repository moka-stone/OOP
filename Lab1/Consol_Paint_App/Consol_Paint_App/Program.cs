
using System.Diagnostics;

namespace Consol_Paint_App 
{
    class Program 
    {
        static void Main(string[] args)
        {
            
           
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetWindowSize(213, 49);
            Console.SetBufferSize(250, 100);
            const int figureAreaHeight = 42; // Высота области для фигур
            const int consoleWidth = 202; // Ширина консоли



            Canvas canvas = new Canvas();
            
            bool running = true;

            while (running)
            {
                
                for (int l = 1; l < 40; l++)
                {
                    Console.SetCursorPosition(0, figureAreaHeight + l);
                    Console.Write(new string(' ', consoleWidth)); // Очищаем пространство под холстом
                }
                Console.SetCursorPosition(0, figureAreaHeight + 1); // Возвращаем курсор обратно для нового вывода
                Console.WriteLine("MENU");
                Console.WriteLine("0 - exit (without saving)");
                Console.WriteLine("1 - choose figure for drawing");
                Console.WriteLine("2 - object selection menu");
                Console.WriteLine("3 - save file");
                Console.WriteLine("4 - load file");
                Console.WriteLine("5 - undo action");
                Console.WriteLine("6 - redo action");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        running = false;
                        break;

                    case "1":
                        Console.WriteLine("Choose figure: 1 - rectangle, 2 - triangle, 3 - ellipse");
                        string figureChoice = Console.ReadLine();
                        if (figureChoice == "1")
                        {
                            // Ввод координат и размеров для прямоугольника
                            Console.WriteLine("Enter x, y, width, height:");
                            int x = int.Parse(Console.ReadLine());
                            int y = int.Parse(Console.ReadLine());
                            int width = int.Parse(Console.ReadLine());
                            int height = int.Parse(Console.ReadLine());
                            ConsoleColor backgroundColor = ConsoleColor.Black;
                            if (Validator.ValidateRectParams(x, y, width, height))
                            {
                                canvas.AddFigure(new Rectangle(x, y, width, height,backgroundColor));
                            }
                            else { Console.WriteLine("Bad params"); }
                        }
                        if (figureChoice == "2")
                        {
                            // Ввод координат и размеров для треугольника
                            Console.WriteLine("Enter x, y, side1, side2, side3:");
                            int x = int.Parse(Console.ReadLine());
                            int y = int.Parse(Console.ReadLine());
                            int side1 = int.Parse(Console.ReadLine());
                            int side2 = int.Parse(Console.ReadLine());
                            int side3 = int.Parse(Console.ReadLine());
                            ConsoleColor backgroundColor = ConsoleColor.Black;
                            if (Validator.ValidateTriangleParams(x, y, side1, side2, side3))
                            {
                                canvas.AddFigure(new Triangle(x, y, side1, side2, side3,backgroundColor));
                            }
                            else { Console.WriteLine("Bad params"); }
                        }
                        if (figureChoice == "3")
                        {
                            // Ввод координат и размеров для прямоугольника
                            Console.WriteLine("Enter x, y, width, height:");
                            int x = int.Parse(Console.ReadLine());
                            int y = int.Parse(Console.ReadLine());
                            int width = int.Parse(Console.ReadLine());
                            int height = int.Parse(Console.ReadLine());
                            ConsoleColor backgroundColor = ConsoleColor.Black;
                            if (Validator.ValidateEllipseParams(x, y, width, height))
                            {
                                canvas.AddFigure(new Ellipse(x, y, width, height, backgroundColor));
                            }
                            else { Console.WriteLine("Bad params"); }
                        }
                        break;

                    case "2":
                        // Меню выбора объектов
                        
                        bool selecting = true;
                        while (selecting)
                        {
                            
                            Console.WriteLine("Object selection menu:");
                            Console.WriteLine("i - info");
                            Console.WriteLine("p - previous object");
                            Console.WriteLine("n - next object");
                            Console.WriteLine("m - move");
                            Console.WriteLine("e - erase");
                            Console.WriteLine("b - change background");
                            Console.WriteLine("0 - exit");

                            string objectChoice = Console.ReadLine();
                            switch (objectChoice)
                            {
                                case "i":
                                    Console.WriteLine(canvas.GetCurrentFigureInfo());
                                    break;
                                case "p":
                                    canvas.SelectPreviousFigure();
                                    break;
                                case "n":
                                    canvas.SelectNextFigure();
                                    break;
                                case "m":
                                    Console.WriteLine("Enter new coordinates (x, y):");
                                    int newX = int.Parse(Console.ReadLine());
                                    int newY = int.Parse(Console.ReadLine());
                                    canvas.MoveCurrentFigure(newX, newY);
                                    break;
                                case "e":
                                    canvas.EraseCurrentFigure();
                                    break;
                                case "b":
                                    Console.WriteLine("Enter background color:");                                 
                                    string color = Console.ReadLine();
                                    canvas.ChangeBackgroundCurrentFigure(color);
                                    break;
                                case "0":
                                    selecting = false;
                                    break;
                            }
                        }
                        break;

                    case "3":
                        // Сохранение файла
                        Console.WriteLine("Enter filename to save:");
                        string saveFileName = Console.ReadLine();
                        canvas.SaveToFile(saveFileName);
                        
                        break;

                    case "4":
                        // загрузка файла
                        Console.WriteLine("Enter filename to load:");
                        string loadFileName = Console.ReadLine();
                        canvas.LoadFromFile(loadFileName);

                        break;

                    case "5":
                        // Логика отмены действия
                        break;

                    case "6":
                        // Логика повтора действия
                        break;
                }

                canvas.DrawAll(); // Отрисовать все фигуры
                Console.SetCursorPosition(0, 42);
            }
            UnitTesting.RunAssertions();


        }
    }
}