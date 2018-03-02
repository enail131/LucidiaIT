using LucidiaIT.Models.EmployeeModels;
using LucidiaIT.Models.PartnerModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LucidiaIT.Interfaces
{
    public interface IStorageService
    {
        Task UploadImages(IEnumerable<IFormFile> files, Employee employee = null, Partner partner = null);
        Task DeleteImages(string containerReference, string imagePath);
    }
}
