using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Models
{
    public enum Role
    {
        Admin = 1,
        Employee = 2,
        Client = 3,
    };
    public class User
    {
        public int Id { get; set; }
        [Required,StringLength(100)]
        public string? FullName { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required,Phone]
        public string? PhoneNumber { get; set; }
        [Required,EmailAddress]
        public string? Email { get; set; }
        [Required,PasswordPropertyText]
        public string? PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
}
