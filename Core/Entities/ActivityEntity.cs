using Core.Entities;

public class ActivityEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; }
    public string City { get; set; }
    public string Venue { get; set; }
    public bool IsCancelled { get; set; } = false; // New field for cancellation status
    public int HostId { get; set; } // Name of the user who hosts the activity
    public string HostName { get; set; }
    public ICollection<UserActivityEntity> Attendees { get; set; } = new List<UserActivityEntity>(); // Many-to-many relation
}