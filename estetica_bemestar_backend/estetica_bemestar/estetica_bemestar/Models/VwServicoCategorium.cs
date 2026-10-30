using System;
using System.Collections.Generic;

namespace estetica_bemestar.Models;

public partial class VwServicoCategorium
{
    public int Iditem { get; set; }

    public int Idcategoria { get; set; }

    public string Item { get; set; } = null!;

    public string DescricaoItem { get; set; } = null!;

    public decimal Preco { get; set; }

    public string TipoItem { get; set; } = null!;

    public bool? AtivoProdutoServico { get; set; }

    public string Categoria { get; set; } = null!;
}
