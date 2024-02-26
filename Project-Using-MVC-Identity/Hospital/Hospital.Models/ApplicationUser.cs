using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Name { get; set; } = null;
        public Gender? MyProperties { get; set; } = null;
        public string? Nationality { get; set; } = null;
        public string? Address {  get; set; } = null;   
        public DateTime? DOB { get; set; }  
        public string? Specialist { get; set; } = null;
        public Department? Department { get; set; }
        
        [NotMapped]
        public ICollection<Appointment>? Appointments { get; set; }
        
        [NotMapped]
        public ICollection<Payroll>? Payrolls { get; set; }  
    }
}

namespace Hospital.Models
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }
}