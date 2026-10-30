using System;
using System.Collections.Generic;

namespace estetica_bemestar.Models;

public partial class VwAtendimento
{
    public int Idatendimento { get; set; }

    public int Idpessoa { get; set; }

    public DateTime DataAtendimento { get; set; }

    public string StatusAtendimento { get; set; } = null!;

    public string Cliente { get; set; } = null!;
}
