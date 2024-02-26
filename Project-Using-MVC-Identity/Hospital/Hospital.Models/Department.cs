using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Department
    {
        public int Id { get; set; } 
        public Gender Gender { get; set; }
        public string? Nationality { get; set; }   
        public string? Address { get; set; } = null;
        public DateTime? DOB { get; set; }
        public string? Specialist {  get; set; } 
         
    }
}
