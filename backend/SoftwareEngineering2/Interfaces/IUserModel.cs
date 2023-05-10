namespace SoftwareEngineering2.Models;

public interface IUserModel {
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
