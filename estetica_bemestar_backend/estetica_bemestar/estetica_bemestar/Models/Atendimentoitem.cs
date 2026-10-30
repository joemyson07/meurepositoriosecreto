using System;
using System.Collections.Generic;

namespace estetica_bemestar.Models;

public partial class Atendimentoitem
{
    public int Idatendimento { get; set; }

    public int Iditem { get; set; }

    public int? Idfuncionario { get; set; }

    public int QuantidadeItens { get; set; }

    public decimal ValorUnitarioVenda { get; set; }

    public virtual Atendimento IdatendimentoNavigation { get; set; } = null!;

    public virtual Funcionario? IdfuncionarioNavigation { get; set; }

    public virtual Item IditemNavigation { get; set; } = null!;
}
