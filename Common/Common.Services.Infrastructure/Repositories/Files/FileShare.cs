using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Common.Services.Infrastructure.Services.Files;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Common.DTO;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.IO.Compression;

using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.ComponentModel;
using Common.DTO.Queries;
using Common.Entities;
using System.Security.Cryptography.Xml;

namespace Common.Services.Infrastructure.Repositories.Files
{
    public class FileShare : IFileShare
    {
        private readonly IConfiguration _config;

        private string storageConnectionString, containerNameAditionalService, containerName; // Obtén esta cadena desde Azure Portal
        private byte[] aesKeyBytes, aesIVBytes;
        public FileShare(IConfiguration configuration)
        {
            _config = configuration;
            storageConnectionString = configuration.GetConnectionString("default");
            containerName = configuration.GetSection("FileShareDetails")["FileShareName"];
            containerNameAditionalService = configuration.GetSection("FileShareDetails")["FileShareNameAditionalService"];
            string aesKeyBase64 = configuration["FileShareDetails:AesKey"];
            string aesIVBase64 = configuration["FileShareDetails:AesIV"];

            aesKeyBytes = Convert.FromBase64String(aesKeyBase64);
            aesIVBytes = Convert.FromBase64String(aesIVBase64);
        }

        public async Task FileUploadAsync<T>(T ObjectResponse, int idQuery,bool isAditional=false)
        {
            var finalContainerName = isAditional ? containerNameAditionalService : containerName;
            var jsonData = JsonConvert.SerializeObject(ObjectResponse);
            // se agrega clave de encriptacion desde el config
            using var aes = Aes.Create();
            aes.Key = aesKeyBytes;
            aes.IV = aesIVBytes;

            byte[] encryptedData;
            using (var encryptor = aes.CreateEncryptor())
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(jsonData);
                }
                encryptedData = msEncrypt.ToArray();
            }
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(finalContainerName);
            //container.CreateIfNotExists();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{idQuery}.json");
            await blockBlob.UploadFromByteArrayAsync(encryptedData, 0, encryptedData.Length);

        }

        public async Task<T> FileDownloadAsync<T>(int idQuery, bool isAditional = false)
        {
            var finalContainerName = isAditional ? containerNameAditionalService : containerName;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(finalContainerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{idQuery}.json");

            // Descargar los datos encriptados desde Azure Storage
            using (var memoryStream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Desencriptar los datos
                using (var aes = Aes.Create())
                {
                    aes.Key = aesKeyBytes; // Debes proporcionar la misma clave de encriptación que se usó para encriptar
                    aes.IV = aesIVBytes;   // Debes proporcionar el mismo IV que se usó para encriptar

                    using (var decryptor = aes.CreateDecryptor())
                    using (var csDecrypt = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        string decryptedData = srDecrypt.ReadToEnd();
                        var queryDTO = JsonConvert.DeserializeObject<T>(decryptedData);
                        return await Task.FromResult(queryDTO);
                    }
                }
            }
        }


    }
}

