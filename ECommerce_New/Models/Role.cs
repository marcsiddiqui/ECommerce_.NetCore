using System;
using System.Collections.Generic;

namespace ECommerce_New.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }
}
