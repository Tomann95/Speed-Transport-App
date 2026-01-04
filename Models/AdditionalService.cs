using System.ComponentModel.DataAnnotations;

namespace Speed.Models
{
    public class AdditionalService
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
