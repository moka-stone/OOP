using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecordManager.Application.DTOs;
using RecordManager.Domain.Entities;
using RecordManager.Domain.Interfaces;
using RecordManager.Infrastructure.External;

namespace RecordManager.Application.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;
        private readonly QuoteApiAdapter _quoteApiAdapter;

        public StudentService(IStudentRepository repository, QuoteApiAdapter quoteApiAdapter)
        {
            _repository = repository;
            _quoteApiAdapter = quoteApiAdapter;
        }

        public async Task<(bool success, string message, QuoteDTO quote)> AddStudentAsync(StudentDTO studentDto)
        {
            if (string.IsNullOrWhiteSpace(studentDto.Name))
                return (false, "Student name cannot be empty.", null);

            if (studentDto.Grade < 0 || studentDto.Grade > 100)
                return (false, "Grade must be between 0 and 100.", null);

            var student = new Student(studentDto.Name, studentDto.Grade);
            _repository.Add(student);
            _repository.SaveChanges();

            var quote = await _quoteApiAdapter.GetRandomQuoteAsync();
            return (true, "Student added successfully.", quote);
        }

        public (bool success, string message) UpdateStudent(StudentDTO studentDto)
        {
            if (string.IsNullOrWhiteSpace(studentDto.Name))
                return (false, "Student name cannot be empty.");

            if (studentDto.Grade < 0 || studentDto.Grade > 100)
                return (false, "Grade must be between 0 and 100.");

            var student = _repository.GetById(studentDto.Id);
            if (student == null)
                return (false, "Student not found.");

            student.Name = studentDto.Name;
            student.Grade = studentDto.Grade;
            _repository.Update(student);
            _repository.SaveChanges();

            return (true, "Student updated successfully.");
        }

        public IEnumerable<StudentDTO> GetAllStudents()
        {
            var students = _repository.GetAll();
            var studentDtos = new List<StudentDTO>();

            foreach (var student in students)
            {
                studentDtos.Add(new StudentDTO
                {
                    Id = student.Id,
                    Name = student.Name,
                    Grade = student.Grade
                });
            }

            return studentDtos;
        }
    }
} 