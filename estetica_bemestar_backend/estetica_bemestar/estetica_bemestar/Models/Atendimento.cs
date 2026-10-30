using System;
using System.Collections.Generic;

namespace estetica_bemestar.Models;

public partial class Atendimento
{
    public int Idatendimento { get; set; }

    public int Idpessoa { get; set; }

    public DateTime DataAtendimento { get; set; }

    public string StatusAtendimento { get; set; } = null!;

    public decimal? ValorTotal { get; set; }

    public virtual ICollection<Atendimentoitem> Atendimentoitems { get; set; } = new List<Atendimentoitem>();

    public virtual Pessoa IdpessoaNavigation { get; set; } = null!;
}
