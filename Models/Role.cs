using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models;

// EF auto generated
public partial class Role
{
    [Key]
    public int Id { get; set; }

    public string? RoleTitle { get; set; }

    public DateTime? DateCreated { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
