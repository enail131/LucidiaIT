using LucidiaIT.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LucidiaIT.Interfaces
{
    public interface IDataService<T>
    {
        Task<List<T>> GetListAsync();
        Task<T> GetDataObjectAsync(int? id);
        Task<int> CreateAsync(T t);
        Task<int> EditAsync(T t);
        Task<int> DeleteAsync(T t);
        bool DataObjectExists(int id);
    }
}
