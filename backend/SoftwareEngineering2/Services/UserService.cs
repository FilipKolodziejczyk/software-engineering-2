using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SoftwareEngineering2.Profiles;

namespace SoftwareEngineering2.Services;

public class UserService : IUserService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientModelRepository _clientModelRepository;
    private readonly IEmployeeModelRepository _employeeModelRepository;
    private readonly IDeliveryManModelRepository _deliveryManModelRepository;
    private readonly IMapper _mapper;
    private readonly PasswordHasher<UserDto> _passwordHasher;

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
        _passwordHasher = new PasswordHasher<UserDto>();
    }

    public async Task<JwtDto?> CreateJwtToken(string username, string password) {
        IUserModel? result = await _clientModelRepository.GetByEmail(username);
        result ??= await _deliveryManModelRepository.GetByEmail(username);
        result ??= await _employeeModelRepository.GetByEmail(username);

        if (result is null)
            return null;

        var passwordResult = _passwordHasher.VerifyHashedPassword(_mapper.Map<UserDto>(result), result.Password!, password);
        if (passwordResult != PasswordVerificationResult.Success)
            return null;

        var (id, role) = result switch {
            EmployeeModel model => (model.EmployeeId, Roles.Employee),
            DeliveryManModel model => (model.DeliveryManId, Roles.DeliveryMan),
            _ => ((result as ClientModel)!.ClientId, Roles.Client)
        };

        // TODO: get secret key from configuration
        var securityKey = new SymmetricSecurityKey("your-secret-key has to be long enough"u8.ToArray());
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "your-issuer",
            audience: "your-audience",
            claims: new List<Claim> {
                new(ClaimTypes.Role, role),
                new("UserID", id.ToString())
            },
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtDto {
            Jwttoken = new JwtSecurityTokenHandler().WriteToken(token)
        };
    }

    public async Task<UserDto?> CreateUser(NewUserDto newUser) {
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

        var userDto = _mapper.Map<UserDto>(model);
        model.Password = _passwordHasher.HashPassword(userDto, newUser.Password!);
        await _unitOfWork.SaveChangesAsync();
        return userDto;
    }

    public async Task<UserDto?> GetUserById(string role, int id) {
        IUserModel? result = role switch {
            Roles.Client => await _clientModelRepository.GetById(id),
            Roles.Employee => await _employeeModelRepository.GetById(id),
            Roles.DeliveryMan => await _deliveryManModelRepository.GetById(id),
            _ => null
        };

        return _mapper.Map<UserDto>(result);
    }

    public async Task<UserDto?> GetUserByEmail(string email) {
        IUserModel? result = await _clientModelRepository.GetByEmail(email);
        result ??= await _deliveryManModelRepository.GetByEmail(email);
        result ??= await _employeeModelRepository.GetByEmail(email);
        return _mapper.Map<UserDto>(result);
    }

    public async Task<UserDto?> GetAvailableDeliveryMan() {
        var deliveryMen = await _deliveryManModelRepository.GetAll();
        var result = deliveryMen.OrderBy(model => model.Orders!.Count(order => order.Status == OrderStatus.Accepted)).First();
        return _mapper.Map<UserDto>(result);
    }

    public async Task<bool> UpdateNewsletter(int id, bool subscribed) {
        var result = await _clientModelRepository.GetById(id);
        if (result is null)
            return false;
        await _clientModelRepository.UpdateNewsletter(id, subscribed);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}