namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public byte[]? Salt { get; set; }
        public string? Role { get; set; }
        public bool IsActivated { get; set; }
        public UserImage? Image { get; set; }
    }
}
