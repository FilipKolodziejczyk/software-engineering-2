﻿using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class ProductModel {
    public int ProductID { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int InStock { get; set; }

    [Required]
    public string? Description { get; set; }

    public byte[]? Image { get; set; }

    [Required]
    public bool Archived { get; set; }

    public ICollection<OrderDetailsModel>? OrderDetails { get; }
}