using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models;

public partial class Category
{
    [Key]
    public int Id { get; set; }

    public string? CategoryName { get; set; }

    public DateTime? DateCreated { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
