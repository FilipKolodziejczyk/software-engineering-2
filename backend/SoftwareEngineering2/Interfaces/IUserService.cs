using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces;

public interface IUserService {
    Task<JwtDTO?> CreateJWTToken(string email, string password);
    Task<UserDTO?> CreateUser(NewUserDTO newUser);
    Task<UserDTO?> GetUserByID(int id);
    Task<UserDTO?> GetUserByEmail(string email);
    Task<bool> UpdateNewsletter(int id, bool subscribed);
}