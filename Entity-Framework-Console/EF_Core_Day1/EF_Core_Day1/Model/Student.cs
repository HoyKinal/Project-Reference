using System.ComponentModel.DataAnnotations;

namespace EF_Core_Day1.Model
{
    // Define Entity Student
    public class Student
    {
        [Key]
        public int Id { get; set; } // Use PascalCase for property names
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public byte Age { get; set; }
        public ICollection<Enrolling>? Enrollings { get; set; }
    }
}
