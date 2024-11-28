using System.ComponentModel.DataAnnotations;

namespace Application.Users.DTO
{
    public class CreateUserRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,10}$", ErrorMessage = "Password must contain 1 Uppercase, 1 Lowercase, 1 Special character, 1 Number")]
        public string Password { get; set; }

    }
}
