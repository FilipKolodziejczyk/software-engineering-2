namespace SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;
public interface IOrderModelRepository
{
    Task AddAsync(OrderModel model);
}