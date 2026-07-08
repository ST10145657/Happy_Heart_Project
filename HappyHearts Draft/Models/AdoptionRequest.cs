namespace HappyHearts_Draft.Models
{
    public class AdoptionRequest
    {
        public int RequestID { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int PetID { get; set; }

        public DateTime RequestDate { get; set; }

        public string Status { get; set; } = "Pending";
    }
}
