using System;
using System.Collections.Generic;

namespace estetica_bemestar.Models;

public partial class VwPessoaEndereco
{
    public int Idpessoa { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public string Cep { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public string Complemento { get; set; } = null!;
}
