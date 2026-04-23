using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Mail { get; set; }

    public string? Password { get; set; }
}
