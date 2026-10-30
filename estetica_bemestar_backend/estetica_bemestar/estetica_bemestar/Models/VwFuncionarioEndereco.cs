using System;
using System.Collections.Generic;

namespace estetica_bemestar.Models;

public partial class VwFuncionarioEndereco
{
    public int Idpessoa { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public string Cep { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public string Complemento { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    public DateOnly DataAdmissao { get; set; }
}
