namespace Musicians_Pocket_Knife.Models
{
    public class VerifyExistingUserRequest
    {
        public string? GoogleId { get; set; } = null;
        public bool isActive { get; set; }
    }
}
