using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);

        Task<Department> CreateAsync(Department department);
        Task<Department?> UpdateAsync(int id, Department department);
    }
}
