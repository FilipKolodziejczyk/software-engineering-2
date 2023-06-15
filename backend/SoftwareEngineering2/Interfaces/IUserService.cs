using SoftwareEngineering2.DTO;

namespace SoftwareEngineering2.Interfaces;

public interface IUserService {
    Task<JwtDto?> CreateJwtToken(string email, string password);
    Task<UserDto?> CreateUser(NewUserDto newUser);
    Task<UserDto?> GetUserById(string role, int id);
    Task<UserDto?> GetUserByEmail(string email);
    Task<UserDto?> GetAvailableDeliveryMan();
    Task<bool> UpdateNewsletter(int id, bool subscribed);
}