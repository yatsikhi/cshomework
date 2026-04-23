using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;
}
