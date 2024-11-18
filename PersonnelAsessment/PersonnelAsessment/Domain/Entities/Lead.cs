namespace Domain.Entities
{
    public class Lead : User
    {
        public string? FIO { get; set; }
        public string? Education { get; set; }
        public List<Assigment> Assigments { get; set; } = new();
        public List<Form> Forms { get; set; } = new();
        public List<Organisation> Organisations { get; set; } = new();
    }
}
