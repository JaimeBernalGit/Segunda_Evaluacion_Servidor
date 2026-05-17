
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CursosAPI.Utils;

namespace CursosAPI.Services
{
    public class CloudinaryUploadDocService: IUploadDocService
    {
        private readonly Cloudinary _cloudinary;

       public CloudinaryUploadDocService(IConfiguration configuration)
       {
           var cloudName = configuration["CloudinarySettings:CloudName"];
           var apiKey = configuration["CloudinarySettings:ApiKey"];
           var apiSecret = configuration["CloudinarySettings:ApiSecret"];
           var account = new Account(cloudName, apiKey, apiSecret);
           _cloudinary = new Cloudinary(account);
       }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var imageValidator = new FileValidationHelper(
               new[] { "image/jpeg", "image/png", "image/gif" },
               new[] { ".jpg", ".jpeg", ".png", ".gif" }
           );
           imageValidator.Validate(file);

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
           {
               File = new FileDescription(file.FileName, stream),
               Transformation = new Transformation().Width(400).Height(400).Crop("fill")
               //Transformation = new Transformation().Width(300).Crop("scale").Chain().Effect("cartoonify")
               //Effect(vignette",20), Effect("remove_background", 0.5), Effect("sharpen", 50)  
           };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl?.ToString();
        }

          public async Task<string> UploadPDFAsync(IFormFile file)
        {
            var pdfValidator = new FileValidationHelper(
                new[] { "application/pdf" },
                new[] { ".pdf" }
            );
            pdfValidator.Validate(file);

            using var stream = file.OpenReadStream();
            var uploadParams = new RawUploadParams
           {
               File = new FileDescription(file.FileName, stream)
           };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl?.ToString();
        }

        public async Task DeleteImageAsync()
        {
            
        }

        public async Task DeleteDocAsync()
        {
            
        }
        
    }
}
