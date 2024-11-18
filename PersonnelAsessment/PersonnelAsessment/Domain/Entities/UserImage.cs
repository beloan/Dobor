namespace Domain.Entities
{
    public class UserImage
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public string? ImageName { get; set; }
    }
}
