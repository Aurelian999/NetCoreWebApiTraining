using Microsoft.AspNetCore.JsonPatch;
using StudentsAPI.Models.v2;
using System.Collections.Generic;

namespace StudentsAPI.Services.v2
{
    public interface IStudentsService
    {
        IEnumerable<Student> Get(Filter filter = null);
        void Add(Student student);
        void Update(Student student);
        void Delete(long studentId);
        void Patch(JsonPatchDocument<Student> patchDocument, Student studentToPatch);
    }
}
