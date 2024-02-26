using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Day1.Model
{
    public class Course
    {
        public int Id { get; set; } = 0!;
        public string? Name { get; set; } = null;    
        public string? Code { get; set; } =string.Empty;
        public ICollection<Enrolling>? Enrollings { get; set; }

        public Course() { }

        public Course(int Id, string Name, string Code)
        {
            this.Id = Id;   
            this.Name = Name;   
            this.Code = Code;   
        }
    }
}
