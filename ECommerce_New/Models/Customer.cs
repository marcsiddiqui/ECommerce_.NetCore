using System;
using System.Collections.Generic;

namespace ECommerce_New.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Cnic { get; set; } = null!;

    public int RoleId { get; set; }

    public string? ImagePath { get; set; }
}
