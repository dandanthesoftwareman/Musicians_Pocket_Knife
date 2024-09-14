namespace Musicians_Pocket_Knife.Models
{
    public class CreateNewUserRequest
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
