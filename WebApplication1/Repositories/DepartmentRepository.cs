using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDBContext appDBContext;

        public DepartmentRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }
        public async Task<List<Department>> GetAllAsync()
        {
           return await appDBContext.Departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return  await appDBContext.Departments.FindAsync(id);
        }

        public async Task<Department> CreateAsync(Department department)
        {
            await appDBContext.Departments.AddAsync(department);
            await appDBContext.SaveChangesAsync();
            return department;
        }

        public async Task<Department?> UpdateAsync(int id,Department department)
        {

            var departmentModel = await appDBContext.Departments.FindAsync(id);
            if (departmentModel == null)
            {
                return null;
            }
            departmentModel.Title = department.Title;
            await appDBContext.SaveChangesAsync();
            return departmentModel;
        }
    }
}
