using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDBContext appDBContext;

        public LocationRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }
        public async Task<Location> CreateAsync(Location location)
        {
            await appDBContext.Locations.AddAsync(location);
            await appDBContext.SaveChangesAsync();
            return location;
        }

        public async Task<List<Location>> GetAllAsync()
        {
            return await appDBContext.Locations.ToListAsync();
        }

        public async Task<Location?> GetByIdAsync(int id)
        {
            return await appDBContext.Locations.FindAsync(id);
        }

        public async Task<Location?> UpdateAsync(int id, Location location)
        {
            var locationModel = await appDBContext.Locations.FindAsync(id);
            if (locationModel == null)
            {
                return null;
            }
            locationModel.Title = location.Title;
            await appDBContext.SaveChangesAsync();
            return locationModel;
        }
    }
}
