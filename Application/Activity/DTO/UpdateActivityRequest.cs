namespace Application.Activity.DTO
{
    public class UpdateActivityRequest
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public bool? IsCancelled { get; set; }
        public int? HostId { get; set; }
    }
}
