using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace api_music.Services
{
    public class StorageFileAzure : IFileUploader
    {
        private readonly string connectionString;
        public StorageFileAzure(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorage");
        }
        public async Task deleteFile(string url, string container)
        {
            if(string.IsNullOrEmpty(url)) { return; }

            var client = new BlobContainerClient(connectionString, container);
            await client.CreateIfNotExistsAsync();

            var file = Path.GetFileName(url);
            var blob = client.GetBlobClient(file);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> editFile(byte[] content, string extension, string container, string url, string contentType)
        {
            await deleteFile(url, container);
            return await saveFile(content,extension,container, contentType);
        }

        public async Task<string> saveFile(byte[] content, string extension, string container, string contentType)
        {
            var client = new BlobContainerClient(connectionString, container);
            await client.CreateIfNotExistsAsync();
            client.SetAccessPolicy(PublicAccessType.Blob);
            
            var fileName = $"{Guid.NewGuid()}{extension}";
            var blob = client.GetBlobClient(fileName);

            var blobUploadOptions = new BlobUploadOptions();
            var blobHttpHeader = new BlobHttpHeaders();
            blobHttpHeader.ContentType = contentType;
            blobUploadOptions.HttpHeaders = blobHttpHeader;

            await blob.UploadAsync(new BinaryData(content), blobUploadOptions);
            return blob.Uri.ToString();
    
        }
    }
}
