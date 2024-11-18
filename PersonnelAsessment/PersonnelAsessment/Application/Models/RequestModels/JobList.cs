using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels
{
    public class JobList
    {
        public int FormId { get; set; }
        public int? AssigmentId { get; set; }

        [RegularExpression("(Monday|Tuesday|Wednesday|Thirsday|Friday|Sunday|Saturday){1}")]
        public string? Day { get; set; }

        [Range(1, 8)]
        public int IndexNum { get; set; }
    }
}
