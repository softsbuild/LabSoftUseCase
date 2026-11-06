using System.ComponentModel.DataAnnotations;

namespace appPetShop.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O Username é obrigatório.")]
        [Display(Name = "Nome de Utilizador")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;
    }
}