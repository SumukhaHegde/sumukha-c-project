using Core.Entities;

namespace Application.Users.DTO
{
    public class UserDetailsLite
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Photo> Photos { get; set; }


    }
}
