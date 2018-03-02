using LucidiaIT.Models.EmployeeModels;
using LucidiaIT.Models.PartnerModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LucidiaIT.Interfaces
{
    public interface IStorageService
    {
        Task UploadPartnerImages(Partner partner, IEnumerable<IFormFile> files);
        Task UploadEmployeeImages(Employee employee, IEnumerable<IFormFile> files);
    }
}
