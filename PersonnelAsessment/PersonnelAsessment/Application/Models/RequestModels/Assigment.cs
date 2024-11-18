using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels
{
    public class Assigment
    {
        public DateOnly Date { get; set; }
        public string? Topic { get; set; }
        [Required]
        public int FormId { get; set; }
    }
}
