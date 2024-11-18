namespace Application.Models.ResponseModels
{
    public class Assigment
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string? Topic { get; set; }
        public int FormId { get; set; }
    }
}
