namespace SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Models;
using System.Threading.Tasks;

public interface IOrderDetailsModelRepository {
    Task AddAsync(OrderDetailsModel itemModel);
}