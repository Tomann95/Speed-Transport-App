using System;
using System.ComponentModel.DataAnnotations;

namespace Speed.Models
{
    public class Inquiry
    {
        public int Id { get; set; }

        [Required]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsRead { get; set; } = false;
    }
}
