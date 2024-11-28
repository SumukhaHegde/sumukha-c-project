using System.ComponentModel.DataAnnotations;

namespace Application.Activity.DTO
{
    public class CancelActivityRequest
    {
        [Required]
        public int ActivityId { get; set; }

    }
}
