using LucidiaIT.Interfaces;
using LucidiaIT.Models;
using LucidiaIT.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LucidiaIT.Services
{
    public class EmployeeService : IDataService<Employee>
    {
        private readonly EmployeeContext _dbcontext;

        public EmployeeService(EmployeeContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<int> CreateAsync(Employee employee)
        {
            _dbcontext.Add(employee);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Employee employee)
        {
            _dbcontext.Employee.Remove(employee);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<int> EditAsync(Employee employee)
        {
            _dbcontext.Update(employee);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<Employee> GetDataObjectAsync(int? id)
        {
            return await _dbcontext.Employee.SingleOrDefaultAsync(m => m.ID == id);
        }

        public async Task<List<Employee>> GetListAsync()
        {
            return await _dbcontext.Employee.ToListAsync();
        }

        public bool DataObjectExists(int id)
        {
            return _dbcontext.Employee.Any(e => e.ID == id);
        }
    }
}
