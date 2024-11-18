using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels
{
    public class Grade
    {
        public int AssigmentId { get; set; }
        public int WoirkerId { get; set; }

        [RegularExpression("(2|3|4|5|Н|Б){1}")]
        public string? Value { get; set; }
    }
}
