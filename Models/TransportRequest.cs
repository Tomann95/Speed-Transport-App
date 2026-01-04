using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Speed.Models
{
    public class TransportRequest
    {
        public int Id { get; set; }

        // 1.2 Loading/Unloading Dates
        [Required]
        public DateTime LoadingDate { get; set; }

        [Required]
        public DateTime UnloadingDate { get; set; }

        // 1.3 Loading/Unloading Addresses (In a real app, these might be separate entities for multiple points)
        [Required]
        public string LoadingAddress { get; set; } = string.Empty;
        [Required]
        public string LoadingCountry { get; set; } = "PL"; // Default

        [Required]
        public string UnloadingAddress { get; set; } = string.Empty;
        [Required]
        public string UnloadingCountry { get; set; } = "PL"; // Default

        // 1.6 Product Data
        [Required]
        public string ShipmentType { get; set; } = string.Empty; // e.g., Pallet, Carton

        public int Quantity { get; set; }
        public double WeightKg { get; set; }

        // 1.4 Vehicle Selection
        public int? SelectedVehicleId { get; set; }
        public Vehicle? SelectedVehicle { get; set; }

        // 1.5 Special Services - Many-to-Many relationship handled via join table or simple list of IDs for MVP
        public List<int> SelectedServiceIds { get; set; } = new List<int>();

        public decimal TotalCost { get; set; }

        public string CustomerEmail { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // 1.8 Validation Logic can be here or in Service
        public bool IsValidDates()
        {
             return LoadingDate >= DateTime.Now.Date && UnloadingDate >= LoadingDate;
        }
    }
}
