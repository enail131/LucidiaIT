using LucidiaIT.Interfaces;
using LucidiaIT.Models.EmployeeModels;
using LucidiaIT.Models.PartnerModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LucidiaIT.Services
{
    public class StorageService : IStorageService
    {
        private readonly IConfiguration _configuration;

        public StorageService(IConfiguration Configuration)
        {
            _configuration = Configuration;            
        }

        private CloudStorageAccount GetStorageAccount()
        {
            string accountName = _configuration["StorageSettings:AccountName"];
            string accountKey = _configuration["StorageSettings:AccountKey"];
            StorageCredentials storageCredentials = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);
            return storageAccount;
        }

        private async Task StoreImage(string containerReference, string fileName, IFormFile file)
        {
            CloudStorageAccount storageAccount = GetStorageAccount();
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerReference);
            await blobContainer.CreateIfNotExistsAsync();

            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);
            
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
        }

        private string BuildImageUrl(string containerName, string fileName)
        {
            var storageUrl = _configuration["StorageSettings:LucidiaStorageUrl"];
            StringBuilder sb = new StringBuilder();
            sb.Append(storageUrl);
            sb.Append(containerName);
            sb.Append("/");
            sb.Append(fileName);
            return sb.ToString();
        }

        public async Task UploadEmployeeImages(Employee employee, IEnumerable<IFormFile> files)
        {
            var containerName = "employee";
            int i = 0;
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString().Replace("-", "") +
                                    Path.GetExtension(file.FileName);                    
                    await StoreImage(containerName, fileName, file);
                    if (i == 0)
                    {
                        employee.InitialImage = BuildImageUrl(containerName, fileName);
                    }
                    else if (i == 1)
                    {
                        employee.HoverImage = BuildImageUrl(containerName, fileName);
                    }
                }
                i++;
            }
        }

        public  async Task UploadPartnerImages(Partner partner, IEnumerable<IFormFile> files)
        {
            string containerName = "partners";
            int i = 0;
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString().Replace("-", "") +
                                    Path.GetExtension(file.FileName);
                    await StoreImage(containerName, fileName, file);
                    if (i == 0)
                    {
                        partner.Logo = BuildImageUrl(containerName, fileName);
                    }
                }
                i++;
            }
        }
    }
}
