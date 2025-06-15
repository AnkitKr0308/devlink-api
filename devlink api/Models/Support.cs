using System.ComponentModel.DataAnnotations;

namespace devlink_api.Models
{
    public class Support
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Issue { get; set; }

        public string? SupportId { get; set; }
        
    }
}
