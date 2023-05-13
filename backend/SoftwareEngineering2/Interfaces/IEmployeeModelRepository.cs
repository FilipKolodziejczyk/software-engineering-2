using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Interfaces;

public interface IEmployeeModelRepository {
    Task AddAsync(EmployeeModel model);
    Task<EmployeeModel?> GetByEmail(string emailAddress);
    Task<EmployeeModel?> GetByID(int id);
}