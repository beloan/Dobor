namespace Application.Models.ResponseModels
{
    public class Grade
    {
        public int Id { get; set; }
        public int AssigmentId { get; set; }
        public int WorkerId { get; set; }
        public string? Value { get; set; }
    }
}
