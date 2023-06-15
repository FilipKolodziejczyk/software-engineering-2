﻿using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class AddressModel {
    public int AddressId { get; set; }

    [Required]
    public string? Street { get; set; }

    [Required]
    public string? BuildingNo { get; set; }

    [Required]
    public string? HouseNo { get; set; }


    [Required]
    public string? City { get; set; }

    [Required]
    public string? PostalCode { get; set; }

    [Required]
    public string? Country { get; set; }

    public ICollection<OrderModel>? Orders { get; }
    public ClientModel? Client { get; set; }
}
