using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace devlink_api.Models
{
    public class Support
    {
        [Required]
        [JsonPropertyName("caseId")]
        public ulong SupportId { get; set; }
       

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Issue { get; set; }
        
    }
}
