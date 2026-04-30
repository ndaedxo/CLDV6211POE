using CLDV6211POE_Part2.Services;
using Microsoft.AspNetCore.Mvc;

namespace CLDV6211POE_Part2.Controllers
{
    public class ImagesController : Controller
    {
        private readonly BlobStorageService _blobService;

        public ImagesController(BlobStorageService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet("images/{blobName}")]
        public async Task<IActionResult> GetImage(string blobName)
        {
            try
            {
                var containerClient = _blobService.GetContainerClient();
                var blobClient = containerClient.GetBlobClient(blobName);
                
                var exists = await blobClient.ExistsAsync();
                if (!exists.Value)
                {
                    return NotFound();
                }

                var downloadInfo = await blobClient.DownloadAsync();
                var contentType = downloadInfo.Value.ContentType ?? "application/octet-stream";
                
                return File(downloadInfo.Value.Content, contentType);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
