using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models;

public partial class Role
{
    [Key]
    public int Id { get; set; }

    public string? RoleTitle { get; set; }

    public DateTime? DateCreated { get; set; }

    public virtual User? Users { get; set; }
}
