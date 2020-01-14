using StudentsAPI.Models.v1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsAPI.Services.v1
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
            }
        }

        private void Initialize()
        {
            students.Add(new Student() { Id = 1, FirstName = "Test1", LastName = "Student1" });
            students.Add(new Student() { Id = 2, FirstName = "Test2", LastName = "Student2" });
        }
    }
}
