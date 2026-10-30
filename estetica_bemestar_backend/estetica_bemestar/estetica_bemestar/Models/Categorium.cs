using System;
using System.Collections.Generic;

namespace estetica_bemestar.Models;

public partial class Categorium
{
    public int Idcategoria { get; set; }

    public string Categoria { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
