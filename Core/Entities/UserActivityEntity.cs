namespace Core.Entities
{
    public class UserActivityEntity
    {
        public int UserId { get; set; } // Unique identifier for the user (assuming it's an int)
        public int ActivityId { get; set; } // Unique identifier for the activity this user is attending
        public string FirstName { get; set; } // Attendee's username
        public string LastName { get; set; } // Attendee's username

        public string Bio { get; set; } // Attendee's bio or description
        public ICollection<Photo> Photos { get; set; }
    }
}
