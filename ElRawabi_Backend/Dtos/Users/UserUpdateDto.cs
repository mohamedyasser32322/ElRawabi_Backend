using ElRawabi_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Users
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string? FullName { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public Role? Role { get; set; }
        public bool IsActive { get; set; }
    }
}
