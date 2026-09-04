using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppTask.Models;

public partial class Funcionario
{
    public int Codigo { get; set; }

    public string Nome { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    public int? CodigoGerente { get; set; }

    [ForeignKey("CodigoGerente")]
    public virtual Funcionario? GerenteNavigation { get; set; }

    public virtual ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
}
