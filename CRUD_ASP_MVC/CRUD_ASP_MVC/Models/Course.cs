// ASP_MVC_CRUD.Models.Course.cs

namespace ASP_MVC_CRUD.Models
{
    public class Course
    {
        public int? CourseId { get; set; }
        public string? CourseName { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
