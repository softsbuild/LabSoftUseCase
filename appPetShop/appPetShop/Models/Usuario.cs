using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace appPetShop.Models;

public partial class Usuario
{
    [Key]
    public int Codigo { get; set; }

    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Senha { get; set; } = null!;
}
