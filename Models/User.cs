using System;
using System.Collections.Generic;

namespace AgriEnergyConnect.Models;

// EF auto generated
public partial class User
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? Roleid { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Role Role { get; set; }
}
