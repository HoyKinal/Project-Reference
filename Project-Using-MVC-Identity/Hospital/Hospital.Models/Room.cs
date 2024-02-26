namespace Hospital.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string? RoomNumber { get; set; } = null;
        public string? Type { get; set; } = null;
        public string? Status { get; set; } = null;
        public int HostpitalId { get; set; }
        public HospitalInfo? Hospital { get; set; } = null;

    }
}