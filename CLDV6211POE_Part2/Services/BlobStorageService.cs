using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace CLDV6211POE_Part2.Services
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;
        private readonly ILogger<BlobStorageService>? _logger;
        private const string ContainerName = "venue-images";

        public BlobStorageService(IConfiguration configuration, ILogger<BlobStorageService> logger)
        {
            _logger = logger;
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
                _logger?.LogError(ex, "Error creating container {ContainerName}", ContainerName);
            }
        }

        private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5MB

        public async Task<string> UploadImageAsync(IFormFile? file, string? existingImageUrl = null)
        {
            if (file == null || file.Length == 0)
            {
                return existingImageUrl ?? "https://via.placeholder.com/300x200?text=No+Image";
            }

            if (file.Length > MaxFileSizeBytes)
            {
                throw new InvalidOperationException($"File size exceeds 5MB limit. Current size: {file.Length / 1024 / 1024:F1}MB");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Invalid file type. Allowed: JPG, PNG, GIF, WebP");
            }

            if (!string.IsNullOrEmpty(existingImageUrl) && !existingImageUrl.Contains("via.placeholder"))
            {
                try
                {
                    await DeleteImageAsync(existingImageUrl);
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

        public async Task DeleteImageAsync(string blobNameOrUrl)
        {
            if (string.IsNullOrEmpty(blobNameOrUrl) || blobNameOrUrl.Contains("placeholder"))
            {
                return;
            }

            try
            {
                string blobName = blobNameOrUrl;
                
                if (blobNameOrUrl.StartsWith("http"))
                {
                    blobName = Path.GetFileName(new Uri(blobNameOrUrl).LocalPath);
                }
                
                var blobClient = _containerClient.GetBlobClient(blobName);
                await blobClient.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting blob {BlobNameOrUrl}", blobNameOrUrl);
            }
        }

    }
}
