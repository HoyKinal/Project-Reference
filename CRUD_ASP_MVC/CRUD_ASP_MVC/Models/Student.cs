namespace ASP_MVC_CRUD.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
