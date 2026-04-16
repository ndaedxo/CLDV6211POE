using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace CLDV6211POE_Part2.Services
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;
        private const string ContainerName = "venue-images";

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AzureStorage") 
                ?? "UseDevelopmentStorage=true";
            
            _blobServiceClient = new BlobServiceClient(connectionString);
            _containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            
            CreateContainerIfNotExists();
        }

        private void CreateContainerIfNotExists()
        {
            try
            {
                _containerClient.CreateIfNotExists(PublicAccessType.Blob);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating container: {ex.Message}");
            }
        }

        public async Task<string> UploadImageAsync(IFormFile file, string existingImageUrl = null)
        {
            if (file == null || file.Length == 0)
            {
                return existingImageUrl ?? "https://via.placeholder.com/300x200?text=No+Image";
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Invalid file type. Allowed: JPG, PNG, GIF, WebP");
            }

            if (existingImageUrl != null && existingImageUrl.Contains("blob.core.windows.net") || existingImageUrl?.Contains("via.placeholder") == false)
            {
                try
                {
                    var oldBlobName = Path.GetFileName(new Uri(existingImageUrl).LocalPath);
                    await DeleteImageAsync(oldBlobName);
                }
                catch
                {
                }
            }

            var blobName = $"{Guid.NewGuid()}{extension}";
            var blobClient = _containerClient.GetBlobClient(blobName);

            var httpHeaders = new BlobHttpHeaders
            {
                ContentType = file.ContentType
            };

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = httpHeaders
            });

            return blobClient.Uri.ToString();
        }

        public async Task DeleteImageAsync(string blobName)
        {
            if (string.IsNullOrEmpty(blobName) || blobName.Contains("placeholder"))
            {
                return;
            }

            try
            {
                var blobClient = _containerClient.GetBlobClient(blobName);
                await blobClient.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting blob: {ex.Message}");
            }
        }

        public string GetImageUrl(string blobName)
        {
            if (string.IsNullOrEmpty(blobName))
            {
                return "https://via.placeholder.com/300x200?text=No+Image";
            }

            if (blobName.StartsWith("http"))
            {
                return blobName;
            }

            var blobClient = _containerClient.GetBlobClient(blobName);
            return blobClient.Uri.ToString();
        }
    }
}
