using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ECommerce_New.ModelClasses;

public partial class CustomerModel
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Cnic { get; set; } = null!;

    public List<SelectListItem> RoleOptions { get; set; }

    public int RoleId { get; set; }

    public string RoleName { get; set; }

    public CustomerModel()
    {
        RoleOptions = new List<SelectListItem>();
    }

    public string ImagePath { get; set; }
}
