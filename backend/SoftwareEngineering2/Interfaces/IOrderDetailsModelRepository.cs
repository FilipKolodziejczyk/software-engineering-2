namespace SoftwareEngineering2.Interfaces;

using Models;
using System.Threading.Tasks;

public interface IOrderDetailsModelRepository {
    Task AddAsync(OrderDetailsModel itemModel);
}