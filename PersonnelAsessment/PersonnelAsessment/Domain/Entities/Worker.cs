namespace Domain.Entities
{
    public class Worker : User
    {
        public string? FIO { get; set; }
        public int FormId { get; set; }
        public Form? Form { get; set; }
        public List<Grade> Grades { get; set; } = new();
    }
}
