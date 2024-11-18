namespace Domain.Entities
{
    public class Assigment
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string? Topic { get; set; }
        public int FormId { get; set; }
        public Form? Form { get; set; }
        public List<Grade> Grades { get; set; } = new();
    }
}
