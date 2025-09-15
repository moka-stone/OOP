using System;
using System.Collections.Generic;
using RecordManager.Domain.Entities;

namespace RecordManager.Domain.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        void Update(Student student);
        Student GetById(Guid id);
        IEnumerable<Student> GetAll();
        void SaveChanges();
    }
} 