using LucidiaIT.Models;
using LucidiaIT.Models.EmployeeModels;

namespace LucidiaIT.Services
{
    public class EmployeeService  : DataService<Employee>
    {
        public EmployeeService(EmployeeContext context) : base(context) { }
    }
}
