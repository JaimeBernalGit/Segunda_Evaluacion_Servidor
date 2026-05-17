namespace CursosAPI.Services
{
   public interface IUploadDocService
   {
        Task<string> UploadImageAsync(IFormFile file);
        Task<string> UploadPDFAsync(IFormFile file);
        Task DeleteImageAsync();
        Task DeleteDocAsync();
   }
}
