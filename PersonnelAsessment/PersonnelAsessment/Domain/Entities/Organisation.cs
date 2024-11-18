namespace Domain.Entities
{
    public class Organisation : User
    {
        public string? INN { get; set; }
        public string? Address { get; set; }
        public string? Name { get; set; }
        public List<Form> Forms { get; set; } = new();
        public List<Lead> Leads { get; set; } = new();
    }
}
