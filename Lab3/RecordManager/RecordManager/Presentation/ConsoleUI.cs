using System;
using System.Threading.Tasks;
using RecordManager.Application.DTOs;
using RecordManager.Application.Services;

namespace RecordManager.Presentation
{
    public class ConsoleUI
    {
        private readonly StudentService _studentService;

        public ConsoleUI(StudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Student Record Management System ===");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Edit Student");
                Console.WriteLine("3. View All Students");
                Console.WriteLine("4. Exit");
                Console.Write("\nEnter your choice (1-4): ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        await AddStudent();
                        break;
                    case 2:
                        await EditStudent();
                        break;
                    case 3:
                        ViewAllStudents();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task AddStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Add New Student ===");
            
            Console.Write("Enter student name: ");
            string name = Console.ReadLine();

            Console.Write("Enter student grade (0-100): ");
            if (!double.TryParse(Console.ReadLine(), out double grade))
            {
                Console.WriteLine("Invalid grade format. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            var studentDto = new StudentDTO { Name = name, Grade = grade };
            var (success, message, quote) = await _studentService.AddStudentAsync(studentDto);

            Console.WriteLine($"\n{message}");
            if (success && quote != null)
            {
                Console.WriteLine("\nHere's a motivational quote for you:");
                Console.WriteLine($"\"{quote.Content}\"");
                Console.WriteLine($"- {quote.Author}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private async Task EditStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Edit Student ===");
            
            var students = _studentService.GetAllStudents();
            if (!students.GetEnumerator().MoveNext())
            {
                Console.WriteLine("No students found. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nCurrent Students:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Grade: {student.Grade}");
            }

            Console.Write("\nEnter student ID to edit: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                Console.WriteLine("Invalid ID format. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter new name: ");
            string name = Console.ReadLine();

            Console.Write("Enter new grade (0-100): ");
            if (!double.TryParse(Console.ReadLine(), out double grade))
            {
                Console.WriteLine("Invalid grade format. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            var studentDto = new StudentDTO { Id = id, Name = name, Grade = grade };
            var (success, message) = _studentService.UpdateStudent(studentDto);

            Console.WriteLine($"\n{message}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ViewAllStudents()
        {
            Console.Clear();
            Console.WriteLine("=== All Students ===\n");
            
            var students = _studentService.GetAllStudents();
            if (!students.GetEnumerator().MoveNext())
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.Id}");
                    Console.WriteLine($"Name: {student.Name}");
                    Console.WriteLine($"Grade: {student.Grade}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
} 