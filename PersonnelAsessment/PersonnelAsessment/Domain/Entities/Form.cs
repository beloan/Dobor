namespace Domain.Entities
{
    public class Form
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int OrganisationId { get; set; }
        public Organisation? Organisation { get; set; }
        public int LeadId { get; set; }
        public Lead? TeamLead { get; set; }
        public List<Worker> Workers { get; set; } = new();
        public List<Assigment> Assigments { get; set; } = new();
        public List<JobList> JobLists { get; set; } = new();
    }
}
