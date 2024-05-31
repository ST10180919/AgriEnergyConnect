using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models;

public partial class Product
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a Name for your Product")]
    public string? ProductName { get; set; }

    [Required(ErrorMessage = "Please select a Product Category")]
    public int? CategoryId { get; set; }

    [Required(ErrorMessage = "Please choose a Production Date")]
    public DateTime? ProductionDate { get; set; }

    public byte[]? Image { get; set; }

    public int? SellerId { get; set; }

    public DateTime? DateCreated { get; set; }

    public virtual Category? Category { get; set; }

    public virtual User? Seller { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
}
