using System.ComponentModel.DataAnnotations;

namespace appPetShop.Models
{
    public class RegistroViewModel
    {
        // --- Dados de Acesso (Usuario) ---
        [Required(ErrorMessage = "O Username é obrigatório.")]
        [Display(Name = "Nome de Utilizador")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

        // --- Dados Pessoais (Cliente) ---
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        public string Telefone { get; set; } = string.Empty;
    }
}