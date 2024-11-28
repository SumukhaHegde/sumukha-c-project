namespace Application.Activity.DTO
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public bool IsCancelled { get; set; }
        public int HostId { get; set; } // Optionally, include host details if necessary
        public string HostName { get; set; }
        public List<AttendeeDto> Attendees { get; set; }
    }
}
