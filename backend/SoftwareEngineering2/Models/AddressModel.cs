using System.ComponentModel.DataAnnotations;
namespace SoftwareEngineering2.Models;

public class AddressModel {
    public int AddressId { get; set; }
    
    public string? Street { get; set; }
    
    public string? BuildingNo { get; set; }
    
    public string? HouseNo { get; set; }

    
    public string? City { get; set; }
    
    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public ICollection<OrderModel> Orders { get; }
    public ClientModel? Client { get; set; }
}
