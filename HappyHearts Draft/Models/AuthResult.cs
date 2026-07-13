namespace HappyHearts_Draft.Models
{
    public class AuthResult
    {
        public bool Success { get; set; }

        public string Message { get; set; } = "";

        public string UserId { get; set; } = "";

        public string Email { get; set; } = "";

        public string Role { get; set; } = "";

        public string FullName { get; set; } = "";
    }
}
