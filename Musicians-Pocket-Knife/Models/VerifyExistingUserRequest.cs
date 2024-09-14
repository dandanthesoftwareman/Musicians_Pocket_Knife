namespace Musicians_Pocket_Knife.Models
{
    public class VerifyExistingUserRequest
    {
        public bool isActive { get; set; }
        public string? GoogleId { get; set; } = null;
    }
}
