
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class HospitalInfo
    {
        public int Id { get; set; } = 0;
        public string? Name { get; set; } = null;
        public string? Type { get; set; } = null;   
        public string? City { get; set; } = null;   
        public string? PinCode { get; set; } = null;    
        public string? Country { get; set; } = null;

        //Design Room Table
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        //new List<Room>(): his means that by default, the Rooms property is an empty list of Room objects.
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }

}
