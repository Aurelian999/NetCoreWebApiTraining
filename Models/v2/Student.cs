using System.ComponentModel.DataAnnotations;

namespace StudentsAPI.Models.v2
{
    public class Student
    {
        [Required]
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
