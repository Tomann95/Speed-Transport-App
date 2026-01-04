using System.ComponentModel.DataAnnotations;

namespace Speed.Models
{
    public class PageContent
    {
        public int Id { get; set; }

        [Required]
        public string PageName { get; set; } = string.Empty; // e.g., "Home", "About", "Contact"

        [Required]
        public string SectionKey { get; set; } = string.Empty; // e.g., "HeroTitle", "FooterText"

        public string Content { get; set; } = string.Empty;
    }
}
