using LucidiaIT.Models.EmployeeModels;
using LucidiaIT.Models.PartnerModels;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LucidiaIT.Interfaces
{
    public interface IUploadImage
    {
        Task UploadPartnerImages(Partner partner, IEnumerable<IFormFile> files);
        Task UploadEmployeeImages(Employee employee, IEnumerable<IFormFile> files);
    }
}
