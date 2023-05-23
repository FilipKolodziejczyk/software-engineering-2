using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;
using SoftwareEngineering2.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace SoftwareEngineering2.Services;

public class UserService : IUserService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientModelRepository _clientModelRepository;
    private readonly IEmployeeModelRepository _employeeModelRepository;
    private readonly IDeliveryManModelRepository _deliveryManModelRepository;
    private readonly IMapper _mapper;
    private readonly PasswordHasher<UserDTO> _passwordHasher;

    public UserService(
        IUnitOfWork unitOfWork,
        IClientModelRepository clientModelRepository,
        IEmployeeModelRepository employeeModelRepository,
        IDeliveryManModelRepository deliveryManModelRepository,
        IMapper mapper) {
        _unitOfWork = unitOfWork;
        _clientModelRepository = clientModelRepository;
        _employeeModelRepository = employeeModelRepository;
        _deliveryManModelRepository = deliveryManModelRepository;
        _mapper = mapper;
        _passwordHasher = new();
    }

    public async Task<JwtDTO?> CreateJWTToken(string username, string password) {
        IUserModel? result = await _clientModelRepository.GetByEmail(username);
        result ??= await _deliveryManModelRepository.GetByEmail(username);
        result ??= await _employeeModelRepository.GetByEmail(username);

        if (result is null)
            return null;

        var passwordResult = _passwordHasher.VerifyHashedPassword(_mapper.Map<UserDTO>(result), result.Password!, password);
        if (passwordResult != PasswordVerificationResult.Success)
            return null;

        (int id, string role) = result switch {
            EmployeeModel => ((result as EmployeeModel)!.EmployeeID, Roles.Employee),
            DeliveryManModel => ((result as DeliveryManModel)!.DeliveryManID, Roles.DeliveryMan),
            _ => ((result as ClientModel)!.ClientID, Roles.Client),
        };

        // TODO: get secret key from configuration
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key has to be long enough"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "your-issuer",
            audience: "your-audience",
            claims: new List<Claim> {
                new Claim(ClaimTypes.Role, role),
                new Claim("UserID", id.ToString())
            },
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtDTO() {
            Jwttoken = new JwtSecurityTokenHandler().WriteToken(token),
        };
    }

    public async Task<UserDTO?> CreateUser(NewUserDTO newUser) {
        IUserModel model;

        switch (newUser.Role) {
        case Roles.DeliveryMan:
            model = _mapper.Map<DeliveryManModel>(newUser);
            await _deliveryManModelRepository.AddAsync((DeliveryManModel) model);
            break;
        case Roles.Employee:
            model = _mapper.Map<EmployeeModel>(newUser);
            await _employeeModelRepository.AddAsync((EmployeeModel) model);
            break;
        default:
            model = _mapper.Map<ClientModel>(newUser);
            await _clientModelRepository.AddAsync((ClientModel) model);
            break;
        }

        UserDTO userDTO = _mapper.Map<UserDTO>(model);
        model.Password = _passwordHasher.HashPassword(userDTO, newUser.Password!);
        await _unitOfWork.SaveChangesAsync();
        return userDTO;
    }

    public async Task<UserDTO?> GetUserByID(string role, int id) {
        IUserModel? result = role switch {
            Roles.Client => await _clientModelRepository.GetByID(id),
            Roles.Employee => await _employeeModelRepository.GetByID(id),
            Roles.DeliveryMan => await _deliveryManModelRepository.GetByID(id),
            _ => null,
        };

        return _mapper.Map<UserDTO>(result);
    }

    public async Task<UserDTO?> GetUserByEmail(string email) {
        IUserModel? result = await _clientModelRepository.GetByEmail(email);
        result ??= await _deliveryManModelRepository.GetByEmail(email);
        result ??= await _employeeModelRepository.GetByEmail(email);
        return _mapper.Map<UserDTO>(result);
    }

    public async Task<bool> UpdateNewsletter(int id, bool subscribed) {
        var result = await _clientModelRepository.GetByID(id);
        if (result is null)
            return false;
        await _clientModelRepository.UpdateNewsletter(id, subscribed);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}