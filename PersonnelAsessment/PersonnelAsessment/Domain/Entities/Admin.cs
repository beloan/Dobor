namespace Domain.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public byte[]? Salt { get; set; }
    }
}
