using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAllAsync();
        Task<Location?> GetByIdAsync(int id);

        Task<Location> CreateAsync(Location location);
        Task<Location?> UpdateAsync(int id, Location location);
    }
}
