namespace Application.Models.ResponseModels
{
    public class JobList
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public Form? Form { get; set; }
        public int AssigmentId { get; set; }
        public Assigment? Assigment { get; set; }
        public string? Day { get; set; }
        public int IndexNum { get; set; }
    }
}
