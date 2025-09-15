using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using RecordManager.Domain.Entities;
using RecordManager.Domain.Interfaces;

namespace RecordManager.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _filePath = "students.json";
        private List<Student> _students;

        public StudentRepository()
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
            }
            else
            {
                _students = new List<Student>();
            }
        }

        public void Add(Student student)
        {
            _students.Add(student);
        }

        public void Update(Student student)
        {
            var existingStudent = _students.FirstOrDefault(s => s.Id == student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Grade = student.Grade;
            }
        }

        public Student GetById(Guid id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _students.ToList();
        }

        public void SaveChanges()
        {
            var json = JsonSerializer.Serialize(_students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
} 