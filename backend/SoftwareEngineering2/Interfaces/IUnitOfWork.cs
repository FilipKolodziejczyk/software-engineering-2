namespace SoftwareEngineering2.Interfaces;

public interface IUnitOfWork {
    Task SaveChangesAsync();
}