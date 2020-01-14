using Microsoft.AspNetCore.JsonPatch;
using StudentsAPI.Models.v2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsAPI.Services.v2
{
    public class StudentsService : IStudentsService
    {
        private readonly List<Student> students;

        public StudentsService()
        {
            students = new List<Student>();
            Initialize();
        }

        public void Add(Student student)
        {
            lock (students)
            {
                students.Add(student);
            }
        }

        public void Delete(long studentId)
        {
            lock (students)
            {
                students.RemoveAll(s => s.Id == studentId);
            }
        }

        public IEnumerable<Student> Get(Filter filter = null)
        {
            if (filter == null || filter.Values == null) return students;

            List<Student> result = new List<Student>();

            if (filter.Field != null && filter.Values.Length == 1)
            {
                switch (filter.Type)
                {
                    case FilterType.Equals:
                        result = students.Where(student => student.GetType().GetProperty(filter.Field).GetValue(student).Equals(filter.Values.First())).ToList();
                        break;
                    case FilterType.Contains:
                        result = students.Where(student => student.GetType().GetProperty(filter.Field).GetValue(student).ToString().Contains(filter.Values.First())).ToList();
                        break;
                    case FilterType.StartsWith:
                        result = students.Where(student => student.GetType().GetProperty(filter.Field).GetValue(student).ToString().StartsWith(filter.Values.First())).ToList();
                        break;
                    case FilterType.EndsWith:
                        result = students.Where(student => student.GetType().GetProperty(filter.Field).GetValue(student).ToString().EndsWith(filter.Values.First())).ToList();
                        break;
                }
                return result;
            }

            foreach (var value in filter.Values)
            {
                switch (filter.Type)
                {
                    case FilterType.Equals:
                        result = result.Union(
                            students.Where(s => (s.FirstName + " " + s.LastName).Equals(value, StringComparison.CurrentCultureIgnoreCase))
                        ).ToList();
                        break;
                    case FilterType.Contains:
                        result = result.Union(
                            students.Where(s => (s.FirstName + " " + s.LastName).Contains(value, StringComparison.CurrentCultureIgnoreCase))
                        ).ToList();
                        break;
                    case FilterType.StartsWith:
                        result = result.Union(
                            students.Where(s => (s.FirstName + " " + s.LastName).StartsWith(value, StringComparison.CurrentCultureIgnoreCase))
                        ).ToList();
                        break;
                    case FilterType.EndsWith:
                        result.Union(
                            students.Where(s => (s.FirstName + " " + s.LastName).EndsWith(value, StringComparison.CurrentCultureIgnoreCase))
                        ).ToList();
                        break;
                }
            }

            return result;
        }

        public void Update(Student student)
        {
            lock (students)
            {
                Student studentToUpdate = students.Single(s => s.Id == student.Id);
                studentToUpdate.FirstName = student.FirstName;
                studentToUpdate.LastName = student.LastName;
                studentToUpdate.Email = student.Email;
                studentToUpdate.Phone = student.Phone;
            }
        }

        public void Patch(JsonPatchDocument<Student> patchDocument, Student studentToPatch)
        {
            lock (students)
            {
                patchDocument.ApplyTo(studentToPatch);
            }
        }

        private void Initialize()
        {
            students.Add(new Student() { Id = 1, FirstName = "Test1", LastName = "Student1", Email = "test.student@mail.com", Phone = "0751 123 456"});
            students.Add(new Student() { Id = 2, FirstName = "Test2", LastName = "Student2", Email = "test.student@mail.com", Phone = "0751 123 456"});
        }
    }
}
