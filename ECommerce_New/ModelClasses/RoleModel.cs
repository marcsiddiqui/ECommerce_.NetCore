﻿using System;
using System.Collections.Generic;

namespace ECommerce_New.ModelClasses;

public partial class RoleModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

}
