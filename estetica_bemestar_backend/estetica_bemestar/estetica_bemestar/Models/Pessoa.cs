using System;
using System.Collections.Generic;

namespace estetica_bemestar.Models;

public partial class Pessoa
{
    public int Idpessoa { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public DateOnly DataNascimento { get; set; }

    public string Sexo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public bool? Ativo { get; set; }

    public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();

    public virtual Endereco? Endereco { get; set; }

    public virtual Funcionario? Funcionario { get; set; }
}
