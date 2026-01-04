using Microsoft.AspNetCore.Identity;

namespace Speed.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Add custom user properties here if needed for 3.0 (Subscription, etc.)
        public string FullName { get; set; } = string.Empty;

        public string SubscriptionType { get; set; } = "None"; // None, Quarterly, Annual
        public DateTime? SubscriptionExpiry { get; set; }
    }
}
