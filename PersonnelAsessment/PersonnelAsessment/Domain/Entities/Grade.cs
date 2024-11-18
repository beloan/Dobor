namespace Domain.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int AssigmentId { get; set; }
        public Assigment? Assigment { get; set; }
        public int WorkerId { get; set; }
        public Worker? Worker { get; set; }
        public string? Value {  get; set; }
    }
}
