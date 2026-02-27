using BCrypt.Net;

namespace ElRawabi_Backend.Helpers

{
    public class PasswordHelper
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string PasswordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }
}
