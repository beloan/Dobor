using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels
{
    public class Form
    {
        public int Id { get; set; }

        [Range(1, 11)]
        public int Number { get; set; }
        public char Litera { get; set; }
        public int OrganisationId { get; set; }
        public int TeamLeadId { get; set; }
    }
}