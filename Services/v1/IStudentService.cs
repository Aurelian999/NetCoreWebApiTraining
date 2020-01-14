using StudentsAPI.Models.v1;
using System.Collections.Generic;

namespace StudentsAPI.Services.v1
{
    public interface IStudentsService
    {
        IEnumerable<Student> Get(Filter filter = null);
        void Add(Student student);
        void Update(Student student);
        void Delete(long studentId);
    }
}
