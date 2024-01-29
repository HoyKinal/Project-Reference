using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Day1.Model
{
    public class Enrolling
    {
        public int Id { get; set; }            // Use PascalCase for property names
        public int StudentId { get; set; }     // Use meaningful names using PascalCase
        public int CourseId { get; set; }      // Use meaningful names using PascalCase
        public DateTime BeginDate { get; set; } // Use PascalCase and meaningful names
        public DateTime EndDate { get; set; }   // Use PascalCase and meaningful names

        public Student? Student { get; set; }
        public Course? Course { get; set; }
    }

}
