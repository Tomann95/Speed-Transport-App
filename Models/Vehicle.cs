using System.ComponentModel.DataAnnotations;

namespace Speed.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string GraphicUrl { get; set; } = string.Empty; // For vehicle selection graphic (1.4)

        public double CargoCapacityKg { get; set; }
        
        public double LengthMeters { get; set; }
        public double WidthMeters { get; set; }
        public double HeightMeters { get; set; }

        public double PricePerKm { get; set; }
    }
}
