using System;

namespace RecordManager.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Grade { get; set; }

        public Student(string name, double grade)
        {
            Id = Guid.NewGuid();
            Name = name;
            Grade = grade;
        }
    }
} 