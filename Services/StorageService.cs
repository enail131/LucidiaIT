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

        public StorageService(IConfiguration Configuration) => _configuration = Configuration;

        public async Task UploadImages(IEnumerable<IFormFile> files, Employee employee = null, Partner partner = null)
        {
            string containerName = (employee != null) ? "employee" : "partners";
            int i = 0;
            foreach (var file in files)
            {
                if ((file != null) && (file.Length > 0))
                {
                    string fileName = Guid.NewGuid().ToString().Replace("-", "") +
                                    Path.GetExtension(file.FileName);
                    string imageUrl = BuildImageUrl(containerName, fileName);
                    await StoreImage(containerName, fileName, file);
                    if (containerName.Equals("employee"))
                    {
                        SetEmployeeImages(employee, imageUrl, i);
                    }
                    else
                    {
                        partner.Logo = imageUrl;
                    }
                    i++;
                }
            }
        }

        public async Task DeleteImages(string containerReferenceName, string imagePath)
        {
            CloudBlobContainer blobContainer = GetBlobContainerReference(containerReferenceName);
            string fileName = ParseImagePath(imagePath);
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);
            await blockBlob.DeleteAsync();
        }

        private string ParseImagePath(string imagePath) => imagePath.Substring((imagePath.LastIndexOf("/") + 1));

        private CloudBlobContainer GetBlobContainerReference(string containerReferenceName)
        {
            string accountName = _configuration["StorageSettings:AccountName"];
            string accountKey = _configuration["StorageSettings:AccountKey"];
            StorageCredentials storageCredentials = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerReferenceName);
            return blobContainer;
        }

        private async Task StoreImage(string containerReferenceName, string fileName, IFormFile file)
        {
            CloudBlobContainer blobContainer = GetBlobContainerReference(containerReferenceName);
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

        private void SetEmployeeImages(Employee employee, string imageUrl, int index)
        {
            if (index == 0)
            {
                employee.InitialImage = imageUrl;
            }
            else
            {
                employee.HoverImage = imageUrl;
            }
        }
    }
}
