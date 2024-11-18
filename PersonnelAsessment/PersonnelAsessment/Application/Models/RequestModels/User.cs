using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels
{
    public class User
    {

        [RegularExpression("^\\S+@\\S+\\.\\S+$", ErrorMessage = "Неверный формат почты")]
        public string? Email { get; set; }

        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Пароль имеет неверный формат")]
        public string? Password { get; set; }
    }
}
