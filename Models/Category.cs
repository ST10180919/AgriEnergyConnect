using System;
using System.Collections.Generic;

namespace AgriEnergyConnect.Models;

// EF auto generated
public partial class Category
{
    public int Id { get; set; }

    public string? CategoryName { get; set; }

    public DateTime? DateCreated { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
